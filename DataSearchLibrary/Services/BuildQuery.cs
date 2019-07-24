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
            var queryParams = JsonConvert.DeserializeObject<Dictionary<string,object>>(queryParamJson);

            var tableSet= new HashSet<string>();
            var columnList = new List<ColumnInfo>();
            var queryType = "";

            //tableSet.Add("Error");
            //tableSet.Add("Salaries");

            //columnList.Add(new ColumnInfo("type", "authentication", "OR", "EQUALS"));
            //columnList.Add(new ColumnInfo("type", "authorization", "AND", "EQUALS"));
            //columnList.Add(new ColumnInfo("severity", "4", "", "LESSTHAN"));
            // queryType = "Search";

            foreach (var item in queryParams)
            {
                if (item.Key.Equals("TableNames"))
                {
                    tableSet = JsonConvert.DeserializeObject<HashSet<string>>(item.Value.ToString().ToUpper());
                }
                else if (item.Key.Equals("Columns"))
                {
                    columnList = JsonConvert.DeserializeObject<List<ColumnInfo>>(item.Value.ToString().ToUpper());
                }
                else if (item.Key.Equals("QueryType"))
                {
                    queryType = JsonConvert.DeserializeObject<string>(item.Value.ToString().ToUpper());
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
                    foreach (var  coldata in input.FindTokens(col.ColumnName.ToLower()))
                    {
                        string parentData = coldata.Path.Substring(0, coldata.Path.IndexOf('.'));
                        var parent = parentData.Split('[');
                        string propertyName = parent[0].ToString();
                        bool bAdd = false;
                        int index = Convert.ToInt16(parent[1].ToString().Substring(0, 1));
                        if (tableSet.Contains(propertyName.ToUpper()))
                        {
                            switch(col.Comparator.ToUpper())
                            {
                                case "EQUAL TO":
                                    if (col.ColumnValue.ToUpper().Equals(((JValue)coldata).Value.ToString().ToUpper()))
                                    {
                                        bAdd = true;
                                    }
                                    break;
                                case "LESS THAN":

                                    if (int.TryParse(col.ColumnValue, out int r))
                                    {
                                        if (Convert.ToInt32(((JValue)coldata).Value).IsLessThan(r))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    else if (DateTime.TryParse(col.ColumnValue, out DateTime n))
                                    {
                                        if (Convert.ToDateTime(((JValue)coldata).Value).DateIsLessThan(n))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                 break;
                                case "GREATER THAN":
                                    if (int.TryParse(col.ColumnValue, out int a))
                                    {
                                        if (Convert.ToInt32(((JValue)coldata).Value).IsGreaterThan(a))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    else if (DateTime.TryParse(col.ColumnValue, out DateTime n))
                                    {
                                        if (Convert.ToDateTime(((JValue)coldata).Value).DateIsGreaterThan(n))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    break;
                                case "LESS THAN EQUAL":
                                    if (int.TryParse(col.ColumnValue, out int b))
                                    {
                                        if (Convert.ToInt32(((JValue)coldata).Value).IsLessThanEqual(b))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    else if (DateTime.TryParse(col.ColumnValue, out DateTime n))
                                    {
                                        if (Convert.ToDateTime(((JValue)coldata).Value).DateIsLessThanEqual(n))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    break;
                                case "GREATER THAN EQUAL":
                                    if (int.TryParse(col.ColumnValue, out int c))
                                    {
                                        if (Convert.ToInt32(((JValue)coldata).Value).IsGreaterThanEqual(c))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    else if (DateTime.TryParse(col.ColumnValue, out DateTime n))
                                    {
                                        if (Convert.ToDateTime(((JValue)coldata).Value).DateIsGreaterThanEqual(n))
                                        {
                                            bAdd = true;
                                        }
                                    }
                                    break;
                               
                            }

                           


                            if (bAdd)
                            {
                                //(previousOperator.Equals("AND") && results.Contains(input.GetValue(propertyName)[index].ToString())) || 
                                if (!previousOperator.ToUpper().Equals("AND"))
                                    results.Add(input.GetValue(propertyName)[index].ToString());
                            }
                            else
                            {
                                if (previousOperator.ToUpper().Equals("AND"))
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
