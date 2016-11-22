using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Extensions
{
    public static class QueryProcessorExtensions
    {
        public static Task<TResult> ProcessAsync<TResult>(
            this IQueryProcessor queryProcessor,
            IQuery<TResult> query)
        {
            return queryProcessor.ProcessAsync(query, CancellationToken.None);
        }
    }
}
