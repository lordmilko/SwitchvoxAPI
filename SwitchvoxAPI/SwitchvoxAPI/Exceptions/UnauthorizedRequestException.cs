using System;
using System.Runtime.Serialization;

namespace SwitchvoxAPI
{
    /// <summary>
    /// The exception that is thrown when a <see cref="T:SwitchvoxAPI.SwitchvoxResponse"/> fails with a <see cref="T:System.Net.WebException"/> with StatusCode <see cref="System.Net.HttpStatusCode.Forbidden"/>/>.
    /// </summary>
    [Serializable]
    public class UnauthorizedRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequestException"/> class.
        /// </summary>
        public UnauthorizedRequestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequestException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public UnauthorizedRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequestException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner"/> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        public UnauthorizedRequestException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SwitchvoxAPI.SwitchvoxRequestException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        public UnauthorizedRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
