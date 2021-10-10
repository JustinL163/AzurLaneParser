using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipStrengthen
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipStrengthen.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipStrengths = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship strengthen");

            return ShipStrengths;
        }

        public class Root
        {
            public Dictionary<string, ShipStrength> data { get; set; }

        }

        public class ShipStrength
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("level_exp")]
            public List<long> LevelExp { get; set; }

            [JsonProperty("attr_exp")]
            public List<int> AttrExp { get; set; }

            [JsonProperty("durability")]
            public List<double> Durability { get; set; }

        }

    }
}
