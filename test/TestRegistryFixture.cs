using ChromaWrapper.Data;
using ChromaWrapper.Tests.Internal;
using Microsoft.Win32;
using Xunit;

namespace ChromaWrapper.Tests
{
    public sealed class TestRegistryFixture : IRegistryHive, IDisposable, IAsyncLifetime
    {
        private readonly TestRegistryHive _hive;

        private bool _disposed;

        public TestRegistryFixture()
        {
            _hive = new TestRegistryHive();
            ApplyValidConfiguration(_hive);
        }

        public RegistryKey Key => _hive.Key;

        public Task InitializeAsync()
        {
            // Let the registry catch up, since native tests elsewhere will make changes in the registry (most notably, the "Active" flag).
            return Task.Delay(1000);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _hive.Dispose();
            _disposed = true;
        }

        internal static void ApplyValidConfiguration(TestRegistryHive hive)
        {
            const string p = @"SOFTWARE\Razer Chroma SDK\Apps";

            hive.SetValue($@"{p}\Active", 0);
            hive.SetValue($@"{p}\Enable", 1);
            hive.SetValue($@"{p}\PriorityList", "SomeApplication.exe;Another.exe");

            hive.SetValues($@"{p}\SomeApplication.exe", new Dictionary<string, object>
            {
                ["Title"] = "Some application",
                ["Description"] = "Some description",
                ["Author"] = "Some author",
                ["Contact"] = "https://some-author.example.com",
                ["DevicesSupported"] = (int)SupportedDevices.All,
                ["Category"] = (int)AppCategory.Utility,
                ["Enable"] = 1,
                ["Path"] = @"C:\SomeApplication\SomeApplication.exe",
            });

            hive.SetValues($@"{p}\Another.exe", new Dictionary<string, object>
            {
                ["Title"] = "Another application",
                ["Description"] = "Another description",
                ["Author"] = "Another author",
                ["Contact"] = "https://another.example.com",
                ["DevicesSupported"] = (int)SupportedDevices.Keyboard,
                ["Category"] = (int)AppCategory.Game,
                ["Enable"] = 0,
                ["Path"] = @"C:\Another\Another.exe",
            });
        }
    }
}
