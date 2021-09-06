namespace ChromaWrapper.Sdk
{
    /// <summary>
    /// Error codes for Chroma SDK. If the error is not defined here, refer to WinError.h from the Windows SDK.
    /// </summary>
    /// <seealso href="https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/_rz_errors_8h.html">RzErrors.h File Reference</seealso>
    /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes">System Error Codes</seealso>
    public enum ChromaResult
    {
        /// <summary>Invalid.</summary>
        Invalid = -1,

        /// <summary>Success.</summary>
        Success = 0,

        /// <summary>Access denied.</summary>
        AccessDenied = 5,

        /// <summary>Invalid handle.</summary>
        InvalidHandle = 6,

        /// <summary>Not supported.</summary>
        NotSupported = 50,

        /// <summary>Invalid parameter.</summary>
        InvalidParameter = 87,

        /// <summary>The service has not been started.</summary>
        ServiceNotActive = 1062,

        /// <summary>Cannot start more than one instance of the specified program.</summary>
        SingleInstanceApp = 1152,

        /// <summary>Device not connected.</summary>
        DeviceNotConnected = 1167,

        /// <summary>Element not found.</summary>
        NotFound = 1168,

        /// <summary>Request aborted.</summary>
        RequestAborted = 1235,

        /// <summary>An attempt was made to perform an initialization operation when initialization has already been completed.</summary>
        AlreadyInitialized = 1247,

        /// <summary>Resource not available or disabled.</summary>
        ResourceDisabled = 4309,

        /// <summary>Device not available or supported.</summary>
        DeviceNotAvailable = 4319,

        /// <summary>The group or resource is not in the correct state to perform the requested operation.</summary>
        NotValidState = 5023,

        /// <summary>No more items.</summary>
        NoMoreItems = 259,

        /// <summary>General failure.</summary>
        Failed = unchecked((int)2147500037),
    }
}
