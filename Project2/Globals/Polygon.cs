using System;
using System.Collections.Generic;

namespace Globals
{
    
    public class Polygon
    {
        
        private List<Point> points;

        public List<Point> Points { get => points; }

        public Polygon(List<Point> points)
        {
            this.points = points;
        }

        //https://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/

        private bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        enum Orientation { Clockwise, CounterClockwise, Collinear }
        private Orientation OrientationPoints(Point p, Point q, Point r)
        {
            double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return Orientation.Collinear; // colinear 
            }
            return (val > 0) ? Orientation.Clockwise : Orientation.CounterClockwise; // clock or counterclock wise 
        }


        // The function that returns true if  
        // line segment 'p1q1' and 'p2q2' intersect. 
        private bool DoIntersect(Point p1, Point q1,
                                Point p2, Point q2)
        {
            // Find the four orientations needed for  
            // general and special cases 
            Orientation o1 = OrientationPoints(p1, q1, p2);
            Orientation o2 = OrientationPoints(p1, q1, q2);
            Orientation o3 = OrientationPoints(p2, q2, p1);
            Orientation o4 = OrientationPoints(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases 
            // p1, q1 and p2 are colinear and 
            // p2 lies on segment p1q1 
            if (o1 == Orientation.Collinear && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are colinear and 
            // q2 lies on segment p1q1 
            if (o2 == Orientation.Collinear && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are colinear and 
            // p1 lies on segment p2q2 
            if (o3 == Orientation.Collinear && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are colinear and 
            // q1 lies on segment p2q2 
            if (o4 == Orientation.Collinear && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases 
            return false;
        }

        // Returns true if the point p lies  
        // inside the polygon[] with n vertices 
        public bool IsInside(Point p)
        {

            double max = GetMaxX() + 1;

            if (points.Count < 3)
            {
                return false;
            }

            Point extreme = new Point(max, p.Y);

            int count = 0;
            for (int i = 0; i < points.Count; i++)
            {
                try
                {
                    if (DoIntersect(points[i], points[i + 1], p, extreme))
                    {

                        if (OrientationPoints(points[i], p, points[i + 1]) == 0)
                        {
                            return onSegment(points[i], p,
                                             points[i + 1]);
                        }
                        count++;
                    }
                }
                catch (Exception)
                {
                    if (DoIntersect(points[i], points[0], p, extreme))
                    {

                        if (OrientationPoints(points[i], p, points[0]) == 0)
                        {
                            return onSegment(points[i], p,
                                             points[0]);
                        }
                        count++;
                    }
                }
                

                
            }

            return (count % 2 == 1); // Same as (count%2 == 1) 
        }



        private double GetMaxX()
        {
            double max = double.MinValue;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X > max)
                {
                    max = points[i].X;
                }
            }

            return max;
        }


    }
}
