using ChromaWrapper.ChromaLink;
using ChromaWrapper.Data;
using ChromaWrapper.Events;
using ChromaWrapper.Headset;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Keypad;
using ChromaWrapper.Mouse;
using ChromaWrapper.Mousepad;

namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Defines the Razer Chroma SDK wrapper API.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/_rz_chroma_s_d_k_8h.html">RzChromaSDK.h File Reference</seealso>
    public interface IChromaSdk : IDisposable
    {
        /// <summary>
        /// Occurs when the Chroma SDK support has been enabled or disabled.
        /// </summary>
        event EventHandler<ChromaSdkSupportEventArgs>? ChromaSdkSupport;

        /// <summary>
        /// Occurs when device access has been granted or revoked.
        /// </summary>
        event EventHandler<ChromaDeviceAccessEventArgs>? DeviceAccess;

        /// <summary>
        /// Occurs when the application has been enabled or disabled.
        /// </summary>
        event EventHandler<ChromaApplicationStateEventArgs>? ApplicationState;

        /// <summary>
        /// Gets the collection of effect IDs created by the <c>CreateEffect()</c> methods.
        /// </summary>
        public IReadOnlySet<Guid> CreatedEffects { get; }

        /// <summary>
        /// Creates an effect for keyboards.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticKeyboardEffect"/></description></item>
        /// <item><description><see cref="CustomKeyboardEffect"/></description></item>
        /// <item><description><see cref="CustomKeyKeyboardEffect"/></description></item>
        /// <item><description><see cref="CustomKeyboardEffect2"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IKeyboardEffect effect);

        /// <summary>
        /// Creates an effect for mice.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticMouseEffect"/></description></item>
        /// <item><description><see cref="CustomMouseEffect2"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IMouseEffect effect);

        /// <summary>
        /// Creates an effect for headsets.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticHeadsetEffect"/></description></item>
        /// <item><description><see cref="CustomHeadsetEffect"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IHeadsetEffect effect);

        /// <summary>
        /// Creates an effect for Chroma Link devices.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticChromaLinkEffect"/></description></item>
        /// <item><description><see cref="CustomChromaLinkEffect"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IChromaLinkEffect effect);

        /// <summary>
        /// Creates an effect for keypads.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticKeypadEffect"/></description></item>
        /// <item><description><see cref="CustomKeypadEffect"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IKeypadEffect effect);

        /// <summary>
        /// Creates an effect for mousepads.
        /// </summary>
        /// <param name="effect">The effect.</param>
        /// <returns>The ID of the created effect.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="effect"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="effect"/> is invalid.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <remarks>
        /// Available effects are:
        /// <list type="bullet">
        /// <item><description><see cref="StaticMousepadEffect"/></description></item>
        /// <item><description><see cref="CustomMousepadEffect"/></description></item>
        /// <item><description><see cref="CustomMousepadEffect2"/></description></item>
        /// </list>
        /// </remarks>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="SetEffect"/>
        /// <seealso cref="DeleteEffect"/>
        Guid CreateEffect(IMousepadEffect effect);

        /// <summary>
        /// Applies the specified effect on the corresponding device.
        /// </summary>
        /// <param name="effectId">The effect ID, obtained from a call to <c>CreateEffect</c>.</param>
        /// <exception cref="UnauthorizedAccessException">No permission to access device.</exception>
        /// <exception cref="ArgumentException">Effect Id not found.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <seealso cref="CreatedEffects"/>
        /// <seealso cref="DeleteEffect"/>
        void SetEffect(Guid effectId);

        /// <summary>
        /// Deletes the specified effect.
        /// </summary>
        /// <param name="effectId">The effect ID, obtained from a call to <c>CreateEffect</c>.</param>
        /// <exception cref="ArgumentException">Effect Id not found.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        /// <seealso cref="DeleteAllEffects"/>
        void DeleteEffect(Guid effectId);

        /// <summary>
        /// Deletes all effects that were previously created with <c>CreateEffect()</c> methods.
        /// </summary>
        /// <exception cref="AggregateException">There was an error while deleting at least one effect.</exception>
        /// <seealso cref="DeleteEffect"/>
        void DeleteAllEffects();

        /// <summary>
        /// Queries for device information.
        /// </summary>
        /// <param name="deviceId">The device ID. <see cref="ChromaDeviceIds"/> defines a collection of device IDs.</param>
        /// <returns>The device information.</returns>
        /// <exception cref="ArgumentException">Device not supported.</exception>
        /// <exception cref="ChromaSdkException">The operation failed. Check <see cref="ChromaSdkException.Result"/> to retrieve the result code.</exception>
        ChromaDeviceInfo QueryDevice(Guid deviceId);
    }
}
