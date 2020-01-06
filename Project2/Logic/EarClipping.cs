using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globals;

namespace Logic
{
    public class EarClipping
    {
        Polygon polygon;
        List<Triangle> triangles;
        public EarClipping()
        {
            triangles = new List<Triangle>();
        }
        //https://www.habrador.com/tutorials/math/10-triangulation/
        public List<Triangle> Triangulate(Polygon p)
        {
            List<Point> points = p.Points;

            points = GetAllConvex(points);



            do
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Convex)
                    {
                        if (i == 0)
                        {
                            if (IsEar(points[i], points[i + 1], points[points.Count - 1], points))
                            {
                                triangles.Add(new Triangle(points[i], points[i + 1], points[points.Count - 1]));
                                points.RemoveAt(i);
                                break;
                            }
                        }
                        else if (i == points.Count - 1)
                        {
                            if (IsEar(points[i], points[0], points[i - 1], points))
                            {
                                triangles.Add(new Triangle(points[i], points[0], points[i - 1]));
                                points.RemoveAt(i);
                                break;
                            }
                        }
                        else
                        {
                            if (IsEar(points[i], points[i + 1], points[i - 1], points))
                            {
                                triangles.Add(new Triangle(points[i], points[i + 1], points[i - 1]));
                                points.RemoveAt(i);
                                break;
                            }
                        }
                        
                    }
                }
            } while ((points.Count > 3) && (IsConvexLeft(points)));

            if(points.Count == 3)
            {
                triangles.Add(new Triangle(points[0], points[1], points[2]));
            }

            return triangles;
            
        }

        private List<Point> GetAllConvex(List<Point> points)
        {
            
            for (int i = 0; i < points.Count; i++)
            {
                if (points.Count < 3)
                {
                    break;
                }
                if (i == 0)
                {
                    IsConvex(points[i], points[i + 1], points[points.Count - 1]);
                }
                else if (i == points.Count - 1)
                {
                    IsConvex(points[i], points[0], points[i - 1]);
                }
                else
                {
                    IsConvex(points[i], points[i + 1], points[i - 1]);
                }
            }

            return points;
        }

        private bool IsConvexLeft(List<Point> points)
        {
            points = GetAllConvex(points);

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Convex)
                {
                    return true;
                }
            }
            return false;
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
