using Xunit.Abstractions;
using Xunit.Sdk;

namespace ChromaWrapper.Tests.Internal.Xunit
{
    internal sealed class SdkFactDiscoverer : IXunitTestCaseDiscoverer
    {
        private readonly IMessageSink _diagnosticMessageSink;

        public SdkFactDiscoverer(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            yield return new SdkTestCase(false, _diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);

            bool alsoTestOnNativeSdk = factAttribute.GetNamedArgument<bool>(nameof(SdkFactAttribute.AlsoTestOnNativeSdk));

            if (alsoTestOnNativeSdk)
            {
                yield return new SdkTestCase(true, _diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod);
            }
        }
    }
}
