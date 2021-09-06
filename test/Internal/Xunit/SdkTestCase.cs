using System;
using System.ComponentModel;
using Xunit.Abstractions;
using Xunit.Sdk;
using static ChromaWrapper.Internal.NativeMethods;

namespace ChromaWrapper.Tests.Internal.Xunit
{
    internal sealed class SdkTestCase : XunitTestCase
    {
        private static readonly bool _isSdkAvailable = Instance.IsSdkAvailable();

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public SdkTestCase()
        {
        }

        public SdkTestCase(bool isNativeSdkTest, IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, TestMethodDisplayOptions defaultMethodDisplayOptions, ITestMethod testMethod, object?[]? testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, defaultMethodDisplayOptions, testMethod, testMethodArguments)
        {
            IsNativeSdkTest = isNativeSdkTest;
            UpdateProperties();
        }

        public bool IsNativeSdkTest { get; set; }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);

            data.AddValue(nameof(IsNativeSdkTest), IsNativeSdkTest);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);

            IsNativeSdkTest = data.GetValue<bool>(nameof(IsNativeSdkTest));
            UpdateProperties();
        }

        protected override string GetUniqueID()
        {
            return $"{base.GetUniqueID()}{(IsNativeSdkTest ? "01" : "00")}";
        }

        private void UpdateProperties()
        {
            DisplayName = (IsNativeSdkTest ? "[Native] " : string.Empty) + DisplayName;

            if (IsNativeSdkTest && !_isSdkAvailable)
            {
                SkipReason = "Native SDK is not available.";
            }
        }
    }
}
