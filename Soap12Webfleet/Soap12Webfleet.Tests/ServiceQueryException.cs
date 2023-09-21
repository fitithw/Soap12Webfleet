using System;

namespace Soap12Webfleet {
    [Serializable]
    public class ServiceQueryException : Exception {
        public int StatusCode { get; }
        public override string Message => $"{StatusCode}: {base.Message}";

        public ServiceQueryException() { }
        public ServiceQueryException(string message, int statusCode) : base(message) {
            StatusCode = statusCode;
        }
        public ServiceQueryException(string message, int statusCode, Exception inner) : base(message, inner) {
            StatusCode = statusCode;
        }
        protected ServiceQueryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
