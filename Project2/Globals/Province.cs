using GeoJSON.Net.Geometry;
using System.Collections.Generic;

namespace Globals
{
    public class Province
    {
        private string name;
        private MultiPolygon multiPolygon;

        private List<GeoJSON.Net.Geometry.Polygon> polygons = new List<GeoJSON.Net.Geometry.Polygon>();

        public MultiPolygon MultiPolygon { get => multiPolygon; }
        public List<GeoJSON.Net.Geometry.Polygon> Polygons { get => polygons; }
        public string Name { get => name; }

        public Province(string name , MultiPolygon multiPolygon)
        {
            this.name = name;
            this.multiPolygon = multiPolygon;

            foreach (var item in multiPolygon.Coordinates)
            {
                polygons.Add(item);
            }
        }

        public override string ToString()
        {
            string returnString = $"{name}\nMultiPolygon:\n";
            for (int i = 0; i < polygons.Count; i++)
            {
                returnString += $"\tType: Polygon, Size: {polygons[i].Coordinates.Count}";

                returnString += "\n\tLineStrings: \n";
                for (int j = 0; j < polygons[i].Coordinates.Count; j++)
                {
                    returnString += $"\t\tSize: {polygons[i].Coordinates[j].Coordinates.Count} Points\n";
                }
            }
            return returnString;
        }
    }
}
