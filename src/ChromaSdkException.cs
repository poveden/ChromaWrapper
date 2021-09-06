using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using ChromaWrapper.Sdk;

namespace ChromaWrapper
{
    /// <summary>
    /// The exception that is thrown when a Chroma SDK operation fails.
    /// </summary>
    [Serializable]
    public class ChromaSdkException : Win32Exception
    {
        private static readonly ResourceManager _resources = new ResourceManager(typeof(ChromaSdkException));

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class.
        /// </summary>
        public ChromaSdkException()
            : this(ChromaResult.Failed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class
        /// with the specified result code.
        /// </summary>
        /// <param name="result">The result code.</param>
        public ChromaSdkException(ChromaResult result)
            : this(result, GetMessage(result, null))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ChromaSdkException(string message)
            : this(ChromaResult.Failed, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class
        /// with the specified result code and message.
        /// </summary>
        /// <param name="result">The result code.</param>
        /// <param name="message">The error message.</param>
        public ChromaSdkException(ChromaResult result, string message)
            : base((int)result, message)
        {
            HResult = NativeErrorCode <= 0
                ? NativeErrorCode
                : (int)(0x80070000 | ((uint)NativeErrorCode & 0x0000ffff));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class
        /// with the specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ChromaSdkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaSdkException"/> class
        /// with the specified context and the serialization information.
        /// </summary>
        /// <param name="info">The serialization info associated with this exception.</param>
        /// <param name="context">The streaming context of this exception.</param>
        protected ChromaSdkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the result code.
        /// </summary>
        public ChromaResult Result => (ChromaResult)NativeErrorCode;

        internal static ChromaSdkException WithMethodName(ChromaResult result, string methodName)
        {
            return new ChromaSdkException(result, GetMessage(result, methodName));
        }

        private static string GetMessage(ChromaResult result, string? methodName)
        {
            string? res = null;

            if (methodName != null)
            {
                res = _resources.GetString($"{result}_{methodName}", CultureInfo.CurrentCulture);
            }

            return res
                ?? _resources.GetString(result.ToString(), CultureInfo.CurrentCulture)
                ?? new Win32Exception((int)result).Message;
        }
    }
}
