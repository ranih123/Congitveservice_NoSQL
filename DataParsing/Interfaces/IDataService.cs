using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataParsing.Interfaces
{
    public interface IDataService
    {
        string GetData();

        string ParseData(string data);
    }
}
