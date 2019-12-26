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

        public string test()
        {
            string fileRead;
            //Belgium_AL2 - AL2
            //Flanders_AL4-AL7.Geojson
            //East Flanders_AL6-AL6
            using (StreamReader r = new StreamReader(@"../../../../Data/Resources/Flanders_AL4-AL7.Geojson"))
            {
                fileRead = r.ReadToEnd();

                dynamic deserialized = (JObject) JsonConvert.DeserializeObject(fileRead);

                foreach(var item in deserialized.features)
                {
                    var geo = item["geometry"];
                    var json = JsonConvert.SerializeObject(geo);
                    var multipol = JsonConvert.DeserializeObject<MultiPolygon>(json);
                }
                

                

                return fileRead;
            }
        }
    }

}
