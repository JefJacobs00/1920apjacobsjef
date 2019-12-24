using System;
using Data;

namespace PresentatieConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadGeo rg = new ReadGeo();

            Console.Write(rg.test());
            Console.Read();
        }
    }
}
