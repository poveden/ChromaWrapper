namespace ChromaWrapper.Events
{
    /// <summary>
    /// Event arguments for the <see cref="ChromaSdk.ChromaSdkSupport"/> event.
    /// </summary>
    public sealed class ChromaSdkSupportEventArgs : EventArgs
    {
        internal ChromaSdkSupportEventArgs(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating whether Chroma SDK support has been enabled.
        /// </summary>
        public bool Enabled { get; }
    }
}
