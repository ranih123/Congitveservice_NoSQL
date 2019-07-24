using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSearchLibrary.Helper
{
    public static class ExtensionMethods
    {
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }



        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }



                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }
    }
    public static class IntExtensions
    {
        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }
        public static bool IsLessThan(this int i, int value)
        {
            return i < value;
        }

        public static bool IsGreaterThanEqual(this int i, int value)
        {
            return i >= value;
        }
        public static bool IsLessThanEqual(this int i, int value)
        {
            return i <= value;
        }
    }
    public static class DateExtensions
    {
        public static bool DateIsGreaterThan(this DateTime i, DateTime value)
        {
            return i > value;
        }
        public static bool DateIsLessThan(this DateTime i, DateTime value)
        {
            return i < value;
        }

        public static bool DateIsGreaterThanEqual(this DateTime i, DateTime value)
        {
            return i >= value;
        }
        public static bool DateIsLessThanEqual(this DateTime i, DateTime value)
        {
            return i <= value;
        }
    }
}
