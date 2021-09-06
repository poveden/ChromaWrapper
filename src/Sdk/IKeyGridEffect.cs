namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Represents an effect with a key-addressable LED grid.
    /// </summary>
    public interface IKeyGridEffect
    {
        /// <summary>
        /// Gets the key-addressable LED color grid.
        /// </summary>
        public IKeyGrid Key { get; }
    }
}
