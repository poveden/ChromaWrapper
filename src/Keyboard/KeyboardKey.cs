namespace ChromaWrapper.Keyboard
{
    /// <summary>
    /// Specifies keyboard LEDs.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/namespace_chroma_s_d_k_1_1_keyboard.html">Razer Chroma SDK v3.3: ChromaSDK::Keyboard Namespace Reference</seealso>
    public enum KeyboardKey
    {
        /// <summary>None.</summary>
        None = 0,

        /// <summary>Esc (VK_ESCAPE).</summary>
        Esc = 0x0001,

        /// <summary>F1 (VK_F1).</summary>
        F1 = 0x0003,

        /// <summary>F2 (VK_F2).</summary>
        F2 = 0x0004,

        /// <summary>F3 (VK_F3).</summary>
        F3 = 0x0005,

        /// <summary>F4 (VK_F4).</summary>
        F4 = 0x0006,

        /// <summary>F5 (VK_F5).</summary>
        F5 = 0x0007,

        /// <summary>F6 (VK_F6).</summary>
        F6 = 0x0008,

        /// <summary>F7 (VK_F7).</summary>
        F7 = 0x0009,

        /// <summary>F8 (VK_F8).</summary>
        F8 = 0x000A,

        /// <summary>F9 (VK_F9).</summary>
        F9 = 0x000B,

        /// <summary>F10 (VK_F10).</summary>
        F10 = 0x000C,

        /// <summary>F11 (VK_F11).</summary>
        F11 = 0x000D,

        /// <summary>F12 (VK_F12).</summary>
        F12 = 0x000E,

        /// <summary>1 (VK_1).</summary>
        D1 = 0x0102,

        /// <summary>2 (VK_2).</summary>
        D2 = 0x0103,

        /// <summary>3 (VK_3).</summary>
        D3 = 0x0104,

        /// <summary>4 (VK_4).</summary>
        D4 = 0x0105,

        /// <summary>5 (VK_5).</summary>
        D5 = 0x0106,

        /// <summary>6 (VK_6).</summary>
        D6 = 0x0107,

        /// <summary>7 (VK_7).</summary>
        D7 = 0x0108,

        /// <summary>8 (VK_8).</summary>
        D8 = 0x0109,

        /// <summary>9 (VK_9).</summary>
        D9 = 0x010A,

        /// <summary>0 (VK_0).</summary>
        D0 = 0x010B,

        /// <summary>A (VK_A).</summary>
        A = 0x0302,

        /// <summary>B (VK_B).</summary>
        B = 0x0407,

        /// <summary>C (VK_C).</summary>
        C = 0x0405,

        /// <summary>D (VK_D).</summary>
        D = 0x0304,

        /// <summary>E (VK_E).</summary>
        E = 0x0204,

        /// <summary>F (VK_F).</summary>
        F = 0x0305,

        /// <summary>G (VK_G).</summary>
        G = 0x0306,

        /// <summary>H (VK_H).</summary>
        H = 0x0307,

        /// <summary>I (VK_I).</summary>
        I = 0x0209,

        /// <summary>J (VK_J).</summary>
        J = 0x0308,

        /// <summary>K (VK_K).</summary>
        K = 0x0309,

        /// <summary>L (VK_L).</summary>
        L = 0x030A,

        /// <summary>M (VK_M).</summary>
        M = 0x0409,

        /// <summary>N (VK_N).</summary>
        N = 0x0408,

        /// <summary>O (VK_O).</summary>
        O = 0x020A,

        /// <summary>P (VK_P).</summary>
        P = 0x020B,

        /// <summary>Q (VK_Q).</summary>
        Q = 0x0202,

        /// <summary>R (VK_R).</summary>
        R = 0x0205,

        /// <summary>S (VK_S).</summary>
        S = 0x0303,

        /// <summary>T (VK_T).</summary>
        T = 0x0206,

        /// <summary>U (VK_U).</summary>
        U = 0x0208,

        /// <summary>V (VK_V).</summary>
        V = 0x0406,

        /// <summary>W (VK_W).</summary>
        W = 0x0203,

        /// <summary>X (VK_X).</summary>
        X = 0x0404,

        /// <summary>Y (VK_Y).</summary>
        Y = 0x0207,

        /// <summary>Z (VK_Z).</summary>
        Z = 0x0403,

        /// <summary>Numlock (VK_NUMLOCK).</summary>
        NumLock = 0x0112,

        /// <summary>Numpad 0 (VK_NUMPAD0).</summary>
        NumPad0 = 0x0513,

        /// <summary>Numpad 1 (VK_NUMPAD1).</summary>
        NumPad1 = 0x0412,

        /// <summary>Numpad 2 (VK_NUMPAD2).</summary>
        NumPad2 = 0x0413,

        /// <summary>Numpad 3 (VK_NUMPAD3).</summary>
        NumPad3 = 0x0414,

        /// <summary>Numpad 4 (VK_NUMPAD4).</summary>
        NumPad4 = 0x0312,

        /// <summary>Numpad 5 (VK_NUMPAD5).</summary>
        NumPad5 = 0x0313,

        /// <summary>Numpad 6 (VK_NUMPAD6).</summary>
        NumPad6 = 0x0314,

        /// <summary>Numpad 7 (VK_NUMPAD7).</summary>
        NumPad7 = 0x0212,

        /// <summary>Numpad 8 (VK_NUMPAD8).</summary>
        NumPad8 = 0x0213,

        /// <summary>Numpad 9 (VK_ NUMPAD9).</summary>
        NumPad9 = 0x0214,

        /// <summary>Divide (VK_DIVIDE).</summary>
        NumPadDivide = 0x0113,

        /// <summary>Multiply (VK_MULTIPLY).</summary>
        NumPadMultiply = 0x0114,

        /// <summary>Subtract (VK_SUBTRACT).</summary>
        NumPadSubtract = 0x0115,

        /// <summary>Add (VK_ADD).</summary>
        NumPadAdd = 0x0215,

        /// <summary>Enter (VK_RETURN - Extended).</summary>
        NumPadEnter = 0x0415, // 1045

        /// <summary>Decimal (VK_DECIMAL).</summary>
        NumPadDecimal = 0x0514,

        /// <summary>Print Screen (VK_PRINT).</summary>
        PrintScreen = 0x000F,

        /// <summary>Scroll Lock (VK_SCROLL).</summary>
        Scroll = 0x0010,

        /// <summary>Pause (VK_PAUSE).</summary>
        Pause = 0x0011,

        /// <summary>Insert (VK_INSERT).</summary>
        Insert = 0x010F,

        /// <summary>Home (VK_HOME).</summary>
        Home = 0x0110,

        /// <summary>Page Up (VK_PRIOR).</summary>
        PageUp = 0x0111,

        /// <summary>Delete (VK_DELETE).</summary>
        Delete = 0x020f,

        /// <summary>End (VK_END).</summary>
        End = 0x0210,

        /// <summary>Page Down (VK_NEXT).</summary>
        PageDown = 0x0211,

        /// <summary>Up (VK_UP).</summary>
        Up = 0x0410,

        /// <summary>Left (VK_LEFT).</summary>
        Left = 0x050F,

        /// <summary>Down (VK_DOWN).</summary>
        Down = 0x0510,

        /// <summary>Right (VK_RIGHT).</summary>
        Right = 0x0511,

        /// <summary>Tab (VK_TAB).</summary>
        Tab = 0x0201,

        /// <summary>Caps Lock(VK_CAPITAL).</summary>
        CapsLock = 0x0301,

        /// <summary>Backspace (VK_BACK).</summary>
        Backspace = 0x010E,

        /// <summary>Enter (VK_RETURN).</summary>
        Enter = 0x030E,

        /// <summary>Left Control(VK_LCONTROL).</summary>
        LCtrl = 0x0501,

        /// <summary>Left Window (VK_LWIN).</summary>
        LWin = 0x0502,

        /// <summary>Left Alt (VK_LMENU).</summary>
        LAlt = 0x0503,

        /// <summary>Spacebar (VK_SPACE).</summary>
        Space = 0x0507,

        /// <summary>Right Alt (VK_RMENU).</summary>
        RAlt = 0x050B,

        /// <summary>Function key.</summary>
        Fn = 0x050C,

        /// <summary>Right Menu (VK_APPS).</summary>
        RMenu = 0x050D,

        /// <summary>Right Control (VK_RCONTROL).</summary>
        RCtrl = 0x050E,

        /// <summary>Left Shift (VK_LSHIFT).</summary>
        LShift = 0x0401,

        /// <summary>Right Shift (VK_RSHIFT).</summary>
        RShift = 0x040E,

        /// <summary>Macro Key 1.</summary>
        Macro1 = 0x0100,

        /// <summary>Macro Key 2.</summary>
        Macro2 = 0x0200,

        /// <summary>Macro Key 3.</summary>
        Macro3 = 0x0300,

        /// <summary>Macro Key 4.</summary>
        Macro4 = 0x0400,

        /// <summary>Macro Key 5.</summary>
        Macro5 = 0x0500,

        /// <summary>~ (tilde/半角/全角) (VK_OEM_3).</summary>
        Oem1 = 0x0101, // 257

        /// <summary>– (minus) (VK_OEM_MINUS).</summary>
        Oem2 = 0x010C, // 268

        /// <summary>= (equal) (VK_OEM_PLUS).</summary>
        Oem3 = 0x010D, // 269

        /// <summary>[ (left sqaure bracket) (VK_OEM_4).</summary>
        Oem4 = 0x020C, // 524

        /// <summary>] (right square bracket) (VK_OEM_6).</summary>
        Oem5 = 0x020D, // 525

        /// <summary>\ (backslash) (VK_OEM_5).</summary>
        Oem6 = 0x020E, // 526

        /// <summary>; (semi-colon) (VK_OEM_1).</summary>
        Oem7 = 0x030B, // 779

        /// <summary>' (apostrophe) (VK_OEM_7).</summary>
        Oem8 = 0x030C, // 780

        /// <summary>, (comma) (VK_OEM_COMMA).</summary>
        Oem9 = 0x040A, // 1034

        /// <summary>. (period) (VK_OEM_PERIOD).</summary>
        Oem10 = 0x040B, // 1035

        /// <summary>/ (forward slash) (VK_OEM_2).</summary>
        Oem11 = 0x040C, // 1036

        /// <summary>"#" (VK_OEM_5).</summary>
        Eur1 = 0x030D,

        /// <summary>\ (VK_OEM_102).</summary>
        Eur2 = 0x0402, // 1026

        /// <summary>¥ (0xFF).</summary>
        Jpn1 = 0x0015, // 21

        /// <summary>\ (0xC1).</summary>
        Jpn2 = 0x040D, // 1037

        /// <summary>無変換 (VK_OEM_PA1).</summary>
        Jpn3 = 0x0504, // 1284

        /// <summary>変換 (0xFF).</summary>
        Jpn4 = 0x0509, // 1289

        /// <summary>ひらがな/カタカナ (0xFF).</summary>
        Jpn5 = 0x050A, // 1290

        /// <summary>| (0xFF).</summary>
        Kor1 = Jpn1,

        /// <summary>(VK_OEM_5).</summary>
        Kor2 = Eur1,

        /// <summary>(VK_OEM_102).</summary>
        Kor3 = Eur2,

        /// <summary>(0xC1).</summary>
        Kor4 = Jpn2,

        /// <summary>(VK_OEM_PA1).</summary>
        Kor5 = Jpn3,

        /// <summary>한/영 (0xFF).</summary>
        Kor6 = Jpn4,

        /// <summary>(0xFF).</summary>
        Kor7 = Jpn5,

        /// <summary>Invalid keys.</summary>
        Invalid = 0xFFFF,

        /// <summary>Razer logo.</summary>
        Logo = 0x0014,
    }
}
