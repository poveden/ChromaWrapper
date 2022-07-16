using System.Runtime.InteropServices;
using ChromaWrapper.Internal;
using ChromaWrapper.Tests.Internal;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaEffectMarshalerTests
    {
        [Fact]
        public void UnmanagedToManagedIsNotSupported()
        {
            var marshaler = ChromaEffectMarshaler.GetInstance(null)!;

            object o = new object();
            var ptr = Marshal.AllocHGlobal(10);

            try
            {
                Assert.Throws<NotSupportedException>(() => marshaler.MarshalNativeToManaged(ptr));
                Assert.Throws<NotSupportedException>(() => marshaler.CleanUpManagedData(o));
                Assert.Equal(-1, marshaler.GetNativeDataSize());
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        [Theory]
        [MemberData(nameof(GetStaticEffectTypes))]
        public void StaticEffectIsMarshaledByPinningTheEffectObject(Type type)
        {
            object o = Activator.CreateInstance(type)!;

            var marshaler = ChromaEffectMarshaler.GetInstance(null);

            var ptr = marshaler.MarshalManagedToNative(o);

            try
            {
                var handle = typeof(ChromaEffectMarshaler).GetPrivateStaticField<GCHandle>("_handle");
                var expected = handle.AddrOfPinnedObject();
                Assert.Equal(expected, ptr);
                Assert.Same(o, handle.Target);
            }
            finally
            {
                marshaler.CleanUpNativeData(ptr);
            }
        }

        [Theory]
        [MemberData(nameof(GetCustomEffectTypes))]
        public void CustomEffectIsMarshaledByPinningItsColorBuffer(Type type)
        {
            object o = Activator.CreateInstance(type)!;

            var marshaler = ChromaEffectMarshaler.GetInstance(null);

            var ptr = marshaler.MarshalManagedToNative(o);

            try
            {
                var handle = typeof(ChromaEffectMarshaler).GetPrivateStaticField<GCHandle>("_handle");
                var expected = handle.AddrOfPinnedObject();
                Assert.Equal(expected, ptr);

                var cb = ((IColorBuffer)o).Buffer;
                Assert.Same(cb, handle.Target);
            }
            finally
            {
                marshaler.CleanUpNativeData(ptr);
            }
        }

        private static IEnumerable<object[]> GetStaticEffectTypes()
        {
            return EffectTests.GetStaticEffectTypes();
        }

        private static IEnumerable<object[]> GetCustomEffectTypes()
        {
            return EffectTests.GetCustomEffectTypes();
        }
    }
}
