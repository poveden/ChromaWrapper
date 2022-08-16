using Microsoft.Win32;

namespace ChromaWrapper.Tests.Internal
{
    internal sealed class NativeRegistryHive : IRegistryHive
    {
        private static readonly RegistryKey _hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

        public RegistryKey Key => _hklm;
    }
}
