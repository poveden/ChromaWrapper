namespace ChromaWrapper.Data
{
    /// <summary>
    /// Specifies the supported devices of a connected application.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_a_p_p_i_n_f_o_t_y_p_e.html">ChromaSDK::APPINFOTYPE Struct Reference</seealso>
    [Flags]
    public enum SupportedDevices
    {
        /// <summary>No device support.</summary>
        None = 0,

        /// <summary>Supports keyboards.</summary>
        Keyboard = 0x01,

        /// <summary>Supports mice.</summary>
        Mouse = 0x02,

        /// <summary>Supports headsets.</summary>
        Headset = 0x04,

        /// <summary>Supports mousepads.</summary>
        Mousepad = 0x08,

        /// <summary>Supports keypads.</summary>
        Keypad = 0x10,

        /// <summary>Supports Chroma Link devices.</summary>
        ChromaLink = 0x20,

        /// <summary>Supports all devices.</summary>
        All = Keyboard | Mouse | Headset | Mousepad | Keypad | ChromaLink,
    }
}
