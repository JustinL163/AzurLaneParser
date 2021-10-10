using System;
using System.Collections.Generic;
using System.Text;

namespace AzurLaneParser
{
    class Stats
    {

        public static List<int> CalculateStats(List<double> statsBase, List<double> statsGrowth, int level, double affection, List<double> durability)
        {
            List<int> statsResult = new List<int>();
            int cd = 0;

            for (int i = 0; i < statsBase.Count; i++)
            {
                if (i == 1 || i == 2 || i == 4 || i == 5)
                {
                    if (i == 4) { cd++; }

                    double result = (statsBase[i] + ((level - 1) * (statsGrowth[i] / 1000)) + durability[cd]) * affection;

                    int stat = (int)Math.Floor(result);

                    statsResult.Add(stat);

                    cd++;
                }
                else if (i != 6 && i != 9 && i != 10)
                {

                    double result = (statsBase[i] + ((level - 1) * (statsGrowth[i] / 1000))) * affection;

                    int stat = (int)Math.Floor(result);

                    statsResult.Add(stat);

                }
                else
                {
                    statsResult.Add((int)statsBase[i]);
                }

            }         

            return statsResult;

        }

    }
}
