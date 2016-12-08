using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Commons.Extension
{
    public static class QueryBuilders
    {
        public static string[] CreateInQueryFromListId(this List<string> list)
        {
            return list.Distinct().ToArray();
        }
    }
}
