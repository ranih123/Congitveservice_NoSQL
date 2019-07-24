using DataParsing.Interfaces;
using DataParsing.Models;
using DataParsing.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataParsing.Services
{
    public class DataService : IDataService
    {
        public string GetData()
        {
            return DataSourceRepository.GetDataFromSource();
        }

        public string ParseData(string data)
        {
           var parseDic = new Dictionary<string, HashSet<string>>();

            #region Commented
            List<Schema> schemaList = new List<Schema>();

            // Deserialize for level 0
            Parsing(data, parseDic, "","", 0);
            

            return JsonConvert.SerializeObject(parseDic);
            #endregion
        }

        public void Parsing(string data, Dictionary<string, HashSet<string>> parseDic, string currentParent, string subParent, int level)
        {
            
            if (data[0] != '{' && data[0] != '[')
            {
                return;
            }
            else if (data[0] == '[' && data[data.Length -1] == ']')
            {
                 var level0_Deserialize_List = JsonConvert.DeserializeObject<List<object>>(data);

                foreach (var listItem in level0_Deserialize_List)
                {
                    var l1_deserialize = JsonConvert.DeserializeObject<Dictionary<string, object>>(listItem.ToString());
                    foreach (var item in l1_deserialize)
                    {
                        var key = item.Key;
                        var value = item.Value;

                        if (level == 0)
                        {
                            currentParent = key;
                            subParent += key;
                        }
                        else
                        {
                            subParent += "." + key;
                        }
                        if (!parseDic.ContainsKey(currentParent))
                        {
                            parseDic.Add(currentParent, new HashSet<string>());
                        }
                        else
                        {
                            key = subParent;
                            parseDic[currentParent].Add(key);
                        }

                        level++;
                        var valueSerialize = JsonConvert.SerializeObject(value);
                        Parsing(valueSerialize, parseDic, currentParent, subParent, level);
                        int lastDotIndex = subParent.LastIndexOf('.');
                        if (lastDotIndex != -1)
                            subParent = subParent.Substring(0, subParent.LastIndexOf('.'));
                        level--;
                    }
                }
            }
            else
            {
                 var level0_Deserialize_Dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                foreach (var item in level0_Deserialize_Dic)
                {
                    var key = item.Key;
                    var value = item.Value;
                    
                    if (level == 0)
                    {
                        currentParent = key;
                        subParent += key;
                    }
                    else
                    {
                        subParent += "." + key;
                    }
                    if (!parseDic.ContainsKey(currentParent))
                    {
                        parseDic.Add(currentParent, new HashSet<string>());
                    }
                    else
                    {
                        key = subParent;
                        parseDic[currentParent].Add(key);
                    }

                    level++;
                    var valueSerialize = JsonConvert.SerializeObject(value);
                    Parsing(valueSerialize, parseDic, currentParent, subParent, level);
                    int lastDotIndex = subParent.LastIndexOf('.');
                    if (lastDotIndex != -1)
                        subParent = subParent.Substring(0, subParent.LastIndexOf('.'));
                    level--;
                }
            } 
            
        }
    }
}
