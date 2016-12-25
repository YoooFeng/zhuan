using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace zhuan
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int m = 0;
        int i = 0;
        int a = 0;
        private ME me;
        private bool ismovingup = false;
        private bool ismovingdown = false;
        private bool ismovingleft = false;
        private bool ismovingright = false;
        public static MainWindow OpenFrom;
        Control c;
        Thread lk1;
        Thread lk2;
        Thread lk3;
        Thread lk4;

        public MainWindow(int mission)
        {
            m = mission;
            OpenFrom = this;
            InitializeComponent();
                        #region 设置全屏
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.WindowStyle = System.Windows.WindowStyle.None;
                        this.ResizeMode = System.Windows.ResizeMode.NoResize;
                        this.Topmost = true;
                        this.Left = 0.0;
                        this.Top = 0.0;
                        this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                        this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                        #endregion
            
            me = new ME();
            me.AglChange += this.receive_AglChange;
            c = new Control(mission);
            lk1 = new Thread(ListenKeyLeft);
            lk1.Start();
            lk2 = new Thread(ListenKeyRight);
            lk2.Start();
            lk3 = new Thread(ListenKeyDown);
            lk3.Start();
            lk4 = new Thread(ListenKeyUp);
            lk4.Start();
        }

        public int Getme()
        {
            return me.agl;
        }

        public void receive_AglChange()
        {
            i++;
            try
            {
                img1.Dispatcher.Invoke(new Action(delegate { zhuan1.Angle = i; }));

            }
            catch (Exception)
            { }
            //            t1.Dispatcher.Invoke(new Action(delegate { t1zhuan.Angle = i/360; }));
        }

        private void ListenKeyLeft()
        {
            while (true)
            {
                if (ismovingleft)
                    moveLeft();
            }

        }

        private void ListenKeyRight()
        {
            while (true)
            {
                if (ismovingright)
                    moveRight();
            }

        }
        private void ListenKeyDown()
        {
            while (true)
            {
                if (ismovingdown)
                    moveDown();
            }

        }
        private void ListenKeyUp()
        {
            while (true)
            {
                if (ismovingup)
                    moveUp();
            }

        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Up))
            {
                if (!ismovingup)
                {
                    ismovingup = true;
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Down))
            {
                if (!ismovingdown)
                {
                    ismovingdown = true;
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Left))
            {
                if (!ismovingleft)
                {
                    ismovingleft = true;
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Right))
            {
                if (!ismovingright)
                {
                    ismovingright = true;
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Space))
            {
                c.save();
                testbox.Text = "space";
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Enter))
            {
                testbox.Text = "enter";
                c.load();
            }

        }
        private void moveUp()
        {
            //                img1.Dispatcher.Invoke(new Action(delegate { zhuan2.Y -=5; }));
            c.moveup();
            if (m == 1)
            {
                Thread.Sleep(3);
            }
            if (m == 2)
            {
                Thread.Sleep(1);
            }
            if (m == 3)
            {
                Thread.Sleep(2);
            }

        }
        private void moveDown()
        {
            //   img1.Dispatcher.Invoke(new Action(delegate { zhuan2.Y += 5; }));
            c.movedown();
            if (m == 1)
            {
                Thread.Sleep(3);
            }
            if (m == 2)
            {
                Thread.Sleep(1);
            }
            if (m == 3)
            {
                Thread.Sleep(2);
            }

        }
        private void moveLeft()
        {
            //                img1.Dispatcher.Invoke(new Action(delegate { zhuan2.X -= 5; }));
            c.moveleft();
            if (m == 1)
            {
                Thread.Sleep(3);
            }
            if (m == 2)
            {
                Thread.Sleep(1);
            }
            if (m == 3)
            {
                Thread.Sleep(2);
            }

        }
        private void moveRight()
        {
            //                img1.Dispatcher.Invoke(new Action(delegate { zhuan2.X += 5; }));
            c.moveright();
            if (m == 1)
            {
                Thread.Sleep(3);
            }
            if (m == 2)
            {
                Thread.Sleep(1);
            }
            if (m == 3)
            {
                Thread.Sleep(2);
            }

        }

        public void end()
        {
            lk1.Abort();
            lk2.Abort();
            lk3.Abort();
            lk4.Abort();
        }
        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyUp(Key.Up))
            {
                if (ismovingup)
                {
                    ismovingup = false;
                }
            }
            if (e.KeyboardDevice.IsKeyUp(Key.Down))
            {
                if (ismovingdown)
                {
                    ismovingdown = false;
                }
            }
            if (e.KeyboardDevice.IsKeyUp(Key.Left))
            {
                if (ismovingleft)
                {
                    ismovingleft = false;
                }
            }
            if (e.KeyboardDevice.IsKeyUp(Key.Right))
            {
                if (ismovingright)
                {
                    ismovingright = false;
                }
            }

        }
    }

}
