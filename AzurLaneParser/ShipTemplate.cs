using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipTemplate
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipTemplate.json") + "}";

            //int startPos = json.IndexOf("\"all\":");
            //int endPos = json.IndexOf("\n  ],") + 5;

            //json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipTemplates = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship template");

            return ShipTemplates;
        }

        public class Root
        {
            public Dictionary<string, Template> data { get; set; }
        }

        public class Template
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("group_type")]
            public long GroupType { get; set; }

            [JsonProperty("equip_id_1")]
            public long EquipId1 { get; set; }

            [JsonProperty("equip_2")]
            public int[] Equip2 { get; set; }

            [JsonProperty("equip_id_2")]
            public long EquipId2 { get; set; }

            [JsonProperty("equip_1")]
            public int[] Equip1 { get; set; }

            [JsonProperty("strengthen_id")]
            public long StrengthenId { get; set; }

            [JsonProperty("star_max")]
            public long StarMax { get; set; }

            [JsonProperty("buff_list")]
            public List<int> BuffList { get; set; }

            [JsonProperty("energy")]
            public long Energy { get; set; }

            [JsonProperty("star")]
            public long Star { get; set; }

            [JsonProperty("can_get_proficency")]
            public long CanGetProficency { get; set; }

            [JsonProperty("hide_buff_list")]
            public List<long> HideBuffList { get; set; }

            [JsonProperty("max_level")]
            public long MaxLevel { get; set; }

            [JsonProperty("equip_3")]
            public int[] Equip3 { get; set; }

            [JsonProperty("specific_type")]
            public List<string> SpecificType { get; set; }

            [JsonProperty("equip_5")]
            public int[] Equip5 { get; set; }

            [JsonProperty("equip_id_3")]
            public long EquipId3 { get; set; }

            [JsonProperty("equip_4")]
            public int[] Equip4 { get; set; }

            [JsonProperty("oil_at_end")]
            public long OilAtEnd { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("oil_at_start")]
            public long OilAtStart { get; set; }

            [JsonProperty("airassist_time")]
            public List<long> AirassistTime { get; set; }

            [JsonProperty("buff_list_display")]
            public List<long> BuffListDisplay { get; set; }
        }
    }
}
