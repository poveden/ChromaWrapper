using System.Runtime.InteropServices;
using ChromaWrapper.Data;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaAppInfoTests
    {
        [Fact]
        public void TooLongStringsGetTruncatedOnMarshaling()
        {
            var ai = new ChromaAppInfo
            {
                Title = new string('t', 300),
                Description = new string('d', 1100),
                AuthorName = new string('n', 300),
                AuthorContact = new string('c', 300),
                Category = ChromaAppInfo.AppCategory.Game,
                SupportedDevice = ChromaAppInfo.SupportedDevices.All,
            };

            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(ai));

            try
            {
                Marshal.StructureToPtr(ai, ptr, false);
                var ai2 = Marshal.PtrToStructure<ChromaAppInfo>(ptr)!;

                string trTitle = ai.Title[0..255];
                string trDescription = ai.Description[0..1023];
                string trAuthorName = ai.AuthorName[0..255];
                string trAuthorContact = ai.AuthorContact[0..255];

                Assert.Equal(trTitle, ai2.Title);
                Assert.Equal(trDescription, ai2.Description);
                Assert.Equal(trAuthorName, ai2.AuthorName);
                Assert.Equal(trAuthorContact, ai2.AuthorContact);
                Assert.Equal(ai.Category, ai2.Category);
                Assert.Equal(ai.SupportedDevice, ai2.SupportedDevice);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
