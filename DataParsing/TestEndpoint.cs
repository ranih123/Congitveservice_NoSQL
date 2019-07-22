using DataParsing.Interfaces;
using DataParsing.Services;

namespace DataParsing
{
    public class TestEndpoint
    {
        public string TestGetData()
        {
            IDataService dataService = new DataService();
            string data = dataService.GetData();
            string parseResponse = dataService.ParseData(data);
            return parseResponse;
        }
    }
}
