using Microsoft.Win32;

namespace ChromaWrapper.Internal
{
    internal static class RegistryKeyExtensions
    {
        public static int? GetDword(this RegistryKey key, string? name)
        {
            return key.GetValue(name) as int?;
        }

        public static string? GetString(this RegistryKey key, string? name)
        {
            return key.GetValue(name) as string;
        }
    }
}
