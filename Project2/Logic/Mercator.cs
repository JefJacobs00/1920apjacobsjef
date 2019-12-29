using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic
{
    public class Mercator
    {
        private double width;
        private double height;

        public Mercator(double width, double height)
        {
            this.height = height;
            this.width = width;
            
        }

        public (double x ,double y) ToPoint(double lat , double lon)
        {
            double latRad = lat * (Math.PI / 180);
            double lonRad = lon * (Math.PI / 180);

            double x = (width / (2 * Math.PI) * Math.Pow(2,6) * (lonRad + Math.PI));

            double y = (height / (2 * Math.PI) * Math.Pow(2,6) * (Math.PI - Math.Log(Math.Tan(Math.PI / 4 + latRad / 2))));

            return (-y , -x);
        }  
    }
}
