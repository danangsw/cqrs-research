using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson<T>(this T obj, bool indented = false)
        {
            return JsonConvert.SerializeObject(
                obj,
                indented ? Formatting.Indented : Formatting.None);
        }
    }
}
