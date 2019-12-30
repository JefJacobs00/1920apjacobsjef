using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globals;

namespace Logic
{
    class EarClipping
    {
        Polygon polygon;
        List<Triangle> triangles;
        public EarClipping(Polygon p)
        {
            triangles = new List<Triangle>();
        }
        //https://www.habrador.com/tutorials/math/10-triangulation/
        public List<Triangle> Triangulate(Polygon p)
        {
            List<Point> points = p.Points;
            List<Point> ears = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                if(i == 0)
                {
                    IsConvex(points[i], points[i + 1], points[points.Count - 1]);
                }
            }
            
        }

        //A vertex is convex when the inside angle is less then 180° (triangle is clockwise)
        private bool IsConvex(Point p, Point nextPoint, Point prevPoint)
        {
            Triangle triangle = new Triangle(prevPoint, p, nextPoint);
            if (triangle.OrientationPoints() == Triangle.Orientation.Clockwise)
            {
                p.Convex = true;
                return true;
            }
            return false;
        }

        private bool IsEar(Point p, Point nextPoint, Point prevPoint, List<Point> points)
        {
            if (!p.Convex)
            {
                return false;
            }
            Triangle triangle = new Triangle(prevPoint, p, nextPoint);
            for (int i = 0; i < points.Count; i++)
            {
                if (triangle.IsInside(points[i]))
                {
                    return false;
                }
            }
            
            return true;
        }


    }
}
