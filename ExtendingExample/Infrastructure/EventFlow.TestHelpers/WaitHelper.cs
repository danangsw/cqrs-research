using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers
{
    public static class WaitHelper
    {
        public static async Task WaitAsync(TimeSpan timeout, Func<Task<bool>> waitFor)
        {
            var start = DateTimeOffset.Now;
            while (start + timeout > DateTimeOffset.Now)
            {
                var result = await waitFor().ConfigureAwait(false);
                if (result) return;
                await Task.Delay(TimeSpan.FromSeconds(0.25)).ConfigureAwait(false);
            }

            throw new ApplicationException("Failed to wait");
        }
    }
}
