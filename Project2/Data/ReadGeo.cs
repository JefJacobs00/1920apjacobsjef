using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data
{
    public class ReadGeo
    {
        string json;

        public ReadGeo()
        {
            
        }

        public string test()
        {
            string fileRead;
            using (StreamReader r = new StreamReader(@"../../../../Data/Resources/Flanders_AL4-AL7.Geojson"))
            {
                fileRead = r.ReadToEnd();

                dynamic deserialized = JsonConvert.DeserializeObject(fileRead);
                List<string> names = new List<string>();
                List<string> cords = new List<string>();
                foreach (var item in deserialized.features)
                {
                    names.Add(""+item["properties"]["name"]);
                    cords.Add(""+item["geometry"]["coordinates"]);

                }
                for (int i = 0; i < cords.Count; i++)
                {
                    do
                    {
                        cords[i].Replace("\r", "");
                    } while (cords[i].Contains("\r"));
                    
                    cords[i].Replace("\n", "");
                    cords[i].Replace(" ", "");
                    string[] cordsArr = cords[i].Split(',');
                }
                return fileRead;
            }
        }
    }
}
