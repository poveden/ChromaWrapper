using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace ChromaWrapper.Tests.Internal
{
    internal sealed class TestRegistryHive : IRegistryHive, IDisposable
    {
        private const string _namespace = nameof(ChromaWrapper);

        private readonly string _methodName;
        private readonly RegistryKey _parentKey;
        private readonly RegistryKey _readWriteKey;

        private bool _disposed;

        public TestRegistryHive([CallerMemberName] string? methodName = null)
        {
            ArgumentNullException.ThrowIfNull(methodName);

            _methodName = methodName;
            _parentKey = Registry.CurrentUser.CreateSubKey($@"SOFTWARE\Poveden\{_namespace}.Tests");
            _parentKey.DeleteSubKeyTree(_methodName, false);

            _readWriteKey = _parentKey.CreateSubKey(_methodName, true);
            Key = _parentKey.OpenSubKey(_methodName, false)!;
        }

        public RegistryKey Key { get; }

        public void SetValue(string path, string value)
        {
            using var subKey = EnsurePath(path, out string valueName);

            (subKey ?? _readWriteKey).SetValue(valueName, value, RegistryValueKind.String);
        }

        public void SetValue(string path, int value)
        {
            using var subKey = EnsurePath(path, out string valueName);

            (subKey ?? _readWriteKey).SetValue(valueName, value, RegistryValueKind.DWord);
        }

        public void SetValues(string path, IReadOnlyDictionary<string, object> values)
        {
            using var subKey = _readWriteKey.CreateSubKey(path, true);

            foreach ((string valueName, object value) in values)
            {
                var kind = value switch
                {
                    int => RegistryValueKind.DWord,
                    string => RegistryValueKind.String,
                    _ => throw new InvalidOperationException(),
                };

                subKey.SetValue(valueName, value, kind);
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Key.Dispose();
            _readWriteKey.Dispose();
            _parentKey.DeleteSubKeyTree(_methodName, false);
            _parentKey.Dispose();
            _disposed = true;
        }

        private RegistryKey? EnsurePath(string path, out string valueName)
        {
            int lastPathDelim = path.LastIndexOf('\\');

            if (lastPathDelim == -1)
            {
                valueName = path;
                return null;
            }

            string subPath = path[..lastPathDelim];
            valueName = path[(lastPathDelim + 1)..];
            return _readWriteKey.CreateSubKey(subPath, true);
        }
    }
}
