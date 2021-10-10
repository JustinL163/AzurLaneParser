using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipTransform
    {

        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ShipTransform.json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ShipTransforms = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted ship transforms");

            return ShipTransforms;
        }

        public class Root
        {
            public Dictionary<string, Transform> data { get; set; }

        }

        public class Transform
        {

            [JsonProperty("transform_list")]
            public List<List<List<int>>> TransformList { get; set; }

            [JsonProperty("skill_id")]
            public long SkillId { get; set; }

            [JsonProperty("skin_id")]
            public long SkinId { get; set; }

            [JsonProperty("group_id")]
            public long GroupId { get; set; }

        }

    }
}
