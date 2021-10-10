using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipGroup
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipGroup.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipGroup = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship group");

            return ShipGroup;
        }

        public class Root
        {
            public Dictionary<string, Group> data { get; set; }
        }

        public class Group
        {

            [JsonProperty("trans_skill")]
            public List<object> TransSkill { get; set; }

            [JsonProperty("redirect_id")]
            public long RedirectId { get; set; }

            [JsonProperty("group_type")]
            public long GroupType { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("trans_radar_chart")]
            public List<object> TransRadarChart { get; set; }

            [JsonProperty("property_hexagon")]
            public List<string> PropertyHexagon { get; set; }

            [JsonProperty("trans_type")]
            public long TransType { get; set; }

            [JsonProperty("code")]
            public long Code { get; set; }

            [JsonProperty("handbook_type")]
            public long HandbookType { get; set; }

            [JsonProperty("index_id")]
            public long IndexId { get; set; }

            [JsonProperty("trans_skin")]
            public long TransSkin { get; set; }

            [JsonProperty("hide")]
            public long Hide { get; set; }

            [JsonProperty("nationality")]
            public long Nationality { get; set; }

            //[JsonProperty("description")]
            //public List<List<The1_Description>> Description { get; set; }

        }

    }
}
