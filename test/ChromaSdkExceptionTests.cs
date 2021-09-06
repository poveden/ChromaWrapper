using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using ChromaWrapper.Sdk;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaSdkExceptionTests
    {
        private static readonly ResourceManager _resources = typeof(ChromaSdkException).GetPrivateStaticField<ResourceManager>("_resources")!;

        [Fact]
        public void ImplementsStandardConstructors()
        {
            var ex = new ChromaSdkException();
            Assert.Equal(ChromaResult.Failed, ex.Result);
            Assert.Equal(_resources.GetString("Failed", CultureInfo.CurrentCulture), ex.Message);
            Assert.NotNull(ex.Message);

            ex = new ChromaSdkException("THE MESSAGE");
            Assert.Equal(ChromaResult.Failed, ex.Result);
            Assert.Equal("THE MESSAGE", ex.Message);

            var inner = new UnauthorizedAccessException();
            ex = new ChromaSdkException("THE MESSAGE", inner);
            Assert.Equal(ChromaResult.Success, ex.Result);
            Assert.Equal("THE MESSAGE", ex.Message);
            Assert.Same(inner, ex.InnerException);

            var ds = new DataContractSerializer(typeof(ChromaSdkException));
            using var ms = new MemoryStream();

            ex = new ChromaSdkException(ChromaResult.NoMoreItems, "SERIALIZABLE");
            ds.WriteObject(ms, ex);
            ms.Position = 0;
            var ex2 = (ChromaSdkException)ds.ReadObject(ms)!;
            Assert.Equal(ex.Message, ex2.Message);
            Assert.Equal(ex.Result, ex2.Result);
        }

        [Fact]
        public void CreatesMethodSpecificMessages()
        {
            var ex = ChromaSdkException.WithMethodName(ChromaResult.AlreadyInitialized, "Init");
            Assert.Equal(ChromaResult.AlreadyInitialized, ex.Result);
            Assert.Equal(_resources.GetString("AlreadyInitialized_Init", CultureInfo.CurrentCulture), ex.Message);
        }

        [Fact]
        public void UsesWin32ExceptionMessagesForUndefinedChromaResults()
        {
            var ex = new ChromaSdkException((ChromaResult)1);
            Assert.Equal((ChromaResult)1, ex.Result);
            Assert.Equal(new Win32Exception(1).Message, ex.Message);
        }
    }
}
