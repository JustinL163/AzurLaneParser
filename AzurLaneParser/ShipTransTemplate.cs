using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipTransTemplate
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipTransTemplate.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipTransTemplates = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship transform templates");

            return ShipTransTemplates;
        }

        public class Root
        {
            public Dictionary<string, TransTemplate> data { get; set; }

        }

        public class TransTemplate
        {

            [JsonProperty("gear_score")]
            public List<long> GearScore { get; set; }

            [JsonProperty("skill_id")]
            public int SkillId { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("descrip")]
            public string Descrip { get; set; }

            [JsonProperty("condition_id")]
            public List<int> ConditionId { get; set; }

            [JsonProperty("effect")]
            public List<Dictionary<string, double>> Effect { get; set; }

            [JsonProperty("ship_id")]
            public List<List<int>> ShipId { get; set; }

            [JsonProperty("use_item")]
            public List<List<List<int>>> UseItem { get; set; }

            [JsonProperty("star_limit")]
            public long StarLimit { get; set; }

            [JsonProperty("use_ship")]
            public int UseShip { get; set; }

            [JsonProperty("skin_id")]
            public long SkinId { get; set; }

            [JsonProperty("max_level")]
            public long MaxLevel { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("level_limit")]
            public long LevelLimit { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("use_gold")]
            public long UseGold { get; set; }

        }

    }
}
