using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ChromaWrapper.Internal
{
    internal sealed class ChromaEffectMarshaler : ICustomMarshaler
    {
        [ThreadStatic]
        private static GCHandle _handle;

        public static ICustomMarshaler GetInstance(string? pstrCookie)
        {
            return new ChromaEffectMarshaler();
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            throw new NotSupportedException();
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            _handle.Free();
        }

        public int GetNativeDataSize()
        {
            return -1; // Reference: https://limbioliong.wordpress.com/2013/11/19/custom-marshaling-comments-and-an-example-use-of-the-marshal-cookie/
        }

        [SuppressMessage("Critical Code Smell", "S2696:Instance members should not write to \"static\" fields", Justification = "Thread-static field")]
        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            object obj = ManagedObj is IColorBuffer colorBuffer
                ? colorBuffer.Buffer
                : ManagedObj;

            _handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            return _handle.AddrOfPinnedObject();
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            throw new NotSupportedException();
        }
    }
}
