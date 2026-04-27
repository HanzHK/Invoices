using System.Net;

namespace Invoices.Shared.Results
{
    /// <summary>
    /// Represents the outcome of an operation that does not return a value.
    /// Provides unified error handling across application layers.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Indicates whether the operation completed successfully.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Human-readable error message describing the failure.
        /// Null when the operation succeeds.
        /// </summary>
        public string? Error { get; }

        /// <summary>
        /// Machine-readable error code used for UI mapping, localization,
        /// logging or automated error processing.
        /// </summary>
        public string? ErrorCode { get; }

        /// <summary>
        /// Optional exception captured during the operation.
        /// Not intended for UI consumption, but useful for logging.
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Optional HTTP status code associated with the failure.
        /// Useful when mapping API responses to application-level errors.
        /// </summary>
        public HttpStatusCode? StatusCode { get; }

        protected OperationResult(
            bool success,
            string? error = null,
            string? errorCode = null,
            Exception? exception = null,
            HttpStatusCode? statusCode = null)
        {
            Success = success;
            Error = error;
            ErrorCode = errorCode;
            Exception = exception;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Creates a successful operation result.
        /// </summary>
        public static OperationResult Ok() =>
            new(true);

        /// <summary>
        /// Creates a failed operation result with an error message.
        /// </summary>
        public static OperationResult Fail(
            string error,
            string? errorCode = null,
            HttpStatusCode? statusCode = null,
            Exception? exception = null) =>
            new(false, error, errorCode, exception, statusCode);
    }

    /// <summary>
    /// Represents the outcome of an operation that returns a value.
    /// Provides unified error handling across application layers.
    /// </summary>
    public class OperationResult<T> : OperationResult
    {
        /// <summary>
        /// The value returned by the operation.
        /// Null when the operation fails.
        /// </summary>
        public T? Value { get; }

        private OperationResult(
            bool success,
            T? value = default,
            string? error = null,
            string? errorCode = null,
            Exception? exception = null,
            HttpStatusCode? statusCode = null)
            : base(success, error, errorCode, exception, statusCode)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a successful operation result containing a value.
        /// </summary>
        public static OperationResult<T> Ok(T value) =>
            new(true, value);

        /// <summary>
        /// Creates a failed operation result with an error message.
        /// </summary>
        public static OperationResult<T> Fail(
            string error,
            string? errorCode = null,
            HttpStatusCode? statusCode = null,
            Exception? exception = null) =>
            new(false, default, error, errorCode, exception, statusCode);
    }
}
