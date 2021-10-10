using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ItemStatistics
    {
        public static Root Extract()
        {

            string json = "{\"data\":" + File.ReadAllText("data/ItemStats.json") + "}";

            //int startPos = json.IndexOf("\"all\":");
            //int endPos = json.IndexOf("\n  ],") + 5;

            //json = json.Substring(0, startPos) + json.Substring(endPos);

            Root ItemStats = JsonConvert.DeserializeObject<Root>(json);

            Console.WriteLine("Extracted item statistics");

            return ItemStats;
        }

        public class Root
        {
            public Dictionary<string, Item> data { get; set; }

        }

        public class Item
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("virtual_type")]
            public long VirtualType { get; set; }

            [JsonProperty("shiptrans_id")]
            public long ShiptransId { get; set; }

            [JsonProperty("drop_oil_max")]
            public long DropOilMax { get; set; }

            [JsonProperty("display_icon")]
            public List<object> DisplayIcon { get; set; }

            [JsonProperty("max_num")]
            public long MaxNum { get; set; }

            [JsonProperty("rarity")]
            public long Rarity { get; set; }

            [JsonProperty("price")]
            public List<object> Price { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("link_id")]
            public long LinkId { get; set; }

            [JsonProperty("replace_item")]
            public long ReplaceItem { get; set; }

            [JsonProperty("shop_id")]
            public long ShopId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("index")]
            public List<object> Index { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("drop_gold_max")]
            public long DropGoldMax { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }

            [JsonProperty("is_world")]
            public long IsWorld { get; set; }
        }
    }
}
