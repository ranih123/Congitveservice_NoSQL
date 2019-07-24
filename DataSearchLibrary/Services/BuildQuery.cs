using DataSearchLibrary.Helper;
using DataSearchLibrary.Interfaces;
using DataSearchLibrary.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataSearchLibrary
{
    public class BuildQuery : IBuildQuery
    {
        public dynamic BuildQueryJson(string queryParamJson, string JsonSchema, List<string> inputJson)
        {
            // Parse queryParamJson to get the information
            //var queryParams = JsonConvert.DeserializeObject<Dictionary<string,string>>(queryParamJson);

            var tableSet= new HashSet<string>();
            var columnList = new List<ColumnInfo>();
            var queryType = "";

            tableSet.Add("Employees");

            columnList.Add(new ColumnInfo("city", "redmond", "Equal"));
            columnList.Add(new ColumnInfo("city", "seattle", "Equal"));
            queryType = "Search";

            //foreach (var item in queryParams)
            //{
            //    if (item.Key.Equals("TableNames"))
            //    {
            //        tableSet = JsonConvert.DeserializeObject<HashSet<string>>(item.Value);
            //    }
            //    else if (item.Key.Equals("ColumnValue"))
            //    {
            //        columnList = JsonConvert.DeserializeObject<List<ColumnInfo>>(item.Value);
            //    }
            //    else if (item.Key.Equals("QueryType"))
            //    {
            //        queryType = JsonConvert.DeserializeObject<string>(item.Value);
            //    }
            //}

            // Fetch the result from inputJson 
            List<string> results = new List<string>();
            foreach (var item in inputJson)
            {
                JObject input = JObject.Parse(item);
                foreach (var col in columnList)
                {
                    foreach (var  coldata in input.FindTokens(col.ColumnName))
                    {
                        if (col.ColumnValue.ToLower().Equals(((JValue)coldata).Value.ToString().ToLower()))
                        {

                            string parentData = coldata.Path.Substring(0,coldata.Path.IndexOf('.'));
                            var parent = parentData.Split('[');
                            string propertyName = parent[0].ToString();

                            if (tableSet.Contains(propertyName))
                            {
                                int index = Convert.ToInt16(parent[1].ToString().Substring(0, 1));
                                results.Add(input.GetValue(propertyName)[index].ToString());
                            }                          
                        }
                        
                    }
                }
            }

            // Return result
            return results;
        }
    }
}
