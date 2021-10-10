using System;
using System.Collections.Generic;
using System.Text;

namespace AzurLaneParser
{
    class PropertyName
    {
        static Dictionary<string, Dictionary<int, string>> Properties = new Dictionary<string, Dictionary<int, string>>();
        static Dictionary<string, Dictionary<int[], string>> PropertiesArray = new Dictionary<string, Dictionary<int[], string>>();
        static Dictionary<string, Dictionary<string, string>> PropertiesString = new Dictionary<string, Dictionary<string, string>>();
        static Dictionary<string, Dictionary<string, int>> PropertiesInt = new Dictionary<string, Dictionary<string, int>>();

        public static void Init()
        {
            // Properties
            Properties["Rarity"] = new Dictionary<int, string>
            {
                [2] = "Common",
                [3] = "Rare",
                [4] = "Elite",
                [5] = "Super Rare",
                [6] = "Ultra Rare"
            };

            Properties["Nationality"] = new Dictionary<int, string>
            {
                [1] = "Eagle Union",
                [2] = "Royal Navy",
                [3] = "Sakura Empire",
                [4] = "Iron Blood",
                [5] = "Dragon Empery",
                [6] = "Sardegna Empire",
                [7] = "Northern Parliament",
                [8] = "Iris Libre",
                [9] = "Vichya Dominion",
                [97] = "META",
                [98] = "Universal",
                [101] = "Neptunia",
                [102] = "Bilibili",
                [103] = "Utawarerumono",
                [104] = "KizunaAI",
                [105] = "Hololive",
                [106] = "Venus Vacation",
                [107] = "The Idolmaster"
            };

            Properties["Type"] = new Dictionary<int, string>
            {
                [1] = "Destroyer",
                [2] = "Light Cruiser",
                [3] = "Heavy Cruiser",
                [4] = "Battlecruiser",
                [5] = "Battleship",
                [6] = "Light Aircraft Carrier",
                [7] = "Aircraft Carrier",
                [8] = "Submarine",
                [10] = "Aviation Battleship",
                [12] = "Repair Ship",
                [13] = "Monitor",
                [17] = "Submarine Carrier",
                [18] = "Large Cruiser",
                [19] = "Munition Ship"
            };

            Properties["Armor"] = new Dictionary<int, string>
            {
                [1] = "Light",
                [2] = "Medium",
                [3] = "Heavy"
            };

            Properties["Bonus"] = new Dictionary<int, string>
            {
                [1] = "Health",
                [2] = "Firepower",
                [3] = "Torpedo",
                [4] = "AA",
                [5] = "Aviation",
                [6] = "Reload",
                [8] = "Accuracy",
                [9] = "Evasion",
                [12] = "ASW"
            };

            Properties["SkillType"] = new Dictionary<int, string>
            {
                [1] = "Offense",
                [2] = "Defense",
                [3] = "Support"
            };

            // PropertiesArray
            PropertiesArray["EquipType"] = new Dictionary<int[], string>(new IntArrayEqualityComparer())
            {
                [new int[] { 1 }] = "DD Guns", // Fix for langley
                [new int[] { 1, 2 }] = "CL/DD Guns", // Doesn't work for Isuzu / or Fusou etc
                [new int[] { 1, 5 }] = "Torpedoes/DD Guns", //Need retrofit for london (no other ships use)
                [new int[] { 2 }] = "CL Guns", // Fix for ships that change after retro e.g Pamiat/Mogami
                [new int[] { 2, 3 }] = "CA/CL Guns",
                [new int[] { 2, 6 }] = "CL Guns/Anti-Air Guns",
                [new int[] { 2, 9 }] = "CL Guns/Dive Bombers",
                [new int[] { 3 }] = "CA Guns", // Fix for Surcouf
                [new int[] { 3, 11 }] = "CA/CB Guns",
                [new int[] { 4 }] = "BB Guns",
                [new int[] { 5 }] = "Torpedoes",
                [new int[] { 6 }] = "Anti-Air Guns",
                [new int[] { 7 }] = "Fighters", // /Dive Bombers/Torp Bombers for Shinano
                [new int[] { 7, 9 }] = "Fighters/Dive Bombers", // MLB for Formi  (no other ships use this)
                [new int[] { 8 }] = "Torpedo Bombers",
                [new int[] { 9 }] = "Dive Bombers",  // /Torpedo Bombers for Graf Zepp
                [new int[] { 10 }] = "Auxiliaries",
                [new int[] { 10, 18 }] = "Auxiliaries/Cargo",
                [new int[] { 12 }] = "Seaplanes",
                [new int[] { 13 }] = "Submarine Torpedoes",
                // Consider going back to ints only and parsing through the list one at a time to construct string
                // Joined ones are joined in alphabetical order (other than F/D and CL/AA)
                // Torp/DD is with DD being retro


            }; // Needs way more

            // PropertiesString
            PropertiesString["UltimateBonus"] = new Dictionary<string, string>
            {
                ["TORP"] = " / Decreased torpedo spread angle",
                ["AUX"] = " / +30% stats gained from auxiliary gear",
                ["GNR"] = " / Hits needed to activate All-Out Assault halved"
            };

            PropertiesString["ProjType"] = new Dictionary<string, string>
            {
                ["hp_1"] = "Defense",
                ["hp_2"] = "Defense",
                ["hp_3"] = "Defense",
                ["tp_1"] = "Offense",
                ["tp_2"] = "Offense",
                ["tp_3"] = "Offense",
                ["shell_1"] = "Offense",
                ["shell_2"] = "Offense",
                ["shell_3"] = "Offense",
                ["air_1"] = "Offense",
                ["air_2"] = "Offense",
                ["air_3"] = "Offense",
                ["dd_1"] = "Support",
                ["dd_2"] = "Support",
                ["dd_3"] = "Support",
                ["aa_1"] = "Defense",
                ["aa_2"] = "Defense",
                ["aa_3"] = "Defense",
                ["as_1"] = "Defense",
                ["as_2"] = "Defense",
                ["as_3"] = "Defense",
                ["rl_1"] = "Support",
                ["rl_2"] = "Support",
                ["rl_3"] = "Support",
                ["sp_1"] = "Support",
                ["sp_2"] = "Support",
                ["sp_3"] = "Support",
                ["hit_1"] = "Support",
                ["hit_2"] = "Support",
                ["hit_3"] = "Support",
                ["bfup_1"] = "Offense",
                ["bfup_2"] = "Offense",
                ["tfup_1"] = "Offense",
                ["tfup_2"] = "Offense",
                ["mgup_1"] = "Offense",
                ["mgup_2"] = "Offense",
                ["sgup_1"] = "Offense",
                ["sgup_2"] = "Offense",
                ["tpup_1"] = "Offense",
                ["tpup_2"] = "Offense",
                ["aaup_1"] = "Defense",
                ["aaup_2"] = "Defense",
                ["ffup_1"] = "Defense",
                ["ffup_2"] = "Defense",
                ["mt_blue"] = "Defense",
                ["mt_red"] = "Offense",
                ["mt_yellow"] = "Support",
                ["skill_red"] = "Offense",
                ["skill_yellow"] = "Support",
                ["skill_blue"] = "Defense",
                

            };

            PropertiesString["Attribute"] = new Dictionary<string, string>
            {
                ["durability"] = "Health",
                ["cannon"] = "Firepower",
                ["torpedo"] = "Torpedo",
                ["antiaircraft"] = "AA",
                ["air"] = "Aviation",
                ["reload"] = "Reload",
                ["hit"] = "Accuracy",
                ["dodge"] = "Evasion",
                ["speed"] = "Speed",
                ["luck"] = "Luck",
                ["antisub"] = "ASW"

            };

            PropertiesString["PlateType"] = new Dictionary<string, string>
            {
                ["Main Gun"] = "Gun",
                ["General"] = "Aux",
                ["Anti-Air Gun"] = "AA",
                ["Torpedo"] = "Torp",
                ["Aircraft"] = "Plane"
            };

            // PropertiesInt
            PropertiesInt["Stat"] = new Dictionary<string, int>
            {
                ["durability"] = 0,
                ["cannon"] = 1,
                ["torpedo"] = 2,
                ["antiaircraft"] = 3,
                ["air"] = 4,
                ["reload"] = 5,
                ["hit"] = 7,
                ["dodge"] = 8,
                ["speed"] = 9,
                ["luck"] = 10,
                ["antisub"] = 11
            };
        }

        public static string C(string PropertyType, int value)
        {
            return Properties[PropertyType][value];
        }

        public static string C(string PropertyType, int[] value)
        {
            return PropertiesArray[PropertyType][value];
        }

        public static string C(string PropertyType, string value)
        {
            return PropertiesString[PropertyType][value];
        }

        public static int C(string PropertyType, string value, bool nothing)
        {
            return PropertiesInt[PropertyType][value];
        }


        public class IntArrayEqualityComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(int[] obj)
            {
                int result = 17;
                for (int i = 0; i < obj.Length; i++)
                {
                    unchecked
                    {
                        result = result * 23 + obj[i];
                    }
                }
                return result;
            }
        }


    }
}
