using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataSearchLibrary.Helper
{
    class BlobConnectionHelper
    {
        public static CloudBlobClient CreateBlobConneciton()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=testhackstorage;AccountKey=f/HiZe0UKqTJz6WNAZ8S6ggc5OANAYqpOGn4Hq9UujgfcuTefSYQdjXBe5LfuAu9d89ifz1YE9skRboRlImCOg==;EndpointSuffix=core.windows.net";

            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient cloudBlobClient = account.CreateCloudBlobClient();

            return cloudBlobClient;
        }

        public static string GetBlobData(CloudBlobClient cloudBlobClient, string containerName)
        {
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            StringBuilder data =  new StringBuilder();

            //// List the blobs in the container.
            foreach (var file in cloudBlobContainer.ListBlobs(useFlatBlobListing: true))
            {
                int lastIndex = file.Uri.ToString().LastIndexOf('/');
                string blobName = file.Uri.ToString().Substring(lastIndex + 1);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                using (StreamReader reader = new StreamReader(cloudBlockBlob.OpenRead()))
                {
                   data.Append(reader.ReadToEnd());
                }
            }
            
            return data.ToString();
        }
    }
}

