using ChromaWrapper.Tests.Internal.Xunit;
using Xunit.Abstractions;

namespace ChromaWrapper.Tests
{
    public abstract class SdkTestBase
    {
        protected SdkTestBase(ITestOutputHelper testOutput)
        {
            var test = testOutput.GetTest();
            var sdkTestCase = test.TestCase as SdkTestCase;

            IsNativeSdkTest = sdkTestCase?.IsNativeSdkTest ?? false;
        }

        protected bool IsNativeSdkTest { get; }
    }
}
