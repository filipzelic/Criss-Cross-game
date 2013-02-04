using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KrizicKruzic
{
    struct Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x;
        private int y;

        public int X {
            get { return x; }
            set { x = value; }
        }


        //public int Y { get; set; }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

    }
}
