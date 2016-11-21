using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers.Installer
{
    public class SoftwareDescription
    {
        public static SoftwareDescription Create(
            string shortName,
            Version version,
            Uri downloadUri)
        {
            return new SoftwareDescription(shortName, version, downloadUri);
        }

        public static SoftwareDescription Create(
            string shortName,
            Version version,
            string downloadUri)
        {
            return Create(shortName, version, new Uri(downloadUri, UriKind.Absolute));
        }

        public string ShortName { get; }
        public Version Version { get; }
        public Uri DownloadUri { get; }

        private SoftwareDescription(
            string shortName,
            Version version,
            Uri downloadUri)
        {
            if (string.IsNullOrEmpty(shortName)) throw new ArgumentNullException(nameof(shortName));
            if (version == null) throw new ArgumentNullException(nameof(version));
            if (downloadUri == null) throw new ArgumentNullException(nameof(downloadUri));
            if (!downloadUri.IsAbsoluteUri) throw new ArgumentException($"'{downloadUri.OriginalString}' is not absolute");

            ShortName = shortName;
            Version = version;
            DownloadUri = downloadUri;
        }

        public override string ToString()
        {
            return $"{ShortName} v{Version}";
        }
    }
}
