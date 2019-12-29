using System;
using System.Collections.Generic;
using Data;
using Globals;

namespace PresentatieConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Point> points = new List<Point>();
            points.Add(new Point(1, 4));
            points.Add(new Point(2, 10));
            points.Add(new Point(4, 4));
            points.Add(new Point(4, 0));
            points.Add(new Point(1, 0));
            points.Add(new Point(0, 2));

            Polygon p = new Polygon(points);
            bool b = p.IsInside(new Point(2,2));
            bool b2 = p.IsInside(new Point(3, 2));
            bool b3 = p.IsInside(new Point(5, 2));
            bool b4 = p.IsInside(new Point(2, 5));
            bool b5 = p.IsInside(new Point(1, 10));
            bool b6 = p.IsInside(new Point(-1, 2));

            if (b)
            {
                
            }
            //ReadGeo rg = new ReadGeo();
            //List<Province> prov = new List<Province>();
            //prov =  rg.ReadJson();

            //for (int i = 0; i < prov.Count; i++)
            //{
            //    Console.WriteLine(prov[i].ToString());
            //}
            
            //Console.Read();
        }
    }
}
