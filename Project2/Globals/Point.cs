﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class Point
    {
        private double x;
        private double y;
        bool convex;

        public bool Convex { get => convex; set => convex = value; }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        public Point(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
