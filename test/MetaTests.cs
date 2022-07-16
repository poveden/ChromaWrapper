using System.Diagnostics;
using System.Text.RegularExpressions;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class MetaTests
    {
        [Fact]
        public void LgtmDotNetSdkVersionCannotBeLesserThanTheCurrentDotnetSdkVersion()
        {
            var currSdkVersion = GetCurrentDotNetSdkVersion();
            var lgtmSdkVersion = GetLgtmDotNetSdkVersion();

            Assert.False(lgtmSdkVersion < currSdkVersion);
        }

        private static Version GetCurrentDotNetSdkVersion()
        {
            using var process = new Process
            {
                StartInfo =
                {
                    FileName = "dotnet.exe",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                },
            };

            process.Start();
            process.WaitForExit(5000);

            string vStr = process.StandardOutput.ReadToEnd().Trim();
            return Version.Parse(vStr);
        }

        private static Version GetLgtmDotNetSdkVersion()
        {
            string solutionDir = MetaTestsCommon.GetSolutionDirectory();
            string path = Path.Combine(solutionDir, ".lgtm.yml");
            string lgtmStr = File.ReadAllText(path);

            var m = Regex.Match(lgtmStr, @"extraction:\s+csharp:\s+index:\s+dotnet:\s+version:\s*""([0-9.]+)""", RegexOptions.Multiline);
            Assert.True(m.Success);

            string vStr = m.Groups[1].Value;
            return Version.Parse(vStr);
        }
    }
}
