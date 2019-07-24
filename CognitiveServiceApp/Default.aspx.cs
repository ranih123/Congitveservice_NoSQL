using DataSearchLibrary;
using DataSearchLibrary.Helper;
using DataSearchLibrary.Interfaces;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            string NLPResult = "";

            string s = txtQuery.Text;
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
    }
}