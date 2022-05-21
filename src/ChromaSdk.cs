using System.Diagnostics.CodeAnalysis;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Events;
using ChromaWrapper.Headset;
using ChromaWrapper.Internal;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;
using ChromaWrapper.Sdk;

namespace ChromaWrapper
{
    /// <summary>
    /// Exposes the Razer Chroma SDK.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/_rz_chroma_s_d_k_8h.html">RzChromaSDK.h File Reference</seealso>
    public sealed class ChromaSdk : IChromaSdk
    {
        private readonly IChromaSdkApi _chromaSdkApi;
        private readonly MessageListener? _messageListener;
        private readonly HashSet<Guid> _createdEffects;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdk"/> class with an optional application info.
        /// </summary>
        /// <param name="appInfo">The information of the running application, or <c>null</c> if this information should be taken from the <c>ChromaAppInfo.xml</c> file.</param>
        /// <remarks>
        /// If <paramref name="appInfo"/> is <c>null</c>, a mandatory <c>ChromaAppInfo.xml</c> must be deployed to the end user and placed in the same directory as your application.
        /// Make sure that it has all the fields filled with the necessary information.
        /// </remarks>
        [ExcludeFromCodeCoverage]
        public ChromaSdk(ChromaAppInfo? appInfo = null)
            : this(appInfo, false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdk"/> class with an optional application info.
        /// </summary>
        /// <param name="appInfo">The information of the running application, or <c>null</c> if this information should be taken from the <c>ChromaAppInfo.xml</c> file.</param>
        /// <param name="suppressEvents">A value indicating whether events from the SDK will be suppressed.</param>
        /// <remarks>
        /// <para>
        /// If <paramref name="appInfo"/> is <c>null</c>, a mandatory <c>ChromaAppInfo.xml</c> file must be
        /// deployed to the end user and placed in the same directory as your application.
        /// Make sure that it has all the fields filled with the necessary information.
        /// </para>
        /// <para>
        /// If <paramref name="suppressEvents"/> is <c>true</c>, the <see cref="ChromaSdkSupport"/>,
        /// <see cref="DeviceAccess"/> and <see cref="ApplicationState"/> won't be raised from SDK notifications.
        /// </para>
        /// </remarks>
        [ExcludeFromCodeCoverage]
        public ChromaSdk(ChromaAppInfo? appInfo, bool suppressEvents)
            : this(appInfo, suppressEvents, null)
        {
        }

        internal ChromaSdk(ChromaAppInfo? appInfo, bool suppressEvents, IChromaSdkApi? chromaSdkApi)
        {
            _chromaSdkApi = chromaSdkApi ?? NativeMethods.Instance;

            ChromaResult res = appInfo != null ? _chromaSdkApi.InitSDK(appInfo) : _chromaSdkApi.Init();
            ThrowIfError(res, nameof(ChromaSdk), null); // Any errors won't be triggered by appInfo contents.

            _createdEffects = new HashSet<Guid>();

            if (suppressEvents)
            {
                return;
            }

            _messageListener = new MessageListener(NativeMethods.Instance, HandleMessage);

            try
            {
                res = _chromaSdkApi.RegisterEventNotification(_messageListener.HWnd);
                ThrowIfError(res, nameof(ChromaSdk));
            }
            catch
            {
                _messageListener.Dispose();
                throw;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<ChromaSdkSupportEventArgs>? ChromaSdkSupport;

        /// <inheritdoc/>
        public event EventHandler<ChromaDeviceAccessEventArgs>? DeviceAccess;

        /// <inheritdoc/>
        public event EventHandler<ChromaApplicationStateEventArgs>? ApplicationState;

        /// <inheritdoc/>
        public IReadOnlySet<Guid> CreatedEffects => _createdEffects;

        /// <summary>
        /// Gets a value indicating whether the Razer Chroma SDK is available.
        /// </summary>
        /// <returns><c>true</c> is the SDK is available; otherwise, <c>false</c>.</returns>
        [ExcludeFromCodeCoverage]
        public static bool IsSdkAvailable()
        {
            return IsSdkAvailable(NativeMethods.Instance);
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IKeyboardEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateKeyboardEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IMouseEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateMouseEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IHeadsetEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateHeadsetEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IChromaLinkEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateChromaLinkEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IKeypadEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateKeypadEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        public Guid CreateEffect(IMousepadEffect effect)
        {
            _ = effect ?? throw new ArgumentNullException(nameof(effect));

            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.CreateMousepadEffect(effect.EffectType, effect, out Guid effectId);
            ThrowIfError(res, nameof(CreateEffect), nameof(effect));

            _ = _createdEffects.Add(effectId);
            return effectId;
        }

        /// <inheritdoc/>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="DeleteEffect"/>
        public void SetEffect(Guid effectId)
        {
            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.SetEffect(effectId);
            ThrowIfError(res, nameof(SetEffect), nameof(effectId));
        }

        /// <inheritdoc/>
        /// <seealso cref="DeleteAllEffects"/>
        public void DeleteEffect(Guid effectId)
        {
            ThrowIfDisposed();
            ChromaResult res = _chromaSdkApi.DeleteEffect(effectId);
            _ = _createdEffects.Remove(effectId);
            ThrowIfError(res, nameof(DeleteEffect), nameof(effectId));
        }

        /// <inheritdoc/>
        /// <seealso cref="DeleteEffect"/>
        public void DeleteAllEffects()
        {
            var exs = new List<Exception>();

            _ = _createdEffects.RemoveWhere(effectId =>
            {
                ChromaResult res = _chromaSdkApi.DeleteEffect(effectId);
                Exception? ex = BuildExceptionIfError(res, nameof(DeleteAllEffects));

                if (ex != null)
                {
                    ex.Data.Add(nameof(effectId), effectId);
                    exs.Add(ex);
                }

                return true;
            });

            if (exs.Count != 0)
            {
                throw new AggregateException(exs);
            }
        }

        /// <inheritdoc/>
        public ChromaDeviceInfo QueryDevice(Guid deviceId)
        {
            ThrowIfDisposed();

            var deviceInfo = new ChromaDeviceInfo();
            ChromaResult res = _chromaSdkApi.QueryDevice(deviceId, deviceInfo);

            if (res == ChromaResult.DeviceNotConnected)
            {
                return deviceInfo;
            }

            ThrowIfError(res, nameof(QueryDevice), nameof(deviceId));

            return deviceInfo;
        }

        /// <summary>
        /// Releases all resources allocated by this object.
        /// </summary>
        /// <remarks>
        /// Any created effects are also deleted.
        /// </remarks>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_messageListener != null)
            {
                _ = _chromaSdkApi.UnregisterEventNotification();
                _messageListener.Dispose();
            }

            _ = _chromaSdkApi.UnInit();
            _createdEffects.Clear(); // UnInit() frees all memory occupied by effects, so we only need to clear the list.
            _disposed = true;
        }

        internal static bool IsSdkAvailable(IChromaSdkApi chromaSdkApi)
        {
            return chromaSdkApi.IsSdkAvailable();
        }

        private static void ThrowIfError(ChromaResult res, string methodName, string? paramName = null)
        {
            Exception? ex = BuildExceptionIfError(res, methodName, paramName);

            if (ex != null)
            {
                throw ex;
            }
        }

        private static Exception? BuildExceptionIfError(ChromaResult res, string methodName, string? paramName = null)
        {
            if (res == ChromaResult.Success)
            {
                return null;
            }

            Exception ex = ChromaSdkException.WithMethodName(res, methodName);

#pragma warning disable IDE0072
            ex = res switch
            {
                ChromaResult.InvalidParameter
                or ChromaResult.NotSupported
                or ChromaResult.NotFound
                or ChromaResult.DeviceNotAvailable
                or ChromaResult.AlreadyInitialized
                when paramName != null => new ArgumentException(ex.Message, paramName, ex),

                ChromaResult.NotValidState
                or ChromaResult.AlreadyInitialized => new InvalidOperationException(ex.Message, ex),

                ChromaResult.AccessDenied => new UnauthorizedAccessException(ex.Message, ex),

                _ => ex,
            };
#pragma warning restore IDE0072

            return ex;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        private bool HandleMessage(uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg != NativeMethods.WM_CHROMA_EVENT)
            {
                return false;
            }

            switch ((NativeMethods.ChromaEvent)wParam.ToInt32())
            {
                case NativeMethods.ChromaEvent.ChromaSdkSupport:
                    OnChromaSdkSupport(lParam != IntPtr.Zero);
                    return true;
                case NativeMethods.ChromaEvent.DeviceAccess:
                    OnDeviceAccess(lParam != IntPtr.Zero);
                    return true;
                case NativeMethods.ChromaEvent.ApplicationState:
                    OnApplicationState(lParam != IntPtr.Zero);
                    return true;
                default:
                    return false;
            }
        }

        [ExcludeFromCodeCoverage]
        private void OnChromaSdkSupport(bool enabled)
        {
            ChromaSdkSupport?.Invoke(this, new ChromaSdkSupportEventArgs(enabled));
        }

        [ExcludeFromCodeCoverage]
        private void OnDeviceAccess(bool accessGranted)
        {
            DeviceAccess?.Invoke(this, new ChromaDeviceAccessEventArgs(accessGranted));
        }

        [ExcludeFromCodeCoverage]
        private void OnApplicationState(bool enabled)
        {
            ApplicationState?.Invoke(this, new ChromaApplicationStateEventArgs(enabled));
        }
    }
}
