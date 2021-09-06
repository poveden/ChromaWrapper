using System;

namespace ChromaWrapper.Events
{
    /// <summary>
    /// Event arguments for the <see cref="ChromaSdk.ApplicationState"/> event.
    /// </summary>
    public sealed class ChromaApplicationStateEventArgs : EventArgs
    {
        internal ChromaApplicationStateEventArgs(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating whether the current application has been enabled at SDK level.
        /// </summary>
        public bool Enabled { get; }
    }
}
