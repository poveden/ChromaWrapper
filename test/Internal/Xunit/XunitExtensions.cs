using System.Reflection;
using Xunit.Abstractions;

namespace ChromaWrapper.Tests.Internal.Xunit
{
    internal static class XunitExtensions
    {
        public static ITest GetTest(this ITestOutputHelper output)
        {
            // Reference: https://github.com/xunit/xunit/issues/416#issuecomment-378512739
            var res = (ITest?)output.GetType()
                .GetField("test", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(output);

            if (res == null)
            {
                throw new NotSupportedException();
            }

            return res;
        }
    }
}
