using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class Buff
    {
        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/buffCfg.json") + "}";

            //int startPos = json.IndexOf("\"all\":");
            //int endPos = json.IndexOf("\n  ],") + 5;

            //json = json.Substring(0, startPos) + json.Substring(endPos);

            Root Buffs = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted buffs");

            return Buffs;
        }

        public class Root
        {
            public Dictionary<string, BuffCfg> data { get; set; }
        }

        public class BuffCfg
        {

            //[JsonProperty("last_effect")]
            //public string LastEffect { get; set; }

            [JsonProperty("icon")]
            public int Icon { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }

            [JsonProperty("stack")]
            public long Stack { get; set; }

            [JsonProperty("desc")]
            public string Desc { get; set; }

            //[JsonProperty("effect_list")]
            //public List<EffectList> EffectList { get; set; }

            [JsonProperty("picture")]
            public string Picture { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("init_effect")]
            public string InitEffect { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("time")]
            public long Time { get; set; }

        }

        public partial class EffectList
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("trigger")]
            public List<string> Trigger { get; set; }

            [JsonProperty("arg_list")]
            public ArgList ArgList { get; set; }
        }

        public partial class ArgList
        {
            [JsonProperty("skill_id")]
            public long SkillId { get; set; }

            //[JsonProperty("target")]
            //public string Target { get; set; }

            [JsonProperty("countTarget")]
            public int CountTarget { get; set; }

            [JsonProperty("rant")]
            public long Rant { get; set; }
        }
    }

}
