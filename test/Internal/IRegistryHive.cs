using Microsoft.Win32;

namespace ChromaWrapper.Tests.Internal
{
    internal interface IRegistryHive
    {
        RegistryKey Key { get; }
    }
}
