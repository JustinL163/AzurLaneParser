using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class NameCode
    {
        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/NameCode.json") + "}";

            //int startPos = json.IndexOf("\"all\":");
            //int endPos = json.IndexOf("\n  ],") + 5;

            //json = json.Substring(0, startPos) + json.Substring(endPos);

            Root NameCodes = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted namecodes");

            return NameCodes;
        }

        public class Root
        {
            public Dictionary<string, Name_Code> data { get; set; }
        }

        public class Name_Code
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}
