using Xunit;
using Xunit.Sdk;

namespace ChromaWrapper.Tests.Internal.Xunit
{
    [XunitTestCaseDiscoverer("ChromaWrapper.Tests.Internal.Xunit.SdkFactDiscoverer", "ChromaWrapper.Tests")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal sealed class SdkFactAttribute : FactAttribute
    {
        public bool AlsoTestOnNativeSdk { get; set; }
    }
}
