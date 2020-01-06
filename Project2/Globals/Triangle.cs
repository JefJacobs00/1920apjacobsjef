using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{

    public class Triangle
    {
        private List<Point> points;

        public List<Point> Points { get => points; }

        public Triangle(List<Point> points)
        {
            if (points.Count != 3) throw new ArgumentException($"A triangle cant have {points.Count} points");
            this.points = points;
        }

        public Triangle(Point a, Point b, Point c)
        {
            points = new List<Point>();

            points.Add(a);
            points.Add(b);
            points.Add(c);
        }
        ////https://www.gamedev.net/forums/?topic_id=295943
        public enum Orientation { Clockwise, CounterClockwise, Collinear }
        public Orientation OrientationPoints()
        {
            Point p = points[0];
            Point q = points[1];
            Point r = points[2];
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return Orientation.Collinear; // colinear 
            }
            return (val > 0) ? Orientation.Clockwise : Orientation.CounterClockwise; // clock or counterclock wise 
        }

        private double Sign(Point p1,Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
        public bool IsInside(Point p)
        {
            bool b1, b2, b3;

            b1 = Sign(p, points[0], points[1]) < 0.0;
            b2 = Sign(p, points[1], points[2]) < 0.0;
            b3 = Sign(p, points[2], points[0]) < 0.0;

            return ((b1 == b2) && (b2 == b3));

        }


    }
}
