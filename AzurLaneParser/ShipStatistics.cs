using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzurLaneParser
{
    class ShipStatistics
    {
        public static Root Extract(string server)
        {

            string shipStats = "{\"data\":" + File.ReadAllText("data/ShipStatistics" + server + ".json") + "}";

            int startPos = shipStats.IndexOf("\"all\":");
            int endPos = shipStats.IndexOf("\n  ],") + 5;

            shipStats = shipStats.Substring(0, startPos) + shipStats.Substring(endPos);

            Root ShipData = JsonConvert.DeserializeObject<Root>(shipStats);

            Console.WriteLine("Extracted " + server + " ship statistics");

            return ShipData;
        }

        public class ShipData
        {
            public List<int> attrs_growth_extra { get; set; }
            public List<int> depth_charge_list { get; set; }
            //public List<int> cld_box { get; set; }
            public int armor_type { get; set; }
            public int oxy_recovery_bench { get; set; }
            public List<int> default_equip_list { get; set; }
            public List<int> base_list { get; set; }
            public List<string> @lock { get; set; }
            public int attack_duration { get; set; }
            public List<int> fix_equip_list { get; set; }
            public int skin_id { get; set; }
            public int oxy_cost { get; set; }
            public List<double> equipment_proficiency { get; set; }
            public List<List<object>> hunting_range { get; set; }
            public string english_name { get; set; }
            public int raid_distance { get; set; }
            public int scale { get; set; }
            public int summon_offset { get; set; }
            public int id { get; set; }
            public int huntingrange_level { get; set; }
            public List<int> position_offset { get; set; }
            public int ammo { get; set; }
            public int rarity { get; set; }
            public List<double> attrs_growth { get; set; }
            public int oxy_recovery { get; set; }
            public int star { get; set; }
            public List<int> aim_offset { get; set; }
            public List<int> preload_count { get; set; }
            public List<int> parallel_max { get; set; }
            //public List<int> cld_offset { get; set; }
            public List<double> attrs { get; set; }
            public List<object> strategy_list_ai { get; set; }
            public List<object> strategy_list { get; set; }
            public int oxy_max { get; set; }
            public string backyard_speed { get; set; }
            public int type { get; set; }
            public List<string> tag_list { get; set; }
            public string name { get; set; }
            public int nationality { get; set; }
        }

        public class Root
        {
            public Dictionary<string, ShipData> data { get; set; }
        }

    }
}
