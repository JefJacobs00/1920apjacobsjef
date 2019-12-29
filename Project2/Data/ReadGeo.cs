using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Globals;

namespace Data
{
    public class ReadGeo
    {
        string json;

        public ReadGeo()
        {
            
        }

        public List<Province> ReadJson()
        {
            string fileRead;


            try
            {
                using (StreamReader r = new StreamReader(@"../../../Data/Resources/Antwerp_AL6-AL7.Geojson"))
                {
                    fileRead = r.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }

            dynamic deserialized = (JObject)JsonConvert.DeserializeObject(fileRead);

            List<Province> prov = new List<Province>();

            foreach (var item in deserialized.features)
            {
                var geo = item["geometry"];
                string name = item["properties"]["name"];

                var json = JsonConvert.SerializeObject(geo);
                var multiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(json);


                prov.Add(new Province(name, multiPolygon));
            }

            return prov;
        }
    }

}
