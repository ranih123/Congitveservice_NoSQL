using DataParsing.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace DataParsing.Repository
{
    internal static class DataSourceRepository
    {
        public static string GetDataFromSource()
        {
            string filePath = Path.GetFullPath(@"TempData\TempData.json");
            StreamReader r = new StreamReader(filePath);
            string data = r.ReadToEnd();
            return data;
        }
    }
}
