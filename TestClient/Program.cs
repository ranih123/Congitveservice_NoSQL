using DataParsing;
using DataParsing.Interfaces;
using DataParsing.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using DataSearchLibrary;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestEndpoint testCall = new TestEndpoint();
            //string response = testCall.TestGetData();
            //Console.WriteLine("Serialized Data:");
            //Console.WriteLine(response);
            //Console.WriteLine("-------------------");
            //Console.WriteLine("Deserialized Data:");
            //var deserialize = JsonConvert.DeserializeObject<Dictionary<string, HashSet<string>>>(response);
            //foreach (var item in deserialize)
            //{
            //    Console.WriteLine(item.Key + "-" + item.Value.ToString());
            //}


            TestEndPointSearch testCall = new TestEndPointSearch();
            testCall.TestGetData();
            Console.ReadLine();
        }
    }
}
