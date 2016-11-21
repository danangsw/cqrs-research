using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers
{
    public sealed class Categories
    {
        /// <summary>
        /// Tests that use external systems
        /// </summary>
        public const string Integration = "integration";

        /// <summary>
        /// Tests that test a single class
        /// </summary>
        public const string Unit = "unit";
    }
}
