using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipTech
    {
        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipTech.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipTechs = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship tech");

            return ShipTechs;
        }

        public class Root
        {
            public Dictionary<string, TechTemplate> data { get; set; }
        }

        public class TechTemplate
        {
            [JsonProperty("pt_get")]
            public int PtGet { get; set; }

            [JsonProperty("add_get_value")]
            public int AddGetValue { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("class")]
            public long Class { get; set; }

            [JsonProperty("add_level_value")]
            public int AddLevelValue { get; set; }

            [JsonProperty("add_get_attr")]
            public int AddGetAttr { get; set; }

            [JsonProperty("pt_upgrage")]
            public int PtUpgrage { get; set; }

            [JsonProperty("add_level_shiptype")]
            public List<long> AddLevelShiptype { get; set; }

            [JsonProperty("add_get_shiptype")]
            public List<long> AddGetShiptype { get; set; }

            [JsonProperty("add_level_attr")]
            public int AddLevelAttr { get; set; }

            [JsonProperty("pt_level")]
            public int PtLevel { get; set; }

            [JsonProperty("max_star")]
            public long MaxStar { get; set; }
        }


    }

}
