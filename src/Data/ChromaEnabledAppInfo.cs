using ChromaWrapper.Internal;
using Microsoft.Win32;

namespace ChromaWrapper.Data
{
    /// <summary>
    /// Represents a Razer Chroma-enabled application.
    /// </summary>
    public sealed class ChromaEnabledAppInfo : IChromaAppInfo
    {
        private readonly RegistryKey _hive;
        private readonly string _path;

        internal ChromaEnabledAppInfo(string appId, RegistryKey hive)
        {
            _hive = hive;
            _path = string.Join('\\', ChromaConnectApps.RegistryPath, appId);

            Refresh();
        }

        /// <inheritdoc/>
        public string? Title { get; private set; }

        /// <inheritdoc/>
        public string? Description { get; private set; }

        /// <inheritdoc/>
        public string? AuthorName { get; private set; }

        /// <inheritdoc/>
        public string? AuthorContact { get; private set; }

        /// <inheritdoc/>
        public SupportedDevices SupportedDevice { get; private set; }

        /// <inheritdoc/>
        public AppCategory Category { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the application is enabled to produce effects on the Razer Chroma SDK.
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets the path of the application's main executable file.
        /// </summary>
        public string? Path { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the application represented by this <see cref="ChromaEnabledAppInfo"/> instance exists as a connected application.
        /// </summary>
        public bool Exists { get; private set; }

        /// <summary>
        /// Refreshes the state of this <see cref="ChromaEnabledAppInfo"/> instance.
        /// </summary>
        public void Refresh()
        {
            using RegistryKey? key = _hive.OpenSubKey(_path, false);

            Exists = key != null;
            Title = key?.GetString("Title");
            Description = key?.GetString("Description");
            AuthorName = key?.GetString("Author");
            AuthorContact = key?.GetString("Contact");
            SupportedDevice = (SupportedDevices)(key?.GetDword("DevicesSupported") ?? 0);
            Category = (AppCategory)(key?.GetDword("Category") ?? 0);
            Enabled = (key?.GetDword("Enable") ?? 0) != 0;
            Path = key?.GetString("Path");
        }
    }
}
