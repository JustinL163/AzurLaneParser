using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace AzurLaneParser
{
    class SkillTemplate
    {

        public static Root Extract(string server)
        {

            string json = "{\"data\":" + File.ReadAllText("data/SkillTemplate" + server + ".json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            json = json.Replace("\"desc_add\": \"[}\",", "\"desc_add\": [],");

            Root SkillTemplates = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted " + server + " skill template");

            return SkillTemplates;
        }

        public static Root2 Extract2(string server)
        {

            string json = "{\"data\":" + File.ReadAllText("data/SkillTemplate" + server + ".json") + "}";

            int startPos = json.IndexOf("\"all\":");
            int endPos = json.IndexOf("\n  ],") + 5;

            json = json.Substring(0, startPos) + json.Substring(endPos);

            json = json.Replace("\"desc_add\": \"[}\",", "\"desc_add\": [],");

            Root2 SkillTemplates = JsonConvert.DeserializeObject<Root2>(json);

            Console.WriteLine("Extracted " + server + " skill template");

            return SkillTemplates;
        }

        public class Root
        {
            public Dictionary<string, SkillTemplates> data { get; set; }
        }

        public class SkillTemplates
        {
            //[JsonProperty("system_transform")]
            //public Dictionary<string, int> SystemTransform { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("desc_add")]
            public List<object> DescAdd { get; set; }

            [JsonProperty("world_death_mark")]
            public List<long> WorldDeathMark { get; set; }

            [JsonProperty("desc")]
            public string Desc { get; set; }

            [JsonProperty("max_level")]
            public long MaxLevel { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }

            [JsonProperty("desc_get_add")]
            public List<List<string>> DescGetAdd { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("desc_get")]
            public string DescGet { get; set; }
        }

        public class Root2
        {
            public Dictionary<string, SkillTemplates2> data { get; set; }
        }

        public class SkillTemplates2
        {
            //[JsonProperty("system_transform")]
            //public Dictionary<string, int> SystemTransform { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("desc_add")]
            public List<object> DescAdd { get; set; }

            [JsonProperty("world_death_mark")]
            public List<long> WorldDeathMark { get; set; }

            [JsonProperty("desc")]
            public string Desc { get; set; }

            [JsonProperty("max_level")]
            public long MaxLevel { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }

            //[JsonProperty("desc_get_add")]
            //public List<List<string>> DescGetAdd { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("desc_get")]
            public string DescGet { get; set; }
        }

    }
}
