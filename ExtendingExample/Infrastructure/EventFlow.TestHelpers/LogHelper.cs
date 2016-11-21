using EventFlow.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers
{
    public static class LogHelper
    {
        private static readonly Lazy<ILog> LazyLog = new Lazy<ILog>(() => new ConsoleLog());
        public static ILog Log => LazyLog.Value;
    }
}
