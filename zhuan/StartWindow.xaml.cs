using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace zhuan
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        int i = 0;
        int stage = 0;
        public StartWindow()
        {
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
            ME me = new ME();
            me.AglChange += this.receive_AglChange;

        }
        public void receive_AglChange()
        {
            i+=2;
            try
            {
                img1.Dispatcher.Invoke(new Action(delegate { zhuan1.Angle = i; }));
                img2.Dispatcher.Invoke(new Action(delegate { zhuan2.Angle = i; }));
            }
            catch (Exception)
            { }

            //            t1.Dispatcher.Invoke(new Action(delegate { t1zhuan.Angle = i/360; }));
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Up))
            {
                if (stage > 0)
                {
                    stage--;
                    img1.Dispatcher.Invoke(new Action(delegate { dong1.Y -= 75; }));
                    img2.Dispatcher.Invoke(new Action(delegate { dong2.Y -= 75; }));
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Down))
            {
                if (stage < 2)
                {
                    stage++;
                    img1.Dispatcher.Invoke(new Action(delegate { dong1.Y += 75; }));
                    img2.Dispatcher.Invoke(new Action(delegate { dong2.Y += 75; }));
                }
            }
            if (e.KeyboardDevice.IsKeyDown(Key.Enter))
            {
                int tmp = stage + 1;
                stage = 0;
                MainWindow mw = new MainWindow(tmp);
                mw.Show();
                this.Close();

            }

        }

    }
}
