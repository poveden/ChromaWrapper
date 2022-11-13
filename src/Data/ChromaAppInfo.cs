using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ChromaWrapper.Data
{
    /// <summary>
    /// Represents the application information to be passed during <see cref="ChromaSdk"/> initialization.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_a_p_p_i_n_f_o_t_y_p_e.html">ChromaSDK::APPINFOTYPE Struct Reference</seealso>
    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Interop marshaling")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public sealed class ChromaAppInfo : IChromaAppInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string? _title;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
        private string? _description;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string? _authorName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        private string? _authorContact;

        private SupportedDevices _supportedDevice;

        private AppCategory _category;

        /// <summary>
        /// Gets or sets the title of the application.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? Title
        {
            get => _title;
            set => _title = value;
        }

        /// <summary>
        /// Gets or sets the description of the application.
        /// </summary>
        /// <remarks>Values of 1024 characters or more will be truncated.</remarks>
        public string? Description
        {
            get => _description;
            set => _description = value;
        }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? AuthorName
        {
            get => _authorName;
            set => _authorName = value;
        }

        /// <summary>
        /// Gets or sets the contact details of the author.
        /// </summary>
        /// <remarks>Values of 256 characters or more will be truncated.</remarks>
        public string? AuthorContact
        {
            get => _authorContact;
            set => _authorContact = value;
        }

        /// <summary>
        /// Gets or sets the devices supported by the application.
        /// </summary>
        [SuppressMessage("Minor Code Smell", "S2292:Trivial properties should be auto-implemented", Justification = "Interop marshaling")]
        public SupportedDevices SupportedDevice
        {
            get => _supportedDevice;
            set => _supportedDevice = value;
        }

        /// <summary>
        /// Gets or sets the category of the application.
        /// </summary>
        [SuppressMessage("Minor Code Smell", "S2292:Trivial properties should be auto-implemented", Justification = "Interop marshaling")]
        public AppCategory Category
        {
            get => _category;
            set => _category = value;
        }
    }
}
