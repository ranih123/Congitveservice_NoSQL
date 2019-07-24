using DataSearchLibrary;
using DataSearchLibrary.Helper;
using DataSearchLibrary.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.UI;

namespace CognitiveServiceApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Call the NLP to get the model (Will get serialized JSON)
            string url = "http://localhost:7071/api/HttpTriggerForNER/?sentence=" + txtQuery.Text;
            string NLPResult = GetNLPData(url);

            // Call the search library to get the result
            IBuildQuery buildQuery = new BuildQuery();

            // Get the blob data
            CloudBlobClient cloudBlobClient = BlobConnectionHelper.CreateBlobConneciton();
            string blobData = BlobConnectionHelper.GetBlobData(cloudBlobClient, "error-log-container");
            List<string> listBlobData = new List<string>();
            listBlobData.Add(blobData);

            var result = buildQuery.BuildQueryJson(NLPResult, null, listBlobData);

            // Set the result in the UI

            string serializeData = JsonConvert.SerializeObject(result);
            var deserializeData = JsonConvert.DeserializeObject<List<string>>(serializeData);

            resultGrid.DataSource = deserializeData;
            resultGrid.DataBind();
        }

        public static string GetNLPData(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); ;
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                return response;
            }
        }
    }
}