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

        public Triangle(List<Point> points)
        {
            if (points.Count != 3) throw new ArgumentException($"A triangle cant have {points.Count} points");
            this.points = points;
        }
    }
}
