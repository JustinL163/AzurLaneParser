using System;
using System.Collections.Generic;
using System.Text;

namespace AzurLaneParser
{
    class Search
    {

        public static string FindProperty(ref string searchSpace, string start, string end, int limit, bool trim)
        {
            string temp = searchSpace;
            int startPos = searchSpace.IndexOf(start);
            if (startPos != -1)
            {
                searchSpace = searchSpace.Substring(startPos + start.Length);
                int endPos = searchSpace.IndexOf(end);

                if (endPos - startPos < limit || limit == 0)
                {
                    string value = searchSpace.Substring(0, endPos);
                    searchSpace = searchSpace.Substring(endPos + end.Length);

                    if (!trim)
                    {
                        searchSpace = temp;
                    }

                    if (value != "")
                    {
                        //File.AppendAllText("shipTest.txt", value + "\n");
                        return value;
                    }
                }
            }

            return "";

        }

        public static string FindProperty(string searchSpace, string start, string end, int limit, bool trim)
        {
            string temp = searchSpace;
            int startPos = searchSpace.IndexOf(start);
            if (startPos != -1)
            {
                searchSpace = searchSpace.Substring(startPos + start.Length);
                int endPos = searchSpace.IndexOf(end);

                if (endPos - startPos < limit || limit == 0)
                {
                    string value = searchSpace.Substring(0, endPos);
                    searchSpace = searchSpace.Substring(endPos + end.Length);

                    if (!trim)
                    {
                        searchSpace = temp;
                    }

                    if (value != "")
                    {
                        //File.AppendAllText("shipTest.txt", value + "\n");
                        return value;
                    }
                }
            }

            return "";

        }

    }
}
