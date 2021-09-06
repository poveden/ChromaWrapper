using System;

namespace ChromaWrapper.Events
{
    /// <summary>
    /// Event arguments for the <see cref="ChromaSdk.DeviceAccess"/> event.
    /// </summary>
    public sealed class ChromaDeviceAccessEventArgs : EventArgs
    {
        internal ChromaDeviceAccessEventArgs(bool accessGranted)
        {
            AccessGranted = accessGranted;
        }

        /// <summary>
        /// Gets a value indicating whether access has been granted to the Chroma SDK.
        /// </summary>
        public bool AccessGranted { get; }
    }
}
