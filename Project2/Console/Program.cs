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

            ReadGeo rg = new ReadGeo();
            List<Province> prov = new List<Province>();
            prov = rg.ReadJson();

            for (int i = 0; i < prov.Count; i++)
            {
                Console.WriteLine(prov[i].ToString());
            }

            Console.Read();
        }
    }
}
