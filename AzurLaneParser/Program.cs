using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AzurLaneParser
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Download/Update JSON files? (y/n)");
            string input = Console.ReadLine();
            if (input == "y")
            {
                string urlEN = "https://raw.githubusercontent.com/AzurLaneTools/AzurLaneData/main/EN/";
                string urlCN = "https://raw.githubusercontent.com/AzurLaneTools/AzurLaneData/main/CN/";
                string urlJP = "https://raw.githubusercontent.com/AzurLaneTools/AzurLaneData/main/JP/";

                if (!Directory.Exists("data"))
                {
                    Directory.CreateDirectory("data");
                }

                Console.WriteLine("Downloading JSON files");

                File.WriteAllText("data/ShipStatisticsEN.json", RetrieveFromLink(urlEN + "ShareCfg/ship_data_statistics.json"));
                File.WriteAllText("data/ShipStrengthen.json", RetrieveFromLink(urlEN + "ShareCfg/ship_data_strengthen.json"));
                File.WriteAllText("data/ShipTech.json", RetrieveFromLink(urlEN + "ShareCfg/fleet_tech_ship_template.json"));
                File.WriteAllText("data/ShipTechClass.json", RetrieveFromLink(urlEN + "ShareCfg/fleet_tech_ship_class.json"));
                File.WriteAllText("data/ShipTemplate.json", RetrieveFromLink(urlEN + "sharecfgdata/ship_data_template.json"));
                File.WriteAllText("data/ShipBreakout.json", RetrieveFromLink(urlEN + "sharecfgdata/ship_data_breakout.json"));
                File.WriteAllText("data/ShipGroup.json", RetrieveFromLink(urlEN + "ShareCfg/ship_data_group.json"));
                File.WriteAllText("data/SkillTemplateEN.json", RetrieveFromLink(urlEN + "ShareCfg/skill_data_template.json"));
                File.WriteAllText("data/SkillTemplateCN.json", RetrieveFromLink(urlCN + "ShareCfg/skill_data_template.json"));
                File.WriteAllText("data/SkillTemplateJP.json", RetrieveFromLink(urlJP + "ShareCfg/skill_data_template.json"));
                File.WriteAllText("data/ShipStatisticsCN.json", RetrieveFromLink(urlCN + "ShareCfg/ship_data_statistics.json"));
                File.WriteAllText("data/ShipStatisticsJP.json", RetrieveFromLink(urlJP + "ShareCfg/ship_data_statistics.json"));
                File.WriteAllText("data/ShipTransform.json", RetrieveFromLink(urlEN + "ShareCfg/ship_data_trans.json"));
                File.WriteAllText("data/ShipTransTemplate.json", RetrieveFromLink(urlEN + "ShareCfg/transform_data_template.json"));
                File.WriteAllText("data/NameCode.json", RetrieveFromLink(urlCN + "ShareCfg/name_code.json"));
                File.WriteAllText("data/buffCfg.json", RetrieveFromLink(urlEN + "buffCfg.json"));
                File.WriteAllText("data/ItemStats.json", RetrieveFromLink(urlEN + "sharecfgdata/item_data_statistics.json"));

                Console.WriteLine("Successfully downloaded JSON files");
            }
            else
            {
                Console.WriteLine("Download skipped");
            }

            string[] requiredFiles = {"ShipStatisticsEN.json", "ShipStrengthen.json", "ShipTech.json", "ShipTechClass.json", "ShipTemplate.json", "ShipBreakout.json",
                "ShipGroup.json", "SkillTemplateEN.json", "SkillTemplateCN.json", "SkillTemplateJP.json", "ShipTransform.json", "ShipTransTemplate.json", "NameCode.json",
                "buffCfg.json", "ItemStats.json"};
            
            foreach (string file in requiredFiles)
            {
                if (!File.Exists("data/" + file))
                {
                    Console.WriteLine("\n" + file + " is missing from /data folder, quitting");
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("\nExtracting data");

            ShipStatistics.Root StatsEN = ShipStatistics.Extract("EN");
            ShipStatistics.Root StatsCN = ShipStatistics.Extract("CN");
            ShipStatistics.Root StatsJP = ShipStatistics.Extract("JP");
            ShipStrengthen.Root Strength = ShipStrengthen.Extract();
            ShipTemplate.Root ShipTemp = ShipTemplate.Extract();
            ShipTech.Root Tech = ShipTech.Extract();
            ShipTechClass.Root TechClass = ShipTechClass.Extract();
            ShipBreakout.Root Breakout = ShipBreakout.Extract();
            ShipGroup.Root Group = ShipGroup.Extract();
            SkillTemplate.Root SkillEN = SkillTemplate.Extract("EN");
            SkillTemplate.Root SkillCN = SkillTemplate.Extract("CN");
            SkillTemplate.Root2 SkillJP = SkillTemplate.Extract2("JP");
            Buff.Root BuffCfg = Buff.Extract();
            ShipTransform.Root Transform = ShipTransform.Extract();
            ShipTransTemplate.Root TransTemplate = ShipTransTemplate.Extract();
            NameCode.Root NameCodes = NameCode.Extract();
            ItemStatistics.Root ItemStats = ItemStatistics.Extract();
            PropertyName.Init();

            Console.WriteLine("Finished extracting data");

            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

            string[] ProficiencyTypeGun = { "DD Guns", "CL/DD Guns", "CL Guns", "CA/CL Guns", "CA/CB Guns", "BB Guns" };

            List<string> shipList = new List<string>();
            bool bypass = false;

            while (true)
            {
                string shipToParse = "";
                if (!bypass)
                {
                    Console.WriteLine("\nWhat ship to parse?");
                    input = Console.ReadLine().ToLower().Trim();

                    if (input == "")
                    {
                        continue;
                    }

                    shipToParse = input;

                    if (input == "exit" || input == "quit")
                    {
                        Environment.Exit(0);
                    }
                    else if (input == "clear")
                    {
                        Console.Clear();
                        continue;
                    }
                    else if (input.Contains(".txt"))
                    {
                        if (File.Exists(input))
                        {
                            shipList = File.ReadAllLines(input).ToList();

                            if (shipList.Count > 0)
                            {
                                shipToParse = shipList[0].ToLower().Trim();
                                shipList.RemoveAt(0);
                                bypass = true;
                                File.AppendAllText("errorLog.txt", DateTime.Now + ": started parsing list of ships \n");
                            }
                            else
                            {
                                bypass = false;
                                continue;
                            }
                        }
                    }

                }
                else
                {
                    if (shipList.Count > 0)
                    {
                        shipToParse = shipList[0].ToLower().Trim();
                        shipList.RemoveAt(0);
                    }
                    else
                    {
                        bypass = false;
                        continue;
                    }
                }



                try
                {
                    string groupID = "";
                    shipToParse = shipToParse.Replace("µ", "μ");
                    int garbage;
                    if (shipToParse.Length > 3 && int.TryParse(shipToParse, out garbage))
                    {
                        if (StatsEN.data.ContainsKey(shipToParse + "1"))
                        {
                            groupID = shipToParse;
                        }
                        else
                        {
                            Console.WriteLine("There is no ship with GroupID " + shipToParse);
                            File.AppendAllText("errorlog.txt", "There is no ship with GroupID " + shipToParse + "\n");
                            continue;
                        }
                    }
                    else
                    {
                        List<string> ships = StatsEN.data.Where(kv => kv.Value.name.ToLower().Trim() == shipToParse).Select(kv => kv.Key).ToList();

                        ships.RemoveAll(s => s.Substring(0, 3) == "900" && s.Length == 6);

                        if (ships.Count > 5)
                        {
                            Console.WriteLine("Too many ships found for [" + shipToParse + "], use GroupID instead");
                            File.AppendAllText("errorlog.txt", "Too many ships found for [" + shipToParse + "], use GroupID instead\n");
                            continue;
                        }

                        int minStars = 7;
                        foreach (string ship in ships)
                        {
                            if (StatsEN.data[ship].star < minStars)
                            {
                                minStars = StatsEN.data[ship].star;
                                string temp = StatsEN.data[ship].id.ToString();
                                groupID = temp.Substring(0, temp.Length - 1);
                            }

                        }

                    }

                    string maxID = groupID + "1";
                    if (StatsEN.data.ContainsKey(groupID + "4"))
                    {
                        maxID = groupID + "4";
                    }

                    if (groupID == "")
                    {
                        Console.WriteLine("Couldn't find [" + shipToParse + "]");
                        File.AppendAllText("errorLog.txt", "Couldn't find [" + shipToParse + "]\n");
                        continue;
                    }

                    #region GeneralData

                    string Code = Group.data.First(kv => kv.Value.GroupType == long.Parse(groupID)).Value.Code.ToString();

                    if (int.Parse(Code) / 100 == 200 || int.Parse(Code) / 100 == 300)
                    {
                        Console.WriteLine("PR/META ship skipped [" + shipToParse + "]");
                        File.AppendAllText("errorLog.txt", "PR/META ship skipped [" + shipToParse + "]\n");
                        continue;
                    }

                    ShipStatistics.ShipData shipMin = StatsEN.data[groupID + "1"];
                    ShipStatistics.ShipData shipMax = StatsEN.data[maxID];

                    string NameEN = StatsEN.data[groupID + "1"].name;
                    string NameCN = StatsCN.data[groupID + "1"].name;
                    List<string> names = NameCodes.data.Where(kv => kv.Value.Name == NameCN).Select(kv => kv.Value.Code).ToList();
                    if (names.Count == 1)
                    {
                        NameCN = names[0];
                    }
                    string NameJP = StatsJP.data[groupID + "1"].name;

                    string Rarity = PropertyName.C("Rarity", shipMin.rarity);

                    string Nationality = PropertyName.C("Nationality", shipMin.nationality);

                    string Type = PropertyName.C("Type", shipMin.type);
                    string TypeRetro = "";

                    string Class = "";
                    bool HasTech = false;
                    if (Tech.data.ContainsKey(groupID))
                    {
                        Class = TechClass.data[Tech.data[groupID].Class.ToString()].Name;
                        if (Class.Contains("Class"))
                        {
                            Class = Class.Substring(0, Class.IndexOf(" Class"));
                        }
                        HasTech = true;
                    }
                    else if (shipMax.tag_list.Count > 0 && shipMax.tag_list[0].Contains("Class")) 
                    {
                        Class = shipMax.tag_list[0].Substring(0, shipMax.tag_list[0].IndexOf("Class") - 1);
                    }



                    string Armor = PropertyName.C("Armor", shipMax.armor_type);

                    List<double> StatsBase = shipMin.attrs;

                    List<double> statBaseMax = shipMax.attrs;
                    List<double> statGrowth = shipMax.attrs_growth;
                    List<double> durabilities = Strength.data[groupID].Durability;

                    List<int> Stats100 = Stats.CalculateStats(statBaseMax, statGrowth, 100, 1.06, durabilities);
                    List<int> Stats120 = Stats.CalculateStats(statBaseMax, statGrowth, 120, 1.06, durabilities);
                    List<int> Stats125 = Stats.CalculateStats(statBaseMax, statGrowth, 125, 1.06, durabilities);

                    List<int> Stats100Kai = new List<int>();
                    List<int> Stats120Kai = new List<int>();
                    List<int> Stats125Kai = new List<int>();

                    List<int> retroAdd = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                    string BaseConsumption = (Math.Floor(ShipTemp.data[groupID + "1"].OilAtEnd * ((100 + 1.0) / 200.0)) + 1).ToString();
                    string MaxConsumption = ShipTemp.data[maxID].OilAtEnd.ToString();

                    string retroID = "";

                    string Eq1Type = PropertyName.C("EquipType", ShipTemp.data[maxID].Equip1);
                    int Eq1BaseMax = shipMax.base_list[0];
                    string Eq1EffInit = Math.Round(shipMin.equipment_proficiency[0] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    string Eq1EffInitMax = Math.Round(shipMax.equipment_proficiency[0] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    int eq1effkai = 0;
                    string Eq1EffInitKai = "";
                    int Eq1BaseKai = 0;

                    string Eq2Type = PropertyName.C("EquipType", ShipTemp.data[maxID].Equip2);
                    int Eq2BaseMax = shipMax.base_list[1];
                    string Eq2EffInit = Math.Round(shipMin.equipment_proficiency[1] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    string Eq2EffInitMax = Math.Round(shipMax.equipment_proficiency[1] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    int eq2effkai = 0;
                    string Eq2EffInitKai = "";
                    int Eq2BaseKai = 0;

                    string Eq3Type = PropertyName.C("EquipType", ShipTemp.data[maxID].Equip3);
                    int Eq3BaseMax = shipMax.base_list[2];
                    string Eq3EffInit = Math.Round(shipMin.equipment_proficiency[2] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    string Eq3EffInitMax = Math.Round(shipMax.equipment_proficiency[2] * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
                    int eq3effkai = 0;
                    string Eq3EffInitKai = "";
                    int Eq3BaseKai = 0;

                    string BonusCollectType = "", Bonus120Type = "";
                    int BonusCollectValue = 0, Bonus120Value = 0, TechCollect = 0, TechMLB = 0, Tech120 = 0;
                    if (HasTech)
                    {
                        BonusCollectType = PropertyName.C("Bonus", Tech.data[groupID].AddGetAttr);
                        BonusCollectValue = Tech.data[groupID].AddGetValue;

                        Bonus120Type = PropertyName.C("Bonus", Tech.data[groupID].AddLevelAttr);
                        Bonus120Value = Tech.data[groupID].AddLevelValue;

                        TechCollect = Tech.data[groupID].PtGet;
                        TechMLB = Tech.data[groupID].PtUpgrage;
                        Tech120 = Tech.data[groupID].PtLevel;
                    }

                    List<int> Reinforcement = Strength.data[groupID].AttrExp;

                    string LB1text = "", LB2text = "", LB3text = "";
                    if (groupID + "1" != maxID)
                    {
                        LB1text = Breakout.data[groupID + "1"].BreakoutView.Replace("/", " / ");
                        LB2text = Breakout.data[groupID + "2"].BreakoutView.Replace("/", " / ");
                        LB3text = Breakout.data[groupID + "3"].BreakoutView.Replace("/", " / ");

                        if (Type == "Destroyer")
                        {
                            LB3text += PropertyName.C("UltimateBonus", Breakout.data[groupID + "3"].UltimateBonus[0]);
                        }

                    }

                    List<int> shipBuffs = ShipTemp.data[maxID].BuffList.ToList();
                    Dictionary<int, int> retroReplaces = new Dictionary<int, int>();
                    List<int> retroSkills = new List<int>();

                    List<Skill> ShipSkills = new List<Skill>();

                    #endregion

                    #region Retrofit

                    string Remodel = "";

                    string RetrofitMap = "", Duplicate = "";
                    List<TransformNode> transformNodes = new List<TransformNode>();
                    if (Transform.data.ContainsKey(groupID))
                    {
                        Remodel = "1";

                        List<List<List<int>>> nodePositions = Transform.data[groupID].TransformList;

                        string topM = "", middleM = "", bottomM = "";

                        string index = "ABCDEFGHIJKL";

                        foreach (List<List<int>> column in nodePositions)
                        {
                            bool top = false, middle = false, bottom = false;

                            foreach (List<int> node in column)
                            {
                                string temp = node[1].ToString();
                                int tempI = int.Parse(temp.Substring(temp.Length - 2));

                                if (node[0] == 2)
                                {
                                    top = true;
                                    topM += index[tempI - 1];
                                }
                                else if (node[0] == 3)
                                {
                                    middle = true;
                                    middleM += index[tempI - 1];
                                }
                                else if (node[0] == 4)
                                {
                                    bottom = true;
                                    bottomM += index[tempI - 1];
                                }
                            }

                            if (top == false)
                            {
                                topM += "-";
                            }
                            else if (middle == false)
                            {
                                middleM += "-";
                            }
                            else if (bottom == false)
                            {
                                bottomM += "-";
                            }
                        }

                        RetrofitMap = "[" + topM + "][" + middleM + "][" + bottomM + "]";

                        int currentTransform = Transform.data[groupID].TransformList[0][0][1];

                        while (TransTemplate.data.ContainsKey(currentTransform.ToString()))
                        {
                            TransformNode newNode = new TransformNode();

                            string tran = currentTransform.ToString();
                            newNode.Index = index[int.Parse(tran.Substring(tran.Length - 2)) - 1];

                            newNode.RetrofitImage = TransTemplate.data[tran].Icon.ToLower();
                            if (newNode.RetrofitImage.Contains("cn_"))
                            {
                                newNode.RetrofitImage = newNode.RetrofitImage.Replace("cn_", "shell_");
                            }
                            newNode.ProjName = TransTemplate.data[tran].Name;
                            newNode.ProjType = PropertyName.C("ProjType", newNode.RetrofitImage);
                            newNode.Coins = TransTemplate.data[tran].UseGold.ToString();
                            newNode.Level = TransTemplate.data[tran].LevelLimit.ToString();
                            newNode.LimitBreak = (TransTemplate.data[tran].StarLimit - 2).ToString();
                            newNode.Repeat = TransTemplate.data[tran].MaxLevel.ToString();

                            string required = "";
                            foreach (int condition in TransTemplate.data[tran].ConditionId)
                            {
                                string temp = condition.ToString();
                                char chara = index[int.Parse(temp.Substring(temp.Length - 2)) - 1];
                                if (required == "")
                                {
                                    required += chara;
                                }
                                else
                                {
                                    required += ", " + chara;
                                }
                            }
                            newNode.Requirements = required;


                            string BPnum = "", BPtype = "", BPrarity = "";
                            string PlateNum = "", PlateType = "", PlateTier = "";
                            foreach (List<List<int>> level in TransTemplate.data[tran].UseItem)
                            {
                                foreach (List<int> itemGroup in level)
                                {
                                    string itemName = ItemStats.data[itemGroup[0].ToString()].Name;

                                    if (itemName.Contains("Retrofit Blueprint"))
                                    {
                                        BPtype = Search.FindProperty(itemName, " ", " ", 0, false);
                                        BPrarity = itemName.Substring(0, 2);
                                        if (BPnum == "")
                                        {
                                            BPnum += itemGroup[1].ToString();
                                        }
                                        else
                                        {
                                            BPnum += " / " + itemGroup[1].ToString();
                                        }
                                    }
                                    else if (itemName.Contains("Part"))
                                    {
                                        string ptype = Search.FindProperty(itemName, " ", " Part", 0, false);
                                        PlateType = PropertyName.C("PlateType", ptype);
                                        PlateTier = itemName.Substring(0, 2);
                                        if (PlateNum == "")
                                        {
                                            PlateNum += itemGroup[1].ToString();
                                        }
                                        else
                                        {
                                            PlateNum += " / " + itemGroup[1].ToString();
                                        }
                                    }
                                    else
                                    {
                                        newNode.MatNotes = "1x [[File:" + itemName + ".png|25px|link=]] ";
                                    }
                                }
                            }
                            newNode.BPnum = BPnum;
                            newNode.BPrarity = BPrarity;
                            newNode.PlateNum = PlateNum;
                            newNode.PlateType = PlateType;
                            newNode.PlateTier = PlateTier;
                            if (BPtype != Type)
                            {
                                newNode.BPtype = BPtype;
                            }

                            if (Rarity == "Super Rare" && TransTemplate.data[tran].UseShip == 1)
                            {
                                newNode.MatNotes += "and 1x [[File:" + NameEN + "Icon.png|25px|link=]] or [[File:Trial_Bullin_MKIIIcon.png|25px|link=Prototype Bulin MKII]]";
                            }

                            if (newNode.ProjName == "Modernization" && (TransTemplate.data[tran].UseShip == 0 || Rarity == "Super Rare"))
                            {
                                Duplicate = "0";
                            }

                            if (TransTemplate.data[tran].SkillId != 0)
                            {
                                newNode.Attribute = "Unlock Skill \"" + SkillEN.data[TransTemplate.data[tran].SkillId.ToString()].Name + "\"";
                                retroSkills.Add(TransTemplate.data[tran].SkillId);
                                shipBuffs.Add(TransTemplate.data[tran].SkillId);
                            }
                            else
                            {
                                string attrib = "", effAttrib = "", initAttrib = "";
                                bool useEff = false;
                                foreach (Dictionary<string, double> level in TransTemplate.data[tran].Effect)
                                {
                                    int kvp = 0;
                                    foreach (KeyValuePair<string, double> kv in level)
                                    {
                                        if (!kv.Key.Contains("proficiency"))
                                        {
                                            if (attrib == "")
                                            {
                                                attrib += "{{" + PropertyName.C("Attribute", kv.Key) + "}} +" + (int)kv.Value;
                                            }
                                            else if (kvp == 0)
                                            {
                                                attrib += " / {{" + PropertyName.C("Attribute", kv.Key) + "}} +" + (int)kv.Value;
                                            }
                                            else if (kvp > 0)
                                            {
                                                attrib += ", {{" + PropertyName.C("Attribute", kv.Key) + "}} +" + (int)kv.Value;
                                            }

                                            retroAdd[PropertyName.C("Stat", kv.Key, false)] += (int)kv.Value;
                                        }
                                        else
                                        {
                                            string slot = kv.Key.Substring(kv.Key.Length - 1), addAttr = "", addEff = "";

                                            string attrType = "", temp = "";
                                            if (slot == "1") { temp = Eq1Type; } else if (slot == "2") { temp = Eq2Type; } else if (slot == "3") { temp = Eq3Type; }
                                            if (temp == "Torpedoes") { attrType = "Torpedo"; } else if (temp == "Anti-Air Guns") { attrType = "Anti-Air Gun"; }
                                            else if (temp == "Fighters") { attrType = "Fighter"; } else if (temp == "Dive Bombers") { attrType = "Dive Bomber"; }
                                            else if (temp == "Torpedo Bombers") { attrType = "Torpedo Bomber"; }


                                            if (slot == "1")
                                            {
                                                if (ProficiencyTypeGun.Contains(Eq1Type)) { attrType = "Main Gun"; } 
                                                if (attrType == "") { attrType = "Equip 1"; }
                                                addAttr = attrType + " Efficiency ";
                                                addEff = "Equip 1 Efficiency ";
                                                eq1effkai += (int)(kv.Value * 100);
                                            }
                                            else if (slot == "2")
                                            {
                                                if (ProficiencyTypeGun.Contains(Eq1Type)) { attrType = "Secondary Gun"; }
                                                if (attrType == "") { attrType = "Equip 2"; }
                                                addAttr = attrType + " Efficiency ";
                                                addEff = "Equip 2 Efficiency ";
                                                eq2effkai += (int)(kv.Value * 100);
                                            }
                                            else if (slot == "3")
                                            {
                                                if (attrType == "") { attrType = "Equip 3"; }
                                                addAttr = attrType + " Efficiency ";
                                                addEff = "Equip 3 Efficiency ";
                                                eq3effkai += (int)(kv.Value * 100);
                                            }

                                            if (addAttr != initAttrib && initAttrib != "")
                                            {
                                                useEff = true;
                                            }
                                            else if (initAttrib == "")
                                            {
                                                initAttrib = addAttr;
                                            }

                                            if (attrib == "")
                                            {
                                                attrib += addAttr + "+" + (int)(kv.Value * 100) + ".0%";
                                                effAttrib += addEff + "+" + (int)(kv.Value * 100) + ".0%";
                                            }
                                            else if (kvp == 0)
                                            {
                                                attrib += " / " + "+" + (int)(kv.Value * 100) + ".0%";
                                                effAttrib += " / " + addEff + "+" + (int)(kv.Value * 100) + ".0%";
                                            }
                                            else if (kvp > 0)
                                            {
                                                //attrib += ", " + "+" + (int)(kv.Value * 100) + ".0%";
                                                effAttrib += ", " + addEff + "+" + (int)(kv.Value * 100) + ".0%";
                                            }
                                        }

                                        kvp++;
                                    }
                                }

                                if (useEff)
                                {
                                    newNode.Attribute = effAttrib;
                                }
                                else
                                {
                                    newNode.Attribute = attrib;
                                }

                                if (TransTemplate.data[tran].ShipId.Count != 0)
                                {
                                    retroID = TransTemplate.data[tran].ShipId[0][1].ToString();

                                    ShipStatistics.ShipData retroShip = StatsEN.data[retroID];

                                    if (shipMax.base_list[0] != retroShip.base_list[0] || shipMax.base_list[1] != retroShip.base_list[1] || shipMax.base_list[2] != retroShip.base_list[2])
                                    {
                                        Eq1BaseKai = retroShip.base_list[0];
                                        Eq2BaseKai = retroShip.base_list[1];
                                        Eq3BaseKai = retroShip.base_list[2];
                                    }

                                    for (int i = 0; i < ShipTemp.data[maxID].BuffList.Count; i++)
                                    {
                                        if (ShipTemp.data[maxID].BuffList[i] != ShipTemp.data[retroID].BuffList[i])
                                        {
                                            retroReplaces[ShipTemp.data[retroID].BuffList[i]] = ShipTemp.data[maxID].BuffList[i];
                                            if (!SkillEN.data[ShipTemp.data[retroID].BuffList[i].ToString()].Name.Contains("All Out Assault") &&
                                                !SkillEN.data[ShipTemp.data[retroID].BuffList[i].ToString()].Name.Contains("All-Out Assault"))
                                            {
                                                retroSkills.Add(ShipTemp.data[retroID].BuffList[i]);
                                                shipBuffs.Add(ShipTemp.data[retroID].BuffList[i]);
                                            }
                                        }
                                    }
                                }
                            }

                            transformNodes.Add(newNode);
                            currentTransform++;
                        }

                        Eq1EffInitKai = ((int)(shipMax.equipment_proficiency[0] * 100 + 0.1) + eq1effkai).ToString() + "%";
                        Eq2EffInitKai = ((int)(shipMax.equipment_proficiency[1] * 100 + 0.1) + eq2effkai).ToString() + "%";
                        Eq3EffInitKai = ((int)(shipMax.equipment_proficiency[2] * 100 + 0.1) + eq3effkai).ToString() + "%";

                        if (retroID != "")
                        {
                            ShipStatistics.ShipData shipRetro = StatsEN.data[retroID];

                            if (shipRetro.type != shipMax.type)
                            {
                                TypeRetro = PropertyName.C("Type", shipRetro.type);
                            }

                            List<double> retroBaseMax = shipRetro.attrs;
                            List<double> retroGrowth = shipRetro.attrs_growth;
                            List<double> retroDurabilities = Strength.data[groupID].Durability;

                            Stats100Kai = Stats.CalculateStats(retroBaseMax, retroGrowth, 100, 1.06, retroDurabilities);
                            Stats120Kai = Stats.CalculateStats(retroBaseMax, retroGrowth, 120, 1.06, retroDurabilities);
                            Stats125Kai = Stats.CalculateStats(retroBaseMax, retroGrowth, 125, 1.06, retroDurabilities);
                        }
                        else
                        {
                            Stats100Kai = Stats.CalculateStats(statBaseMax, statGrowth, 100, 1.06, durabilities);
                            Stats120Kai = Stats.CalculateStats(statBaseMax, statGrowth, 120, 1.06, durabilities);
                            Stats125Kai = Stats.CalculateStats(statBaseMax, statGrowth, 125, 1.06, durabilities);
                        }

                        for (int i = 0; i < Stats100.Count; i++)
                        {
                            Stats100Kai[i] += retroAdd[i];
                            Stats120Kai[i] += retroAdd[i];
                            Stats125Kai[i] += retroAdd[i];
                        }
                    }

                    #endregion

                    #region Skills

                    shipBuffs.Sort();

                    foreach (int buffID in shipBuffs)
                    {
                        string sBuffID = buffID.ToString();

                        if (SkillEN.data[sBuffID].Type == 0)
                        {
                            continue;
                        }

                        Skill newSkill = new Skill();

                        newSkill.SkillType = PropertyName.C("SkillType", SkillEN.data[sBuffID].Type);
                        newSkill.SkillNameEN = SkillEN.data[sBuffID].Name.Trim();
                        newSkill.SkillNameCN = SkillCN.data[sBuffID].Name.Trim();
                        newSkill.SkillNameJP = SkillJP.data[sBuffID].Name.Trim();
                        newSkill.SkillIcon = BuffCfg.data["buff_" + buffID].Icon;
                        newSkill.SkillDesc = SkillEN.data[sBuffID].Desc;

                        if ((newSkill.SkillNameEN.Contains("All Out Assault") || newSkill.SkillNameEN.Contains("All-Out Assault")) && newSkill.SkillNameEN.Length < 20)
                        {
                            string previousBuffID = sBuffID.Substring(0, sBuffID.Length - 1) + (int.Parse(sBuffID[^1].ToString()) - 1);
                            string part = SkillEN.data[sBuffID].Desc;
                            part = part.Substring(part.IndexOf("once every ") + 11);
                            part = part.Substring(0, part.IndexOf(" "));

                            string previousBuff = SkillEN.data[previousBuffID].Desc;

                            newSkill.SkillDesc = previousBuff.Replace("I:", "I (II):").Replace("Ⅰ:", "I (II):").Replace("times", "(" + part + ") times");

                            newSkill.SkillNameEN = newSkill.SkillNameEN.Replace("II", "").Replace("ⅠⅠ", "").Replace("Ⅱ", "").Replace(":","").Replace("-", " ").Trim();
                            newSkill.SkillNameCN = newSkill.SkillNameCN.Replace("II", "").Replace("ⅠⅠ", "").Replace("Ⅱ", "").Replace(":", "").Trim();
                            newSkill.SkillNameJP = newSkill.SkillNameJP.Replace("II", "").Replace("ⅠⅠ", "").Replace("Ⅱ", "").Replace(":", "").Trim();

                            if (retroReplaces.ContainsValue(int.Parse(sBuffID)))
                            {
                                int retroSkillID = retroReplaces.Where(kv => kv.Value == int.Parse(sBuffID)).First().Key;
                                newSkill.SkillDesc += "\n'''(Upon Retrofit)''' " + SkillEN.data[retroSkillID.ToString()].Desc;
                            }

                        }
                        else
                        {
                            int guard = SkillEN.data[sBuffID].DescGetAdd.Count;

                            for (int i = 1; i <= guard; i++)
                            {
                                newSkill.SkillDesc = newSkill.SkillDesc.Replace("$" + i, SkillEN.data[sBuffID].DescGetAdd[i - 1][0] + " (" + SkillEN.data[sBuffID].DescGetAdd[i - 1][1] + ")");
                            }

                            if (retroSkills.Contains(int.Parse(sBuffID)))
                            {
                                newSkill.SkillNameEN = "(Retrofit) " + newSkill.SkillNameEN;
                            }

                            if (retroReplaces.ContainsKey(int.Parse(sBuffID)))
                            {
                                newSkill.SkillDesc += "\n'''(Replaces " + SkillEN.data[retroReplaces[int.Parse(sBuffID)].ToString()].Name + ")'''";
                            }
                        }

                        if (newSkill.SkillNameCN.Contains("namecode:"))
                        {
                            string code = Search.FindProperty(newSkill.SkillNameCN, "namecode:", "}", 0, false);

                            newSkill.SkillNameCN = newSkill.SkillNameCN.Replace("{namecode:" + code + "}", NameCodes.data[code].Code);
                        }

                        ShipSkills.Add(newSkill);
                    }

                    #endregion

                    // Fill Template
                    #region FillTemplate

                    #region Section1

                    string Section1 = string.Format(@"<!------------- Section 1: General ------------->
 | ID = {0}
 | CNName = {1}
 | JPName = {2}
 | KRName = {3}
 | Rarity = {4}
 | Nationality = {5}
 | ConstructTime = 
 | Type = {6}", Code, NameCN, NameJP, "", Rarity, Nationality, Type);

                    if (Class != "")
                    {
                        Section1 += "\n | Class = " + Class;
                    }

                    if (Remodel == "1")
                    {
                        Section1 += "\n | Remodel = 1";
                    }

                    if (TypeRetro != "")
                    {
                        Section1 += "\n | SubtypeRetro = " + TypeRetro;
                    }

                    Section1 += "\n" + string.Format(@"
 | Artist = 
 | ArtistLink = 
 | ArtistPixiv = 
 | ArtistTwitter = 
 | VA = ");

                    #endregion

                    #region Section2

                    string Section2 = string.Format(@"<!------------- Section 2: Stats ------------->
 | Luck = {0}
 | Armor = {1}
 | Speed = {2}", StatsBase[10], Armor, (int)StatsBase[9]);

                    if (Remodel == "1")
                    {
                        Section2 += "\n | SpeedKai = " + Stats100Kai[9];
                    }


                    string statTemplate = @"<!-- Name -->
 | HealthStage = {0}
 | FireStage = {1}
 | AAStage = {2}
 | TorpStage = {3}
 | AirStage = {4}
 | ReloadStage = {5}
 | EvadeStage = {6}
 | ConsumptionStage = {7}
 | ASWStage = {8}
 | AccStage = {9}";

                    string statBase = statTemplate.Replace("Name", "Base").Replace("Stage", "Initial");
                    string stat100 = statTemplate.Replace("Name", "Level 100").Replace("Stage", "Max");
                    string stat120 = statTemplate.Replace("Name", "Level 120").Replace("Stage", "120");
                    string stat125 = statTemplate.Replace("Name", "Level 125").Replace("Stage", "125");
                    string stat100retro = "", stat120retro = "", stat125retro = "";

                    statBase = string.Format(statBase, StatsBase[0], StatsBase[1], StatsBase[3], StatsBase[2], StatsBase[4], StatsBase[5],
                        StatsBase[8], BaseConsumption, StatsBase[11], StatsBase[7]);
                    stat100 = string.Format(stat100, Stats100[0], Stats100[1], Stats100[3], Stats100[2], Stats100[4], Stats100[5],
                        Stats100[8], MaxConsumption, Stats100[11], Stats100[7]);
                    stat120 = string.Format(stat120, Stats120[0], Stats120[1], Stats120[3], Stats120[2], Stats120[4], Stats120[5],
                        Stats120[8], MaxConsumption, Stats120[11], Stats120[7]);
                    stat125 = string.Format(stat125, Stats125[0], Stats125[1], Stats125[3], Stats125[2], Stats125[4], Stats125[5],
                        Stats125[8], MaxConsumption, Stats125[11], Stats125[7]);

                    Section2 += "\n\n" + statBase + "\n\n" + stat100 + "\n\n" + stat120 + "\n\n" + stat125;

                    if (Remodel == "1")
                    {
                        stat100retro = statTemplate.Replace("Name", "Level 100 Retrofit").Replace("Stage", "Kai");
                        stat120retro = statTemplate.Replace("Name", "Level 120 Retrofit").Replace("Stage", "Kai120");
                        stat125retro = statTemplate.Replace("Name", "Level 125 Retrofit").Replace("Stage", "Kai125");

                        stat100retro = string.Format(stat100retro, Stats100Kai[0], Stats100Kai[1], Stats100Kai[3], Stats100Kai[2], Stats100Kai[4], Stats100Kai[5],
                            Stats100Kai[8], MaxConsumption, Stats100Kai[11], Stats100Kai[7]);
                        stat120retro = string.Format(stat120retro, Stats120Kai[0], Stats120Kai[1], Stats120Kai[3], Stats120Kai[2], Stats120Kai[4], Stats120Kai[5],
                            Stats120Kai[8], MaxConsumption, Stats120Kai[11], Stats120Kai[7]);
                        stat125retro = string.Format(stat125retro, Stats125Kai[0], Stats125Kai[1], Stats125Kai[3], Stats125Kai[2], Stats125Kai[4], Stats125Kai[5],
                            Stats125Kai[8], MaxConsumption, Stats125Kai[11], Stats125Kai[7]);

                        Section2 += "\n\n" + stat100retro + "\n\n" + stat120retro + "\n\n" + stat125retro;
                    }

                    #endregion

                    #region Section3
                    // Equip 1
                    string Section3 = string.Format(@"<!------------- Section 3: Equipment ------------->
 | Eq1Type = {0}
 | Eq1BaseMax = {1}", Eq1Type, Eq1BaseMax);

                    if (Remodel == "1" && Eq1BaseMax != Eq1BaseKai)
                    {
                        Section3 += "\n | Eq1BaseKai = " + Eq1BaseKai;
                    }

                    Section3 += "\n" + string.Format(@" | Eq1EffInit = {0}
 | Eq1EffInitMax = {1}", Eq1EffInit, Eq1EffInitMax);

                    if (Remodel == "1")
                    {
                        Section3 += "\n | Eq1EffInitKai = " + Eq1EffInitKai;
                    }

                    // Equip 2
                    Section3 += "\n\n" + string.Format(@" | Eq2Type = {0}
 | Eq2BaseMax = {1}", Eq2Type, Eq2BaseMax);

                    if (Remodel == "1" && Eq2BaseMax != Eq2BaseKai)
                    {
                        Section3 += "\n | Eq2BaseKai = " + Eq2BaseKai;
                    }

                    Section3 += "\n" + string.Format(@" | Eq2EffInit = {0}
 | Eq2EffInitMax = {1}", Eq2EffInit, Eq2EffInitMax);

                    if (Remodel == "1")
                    {
                        Section3 += "\n | Eq2EffInitKai = " + Eq2EffInitKai;
                    }

                    // Equip 3
                    Section3 += "\n\n" + string.Format(@" | Eq3Type = {0}
 | Eq3BaseMax = {1}", Eq3Type, Eq3BaseMax);

                    if (Remodel == "1" && Eq3BaseMax != Eq3BaseKai)
                    {
                        Section3 += "\n | Eq3BaseKai = " + Eq3BaseKai;
                    }

                    Section3 += "\n" + string.Format(@" | Eq3EffInit = {0}
 | Eq3EffInitMax = {1}", Eq3EffInit, Eq3EffInitMax);

                    if (Remodel == "1")
                    {
                        Section3 += "\n | Eq3EffInitKai = " + Eq3EffInitKai;
                    }

                #endregion

                    #region Section4

                    string Section4 = string.Format(@"<!------------- Section 4: Fleet Tech ------------->");

                    if (HasTech) {
                        Section4 += "\n" + string.Format(@"
 | StatBonusCollectType = {0}
 | StatBonusCollect = {1}
 | StatBonus120Type = {2}
 | StatBonus120 = {3}

 | TechPointCollect = {4}
 | TechPointMLB = {5}
 | TechPoint120 = {6}", BonusCollectType, BonusCollectValue, Bonus120Type, Bonus120Value, TechCollect, TechMLB, Tech120) + "\n";
                    }

                    Section4 += "\n" + string.Format(" | ReinforcementValue = {0} {{{{Firepower}}}} {1} {{{{Torpedo}}}} {2} {{{{Aviation}}}} {3} {{{{RoF}}}}", 
                                Reinforcement[0], Reinforcement[1], Reinforcement[3], Reinforcement[4]);

                    #endregion

                    #region Section5

                    string Section5 = string.Format(@"<!------------- Section 5: LB + Skill ------------->
 | LB1 = {0}
 | LB2 = {1}
 | LB3 = {2}", LB1text, LB2text, LB3text);

                    string skillTemplate = @" | Type! = {0}
 | Skill! = {1}
 | Skill!CN = {2}
 | Skill!JP = {3}
 | Skill!Desc = {4}
 | Skill!Icon = {5}";

                    int skillInt = 1;
                    foreach (Skill skill in ShipSkills)
                    {
                        string skillText = skillTemplate.Replace("!", skillInt.ToString());
                        skillText = string.Format(skillText, skill.SkillType, skill.SkillNameEN, skill.SkillNameCN, skill.SkillNameJP, skill.SkillDesc, skill.SkillIcon);

                        Section5 += "\n\n" + skillText;

                        skillInt++;
                    }

                    #endregion

                    #region Section6
                    #endregion

                    #region Section7

                    string Section7 = string.Format(@"<!------------ Section 7: Retrofit ------------>
 | RetrofitMap = {0}", RetrofitMap);

                    if (Duplicate == "0")
                    {
                        Section7 += "\n | Duplicate = 0";
                    }

                    string r1 = @" | Index! = {0}
 | RetrofitImage! = {1}
 | ProjName! = {2}
 | ProjType! = {3}
 | Attribute! = {4}";

                    string r2 = @" | Coin! = {0}
 | LV! = {1}
 | LimitBreak! = {2}
 | Repeat! = {3}";

                    foreach (TransformNode RetroNode in transformNodes)
                    {
                        string temp = r1.Replace('!', RetroNode.Index);

                        Section7 += "\n\n" + string.Format(temp, RetroNode.Index, RetroNode.RetrofitImage, RetroNode.ProjName, RetroNode.ProjType, RetroNode.Attribute);

                        if (RetroNode.BPnum != "")
                        {
                            Section7 += "\n | BPNum" + RetroNode.Index + " = " + RetroNode.BPnum;

                            if (RetroNode.BPtype != "")
                            {
                                Section7 += "\n | BPType" + RetroNode.Index + " = " + RetroNode.BPtype;
                            }

                            Section7 += "\n | BPRarity" + RetroNode.Index + " = " + RetroNode.BPrarity;
                        }

                        if (RetroNode.PlateNum != "")
                        {
                            Section7 += "\n | PlateNum" + RetroNode.Index + " = " + RetroNode.PlateNum;
                            Section7 += "\n | PlateType" + RetroNode.Index + " = " + RetroNode.PlateType;
                            Section7 += "\n | PlateTier" + RetroNode.Index + " = " + RetroNode.PlateTier;
                        }

                        temp = r2.Replace('!', RetroNode.Index);

                        Section7 += "\n" + string.Format(temp, RetroNode.Coins, RetroNode.Level, RetroNode.LimitBreak, RetroNode.Repeat);

                        if (RetroNode.MatNotes != "")
                        {
                            Section7 += "\n | MatNotes" + RetroNode.Index + " = " + RetroNode.MatNotes;
                        }

                        if (RetroNode.Requirements != "")
                        {
                            Section7 += "\n | Requirements" + RetroNode.Index + " = " + RetroNode.Requirements;
                        }

                    }


                    #endregion

                    #endregion

                    string output = "{{Ship\n" + Section1 + "\n\n" + Section2 + "\n\n" + Section3 + "\n\n" + Section4 + "\n\n" + Section5;

                    if (Remodel == "1")
                    {
                        output += "\n\n" + Section7;
                    }

                    output += "\n}}";

                    File.WriteAllText("output/" + NameEN + ".txt", output);

                    Console.WriteLine("Wrote data to " + NameEN + ".txt");

                }
                catch
                {
                    Console.WriteLine("Encountered an error while parsing [" + shipToParse + "]");
                    File.AppendAllText("errorLog.txt", "Encountered an error while parsing [" + shipToParse + "]\n");
                }

            }
            
            Console.WriteLine();


        }
        

        public static string RetrieveFromLink(string url)
        {
            WebRequest webRequest;
            WebResponse webResponse;
            Stream streamResponse;
            StreamReader sreader;

            webRequest = WebRequest.Create(url);
            webResponse = webRequest.GetResponse();

            streamResponse = webResponse.GetResponseStream();

            sreader = new StreamReader(streamResponse);
            string data = sreader.ReadToEnd();

            return data;
        }
    }


}
