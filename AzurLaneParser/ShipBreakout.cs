using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipBreakout
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipBreakout.json") + "}";

            //int startPos = json.IndexOf("\"all\":");
            //int endPos = json.IndexOf("\n  ],") + 5;

            //json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipBreakout = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship breakout");

            return ShipBreakout;
        }

        public class Root
        {
            public Dictionary<string, Breakout> data { get; set; }
        }

        public class Breakout
        {
            [JsonProperty("level")]
            public long Level { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("use_item")]
            public List<object> UseItem { get; set; }

            [JsonProperty("breakout_id")]
            public long BreakoutId { get; set; }

            [JsonProperty("weapon_ids")]
            public List<long> WeaponIds { get; set; }

            [JsonProperty("pre_id")]
            public long PreId { get; set; }

            [JsonProperty("ultimate_bonus")]
            public List<string> UltimateBonus { get; set; }

            [JsonProperty("use_char")]
            public long UseChar { get; set; }

            [JsonProperty("use_char_num")]
            public long UseCharNum { get; set; }

            [JsonProperty("breakout_view")]
            public string BreakoutView { get; set; }

            [JsonProperty("use_gold")]
            public long UseGold { get; set; }
        }

    }
}
