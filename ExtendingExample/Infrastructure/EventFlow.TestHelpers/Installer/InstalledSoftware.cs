using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Installer
{
    public class InstalledSoftware
    {
        internal InstalledSoftware(
            SoftwareDescription description,
            string installPath)
        {
            if (description == null) throw new ArgumentNullException(nameof(description));
            if (string.IsNullOrEmpty(installPath)) throw new ArgumentNullException(nameof(installPath));

            Description = description;
            InstallPath = installPath;
        }

        public SoftwareDescription Description { get; }
        public string InstallPath { get; }
    }
}
