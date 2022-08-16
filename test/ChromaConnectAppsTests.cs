using System.Diagnostics.CodeAnalysis;
using ChromaWrapper.Data;
using ChromaWrapper.Tests.Internal;
using ChromaWrapper.Tests.Internal.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace ChromaWrapper.Tests
{
    [SuppressMessage("Usage", "xUnit1033", Justification = "https://github.com/xunit/xunit/issues/2567")]
    public class ChromaConnectAppsTests : SdkTestBase, IClassFixture<TestRegistryFixture>
    {
        private const string _p = @"SOFTWARE\Razer Chroma SDK\Apps";

        public ChromaConnectAppsTests(TestRegistryFixture fixture, ITestOutputHelper testOutput)
            : base(testOutput)
        {
            Hive = IsNativeSdkTest
                ? new NativeRegistryHive()
                : fixture;
        }

        private IRegistryHive Hive { get; }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void ReadsValuesFromTheRegistry()
        {
            var apps = new ChromaConnectApps(Hive.Key);

            Assert.True(apps.Exists);
            Assert.True(apps.Enabled);
            Assert.False(apps.Active);
            Assert.NotEmpty(apps.EnumerateApps());
        }

        [SdkFact]
        public void ToleratesMissingRegistryEntry()
        {
            using var trh = new TestRegistryHive();

            var apps = new ChromaConnectApps(trh.Key);

            Assert.False(apps.Exists);
            Assert.False(apps.Enabled);
            Assert.False(apps.Active);
            Assert.Empty(apps.EnumerateApps());
        }

        [SdkFact]
        public void EnumerateAppsToleratesInconsistentPriorityList()
        {
            using var trh = new TestRegistryHive();
            TestRegistryFixture.ApplyValidConfiguration(trh);
            trh.SetValue($@"{_p}\PriorityList", "Another.exe;NonExisting.exe");

            var apps = new ChromaConnectApps(trh.Key);

            var expected = new SortedSet<string>(new[] { "Another.exe", "SomeApplication.exe" }, StringComparer.Ordinal);
            var appIds = new SortedSet<string>(apps.EnumerateApps().Select(GetAppId), StringComparer.Ordinal);

            Assert.Equal(expected, appIds);
        }

        [SdkFact]
        public void EnumerateAppsToleratesInvalidPriorityList()
        {
            using var trh = new TestRegistryHive();
            TestRegistryFixture.ApplyValidConfiguration(trh);
            trh.SetValue($@"{_p}\PriorityList", 123);

            var apps = new ChromaConnectApps(trh.Key);

            string[] expected = new[] { "Another.exe", "SomeApplication.exe" };
            string[] appIds = apps.EnumerateApps().Select(GetAppId).ToArray();

            Assert.Equal(expected, appIds);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void AppInfoReadsValuesFromTheRegistry()
        {
            var apps = new ChromaConnectApps(Hive.Key);
            var info = apps.EnumerateApps().First();

            Assert.True(info.Exists);
            Assert.NotNull(info.Title);
            Assert.NotNull(info.Description);
            Assert.NotNull(info.AuthorName);
            Assert.NotNull(info.AuthorContact);
            Assert.NotEqual(SupportedDevices.None, info.SupportedDevice);
            Assert.NotEqual(AppCategory.None, info.Category);
            Assert.NotNull(info.Path);
        }

        [SdkFact(AlsoTestOnNativeSdk = true)]
        public void AppInfoToleratesMissingRegistryEntry()
        {
            var info = new ChromaEnabledAppInfo("This-app-cannot-possibly-exist.exe", Hive.Key);

            Assert.False(info.Exists);
            Assert.Null(info.Title);
            Assert.Null(info.Description);
            Assert.Null(info.AuthorName);
            Assert.Null(info.AuthorContact);
            Assert.Equal(SupportedDevices.None, info.SupportedDevice);
            Assert.Equal(AppCategory.None, info.Category);
            Assert.Null(info.Path);
        }

        private static string GetAppId(ChromaEnabledAppInfo appInfo)
        {
            string? path = appInfo.GetPrivateField<string>("_path");
            int i = path!.LastIndexOf('\\');
            return path[(i + 1)..];
        }
    }
}
