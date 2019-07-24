using System;
using System.Collections.Generic;
using System.Text;

namespace DataSearchLibrary.Interfaces
{
    public interface IBuildQuery
    {
        dynamic BuildQueryJson(string queryParamJson, string JsonSchema, List<string> inputJson);
    }
}
