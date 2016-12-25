using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace zhuan
{
    class Control
    {
        private List<object> shape_save = new List<object>();
        private List<object> shape = new List<object>();

        private StartWindow bitch= new StartWindow();
        Thread t ;
        Thread t1;
        int fuckyou=-1;

        [DllImport("gdi32")]
        private static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private void loadMap(int mission)
        {
            string map = "mission" + mission + ".dll";
            Assembly asm = Assembly.LoadFrom(@map);//这里改dll路径
            Type t = asm.GetType("MapData.myMap");//这里填类名
            Object obj = Activator.CreateInstance(t);
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod;
            t.InvokeMember("getMap", flag, null, obj, new object[] { shape });
            if (shape != null)
                Console.WriteLine(shape.Count());
            else Console.WriteLine("false");
        }

        private int getX()
        {
            int me = MainWindow.OpenFrom.Getme();
            double angle = ((double)me / 180) * Math.PI;
            double l = 89 * Math.Cos(angle);
            return (int)l;
        }

        private int getY()
        {
            int me = MainWindow.OpenFrom.Getme();
            double angle = ((double)me / 180) * Math.PI;
            double l = 89 * Math.Sin(angle);
            return (int)l;
        }

        public void check_win()
        {
            while (true)
            {
                Thread.Sleep(500);
                Type type = shape[0].GetType();
                List<int> info = type.InvokeMember("getInfo", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[0], null) as List<int>;
                int x = Convert.ToInt32(info[0] - info[1]), y = Convert.ToInt32(info[2] - info[3]);
                if (x < 600 && y < 400)
                {
                    fuckyou++;

                        t.Abort();
                        for (int sad = 0; sad < 1000; sad++)
                        {
                            moveright();
                        }
                        MainWindow.OpenFrom.Dispatcher.Invoke(new Action(() =>
                        {
                            Image im = new Image();
                            Uri uri = new Uri("pack://application:,,,/images/youwin.png");
                            BitmapImage bitmap = new BitmapImage(uri);
                            im.Source = bitmap;
                            im.Width = 600;
                            im.Height = 400;
                            im.Stretch = Stretch.Fill;
                            Point p = new Point();
                            p.X = 0.5;
                            p.Y = 0.5;
                            im.RenderTransformOrigin = p;
                            MainWindow.OpenFrom.maingrid.Children.Add(im);
                        }));
                        Thread.Sleep(2000);
                        for (int i = 0; i < shape.Count(); i++)
                        {
                            Type ty = shape[i].GetType();
                            ty.InvokeMember("terminate", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], new object[] { MainWindow.OpenFrom.maingrid});

                        }
                        shape.Clear();

                        MainWindow.OpenFrom.Dispatcher.Invoke(new Action(() =>
                        {
                            MainWindow.OpenFrom.end();
//                            MainWindow.OpenFrom.Close();
//                            Application.Current.Shutdown(-1);
                            Environment.Exit(0);
                        }));
//                        bitch.Dispatcher.Invoke(new Action(() =>
//                        {
//                            bitch.Show();
//                        }));
 
                        t1.Abort();

                }

            }

        }
        public void check()
        {
            int a = 0;
            while (true)
            {
                Thread.Sleep(10);
                MainWindow.OpenFrom.Dispatcher.Invoke(new Action(() =>
                {
                    var helper = new WindowInteropHelper(MainWindow.OpenFrom);
                }));



                int x_1 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 + getX();
                int y_1 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 + getY();
                int x_2 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 - getX();
                int y_2 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 - getY();
                int x_3 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 + (getX() / 2);
                int y_3 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 + (getY() / 2);
                int x_4 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 - getX() / 2;
                int y_4 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 - getY() / 2;
                int x_5 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2;
                int y_5 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2;
                int x_6 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 + (getX() / 4)*3;
                int y_6 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 + (getY() / 4)*3;
                int x_7 = System.Int32.Parse(MainWindow.OpenFrom.ActualWidth.ToString()) / 2 - (getX() / 4)*3;
                int y_7 = System.Int32.Parse(MainWindow.OpenFrom.ActualHeight.ToString()) / 2 - (getY() / 4)*3;

                IntPtr lDC = GetWindowDC((IntPtr)0);
                List<int> color = new List<int>();
                color.Add(GetPixel(lDC, x_1, y_1));
                color.Add(GetPixel(lDC, x_2, y_2));
                color.Add(GetPixel(lDC, x_3, y_3));
                color.Add(GetPixel(lDC, x_4, y_4));
                color.Add(GetPixel(lDC, x_5, y_5));
                color.Add(GetPixel(lDC, x_6, y_6));
                color.Add(GetPixel(lDC, x_7, y_7));

                ReleaseDC((IntPtr)0, lDC);
                foreach (int i in color)
                {
                    Byte r = (Byte)((i >> 0x10) & 0xffL);
                    Byte g = (Byte)((i >> 8) & 0xffL);
                    Byte b = (Byte)(i & 0xffL);
                    if (b >=5)
                    {
/*                        a++;
                        MainWindow.OpenFrom.testbox.Dispatcher.Invoke(new Action(() =>
                        {
                            MainWindow.OpenFrom.testbox.Text = a.ToString() + " " + r.ToString() + " " + g.ToString() + " " + b.ToString();
                        }));
*/                        MainWindow.OpenFrom.Dispatcher.Invoke(new Action(() =>
                        {
                            Storyboard sb=(Storyboard)MainWindow.OpenFrom.maingrid.FindResource("fuck");
                            sb.Begin();

                            load();

                        }));



                    }
                    else
                        MainWindow.OpenFrom.testbox.Dispatcher.Invoke(new Action(() =>
                        {
                            //                            MainWindow.OpenFrom.testbox.Text = "go on";
                        }));
                }

            }
        }


        public Control(int mission)
        {
            loadMap(mission);
            save();
            show();
            t = new Thread(check);
            t.Start();
            t1 = new Thread(check_win);
            t1.Start();
        }


        public void show()
        {
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("show", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], new object[] { MainWindow.OpenFrom.maingrid });

            }
        }

        public void moveup()
        {
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("moveup", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], null);

            }
        }
        public void movedown()
        {
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("movedown", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], null);

            }
        }
        public void moveleft()
        {
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("moveleft", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], null);

            }
        }
        public void moveright()
        {
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("moveright", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], null);

            }
        }

        public void save()
        {
            shape_save.Clear();
            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                object temp = t.InvokeMember("Clone", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], null);
                shape_save.Add(temp);
            }
 
        }
        public void load()
        {

            for (int i = 0; i < shape.Count(); i++)
            {
                Type t = shape[i].GetType();
                t.InvokeMember("terminate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape[i], new object[] { MainWindow.OpenFrom.maingrid });
                
            }
            shape.Clear();

            for (int i = 0; i < shape_save.Count(); i++)
            {
                Type t = shape_save[i].GetType();
                object temp = t.InvokeMember("Clone", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, shape_save[i], null);
                shape.Add(temp);

            }
            show();
        }

    }

}
