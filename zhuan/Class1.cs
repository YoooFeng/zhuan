using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zhuan
{
    class ME
    {
        private int x = 0, y = 0;
        public int agl = 0;
        public ME()
        {
            Thread t = new Thread(zhuan);
            t.Start();
        }

        public void zhuan()
        {
            while (true)
            {
                AglChange();
                if (agl == 360) agl = 0;
                Thread.Sleep(20);
                agl++;
            }
        }

        public delegate void AGLchangeEventHander();
        //在委托的机制下我们建立一个事件
        public event AGLchangeEventHander AglChange;
        //事件必须要在方法里去触发，提供Main函数AGL改变的提醒

        public void move(int i)
        {
            switch (i)
            {
                case 1:
                    x++;
                    break;
            }

        }
    }
}
