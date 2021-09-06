using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ChromaWrapper.Data
{
    /// <summary>
    /// Represents device information obtained through a call to <see cref="ChromaSdk.QueryDevice"/>.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_d_e_v_i_c_e___i_n_f_o___t_y_p_e.html">ChromaSDK::DEVICE_INFO_TYPE Struct Reference</seealso>
    [SuppressMessage("Style", "IDE0032:Use auto property", Justification = "Interop marshaling")]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class ChromaDeviceInfo
    {
        private HardwareType _deviceType;
        private int _connected;

        internal ChromaDeviceInfo()
        {
        }

        /// <summary>
        /// Specifies a device type for the <see cref="ChromaDeviceInfo"/> object.
        /// </summary>
        /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/struct_chroma_s_d_k_1_1_d_e_v_i_c_e___i_n_f_o___t_y_p_e.html">ChromaSDK::DEVICE_INFO_TYPE Struct Reference</seealso>
        public enum HardwareType
        {
            /// <summary>Unknown device type.</summary>
            Unknown = 0,

            /// <summary>Keyboard device.</summary>
            Keyboard = 1,

            /// <summary>Mouse device.</summary>
            Mouse = 2,

            /// <summary>Headset device.</summary>
            Headset = 3,

            /// <summary>Mousepad device.</summary>
            Mousepad = 4,

            /// <summary>Keypad device.</summary>
            Keypad = 5,

            /// <summary>System device.</summary>
            System = 6,

            /// <summary>Speakers.</summary>
            Speakers = 7,

            /// <summary>Invalid device.</summary>
            Invalid = 8,
        }

        /// <summary>
        /// Gets the device type.
        /// </summary>
        public HardwareType DeviceType
        {
            get => _deviceType;
            internal set => _deviceType = value;
        }

        /// <summary>
        /// Gets the number of connected devices.
        /// </summary>
        public int Connected
        {
            get => _connected;
            internal set => _connected = value;
        }
    }
}
