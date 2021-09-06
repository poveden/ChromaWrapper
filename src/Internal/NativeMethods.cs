using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]

namespace ChromaWrapper.Internal
{
    [ExcludeFromCodeCoverage]
    internal sealed partial class NativeMethods
    {
        public static readonly NativeMethods Instance = new NativeMethods();

        private NativeMethods()
        {
        }
    }
}
