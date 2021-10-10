using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipTechClass
    {
        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipTechClass.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipTechClasses = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship tech class");

            return ShipTechClasses;
        }

        public class Root
        {
            public Dictionary<string, TechClass> data { get; set; }
        }


        public class TechClass
        {
            [JsonProperty("nation")]
            public long Nation { get; set; }

            [JsonProperty("t_level_1")]
            public long TLevel1 { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("ships")]
            public List<long> Ships { get; set; }

            [JsonProperty("t_level")]
            public long TLevel { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("shiptype")]
            public long Shiptype { get; set; }
        }

    }

}
