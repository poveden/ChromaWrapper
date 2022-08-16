namespace ChromaWrapper.Data
{
    /// <summary>
    /// Defines details of a Razer Chroma-enabled application.
    /// </summary>
    internal interface IChromaAppInfo
    {
        /// <summary>
        /// Gets the title of the application.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? Title { get; }

        /// <summary>
        /// Gets the description of the application.
        /// </summary>
        /// <remarks>Values of 1024 characters or more will be truncated.</remarks>
        public string? Description { get; }

        /// <summary>
        /// Gets the name of the author.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? AuthorName { get; }

        /// <summary>
        /// Gets the contact details of the author.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? AuthorContact { get; }

        /// <summary>
        /// Gets the devices supported by the application.
        /// </summary>
        public SupportedDevices SupportedDevice { get; }

        /// <summary>
        /// Gets the category of the application.
        /// </summary>
        public AppCategory Category { get; }
    }
}
