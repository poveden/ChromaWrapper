using System.Diagnostics.CodeAnalysis;
using ChromaWrapper.Data;
using ChromaWrapper.Internal;
using Microsoft.Win32;

namespace ChromaWrapper
{
    /// <summary>
    /// Exposes the status of Razer Chroma SDK's connected applications.
    /// </summary>
    public sealed class ChromaConnectApps
    {
        internal const string RegistryPath = @"SOFTWARE\Razer Chroma SDK\Apps";

        private static readonly RegistryKey _defaultHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

        private readonly RegistryKey _hive;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaConnectApps"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public ChromaConnectApps()
            : this(_defaultHive)
        {
        }

        internal ChromaConnectApps(RegistryKey hive)
        {
            _hive = hive;
            Refresh();
        }

        /// <summary>
        /// Gets a value indicating whether the Chroma SDK is being in use by a registered application.
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the Chroma SDK is enabled.
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the status represented by this <see cref="ChromaConnectApps"/> instance is available.
        /// </summary>
        public bool Exists { get; private set; }

        /// <summary>
        /// Refreshes the state of this <see cref="ChromaConnectApps"/> instance.
        /// </summary>
        public void Refresh()
        {
            using RegistryKey? key = _hive.OpenSubKey(RegistryPath, false);

            Exists = key != null;
            Active = (key?.GetDword("Active") ?? 0) != 0;
            Enabled = (key?.GetDword("Enable") ?? 0) != 0;
        }

        /// <summary>
        /// Returns an enumerable collection of connected applications.
        /// </summary>
        /// <returns>The enumerable collection of connected applications.</returns>
        public IEnumerable<ChromaEnabledAppInfo> EnumerateApps()
        {
            using RegistryKey? key = _hive.OpenSubKey(RegistryPath, false);

            if (key == null)
            {
                yield break;
            }

            var appIds = new HashSet<string>(key.GetSubKeyNames(), StringComparer.OrdinalIgnoreCase);
            string[] appOrd = key.GetString("PriorityList")?.Split(';') ?? Array.Empty<string>();

            foreach (string appId in appOrd.Where(appIds.Remove))
            {
                yield return new ChromaEnabledAppInfo(appId, _hive);
            }

            foreach (string appId in appIds)
            {
                yield return new ChromaEnabledAppInfo(appId, _hive);
            }
        }
    }
}
