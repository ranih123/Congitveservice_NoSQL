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
            var queryParams = JsonConvert.DeserializeObject<Dictionary<string,string>>(queryParamJson);

            var tableSet= new HashSet<string>();
            var columnList = new List<ColumnInfo>();
            var queryType = "";

            //tableSet.Add("Employees");
            //tableSet.Add("Salaries");

            //columnList.Add(new ColumnInfo("name", "Nilepta", "AND", "EQUALS"));
            //columnList.Add(new ColumnInfo("salary", "12000", "","LT"));
            //columnList.Add(new ColumnInfo("city", "seattle", "", "EQUALS"));
            queryType = "Search";

            foreach (var item in queryParams)
            {
                if (item.Key.Equals("TableNames"))
                {
                    tableSet = JsonConvert.DeserializeObject<HashSet<string>>(item.Value);
                }
                else if (item.Key.Equals("ColumnValue"))
                {
                    columnList = JsonConvert.DeserializeObject<List<ColumnInfo>>(item.Value);
                }
                else if (item.Key.Equals("QueryType"))
                {
                    queryType = JsonConvert.DeserializeObject<string>(item.Value);
                }
            }

            // Fetch the result from inputJson 
            List<string> results = new List<string>();
            foreach (var item in inputJson)
            {
                JObject input = JObject.Parse(item);
                string previousOperator = "";
                foreach (var col in columnList)
                {
                    foreach (var  coldata in input.FindTokens(col.ColumnName))
                    {
                        string parentData = coldata.Path.Substring(0, coldata.Path.IndexOf('.'));
                        var parent = parentData.Split('[');
                        string propertyName = parent[0].ToString();
                        int index = Convert.ToInt16(parent[1].ToString().Substring(0, 1));
                        if (tableSet.Contains(propertyName))
                        {
                            if (col.ColumnValue.ToLower().Equals(((JValue)coldata).Value.ToString().ToLower()))
                            {
                                //(previousOperator.Equals("AND") && results.Contains(input.GetValue(propertyName)[index].ToString())) || 
                                if (!previousOperator.Equals("AND"))
                                    results.Add(input.GetValue(propertyName)[index].ToString());
                            }
                            else
                            {
                                if (previousOperator.Equals("AND") )
                                {
                                    results.Remove(input.GetValue(propertyName)[index].ToString());
                                }
                            }
                        }
                        
                    }
                    previousOperator = col.LogicalOperator;

                }
            }

            // Return result
            return results.Distinct();
        }
    }
}
