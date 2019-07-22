using DataParsing;
using DataParsing.Interfaces;
using DataParsing.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TestEndpoint testCall = new TestEndpoint();
            string response = testCall.TestGetData();
            Console.WriteLine("Serialized Data:");
            Console.WriteLine(response);
            Console.WriteLine("-------------------");
            Console.WriteLine("Deserialized Data:");
            var deserialize = JsonConvert.DeserializeObject<List<Schema>>(response);
            foreach (var item in deserialize)
            {
                Console.WriteLine(item.Entity+"-"+item.ColumnName +"-"+ item.ColumnValue);
            }
            Console.ReadLine();
        }
    }
}
