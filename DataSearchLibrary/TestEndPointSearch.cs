using DataSearchLibrary.Helper;
using DataSearchLibrary.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataSearchLibrary
{
    public class TestEndPointSearch
    {
        public void TestGetData()
        {
            CloudBlobClient cloudBlobClient =  BlobConnectionHelper.CreateBlobConneciton();
            string result = BlobConnectionHelper.GetBlobData(cloudBlobClient, "data-container");
            List<string> test = new List<string>();
            test.Add(result);

            IBuildQuery buildQuery = new BuildQuery();
            var res = buildQuery.BuildQueryJson(null, null, test);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
