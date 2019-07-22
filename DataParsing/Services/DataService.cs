using DataParsing.Interfaces;
using DataParsing.Models;
using DataParsing.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            List<Schema> schemaList = new List<Schema>();

            // Deserialize for level 0
            var level0_Deserialize = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            foreach (var item in level0_Deserialize)
            {
                string entity = item.Key;

                // Deserialize for level 1
                var serializeJson = JsonConvert.SerializeObject(item.Value);
                var level1_Deserialize = JsonConvert.DeserializeObject<List<object>>(serializeJson);

                foreach (var obj in level1_Deserialize)
                {
                    // Deserialize for level 2
                    var level2_Deserialize = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());
                    foreach (var prop in level2_Deserialize)
                    {
                        Schema schema = new Schema
                        {
                            Entity = entity,
                            ColumnName = prop.Key,
                            ColumnValue = Convert.ToString(prop.Value)
                        };
                        schemaList.Add(schema);

                    }

                }
            }
          
            return JsonConvert.SerializeObject(schemaList);
        }
    }
}
