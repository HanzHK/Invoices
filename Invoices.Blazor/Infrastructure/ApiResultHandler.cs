using Invoices.Shared.Results;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Invoices.Blazor.Infrastructure
{
    /// <summary>
    /// Delegating handler that provides unified error handling for all HTTP requests.
    /// Converts HTTP responses and exceptions into <see cref="OperationResult{T}"/>
    /// to ensure consistent behavior across the Blazor application.
    /// </summary>
    public class ApiResultHandler : DelegatingHandler
    {
        private readonly JsonSerializerOptions _json;

        /// <summary>
        /// Initializes a new instance of <see cref="ApiResultHandler"/>.
        /// The handler uses shared <see cref="JsonSerializerOptions"/> to ensure
        /// consistent serialization behavior across the application.
        /// </summary>
        public ApiResultHandler(JsonSerializerOptions json)
        {
            _json = json;
        }

        /// <summary>
        /// Sends an HTTP request and catches network-level exceptions.
        /// If an exception occurs, a synthetic <see cref="HttpResponseMessage"/>
        /// with status 503 (ServiceUnavailable) is returned.
        /// </summary>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    ReasonPhrase = ex.Message
                };

                response.Headers.Add("X-Exception", ex.GetType().Name);
                return response;
            }
        }

        /// <summary>
        /// Executes an HTTP request and converts the result into <see cref="OperationResult{T}"/>.
        /// Handles both HTTP errors and unexpected exceptions.
        /// </summary>
        public async Task<OperationResult<T>> ExecuteAsync<T>(HttpRequestMessage request)
        {
            HttpResponseMessage response;

            try
            {
                response = await SendAsync(request, CancellationToken.None);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Fail(
                    error: "Unexpected network error.",
                    errorCode: "NETWORK_EXCEPTION",
                    exception: ex
                );
            }

            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<T>(_json);
                return OperationResult<T>.Ok(value!);
            }

            var errorText = await response.Content.ReadAsStringAsync();

            return OperationResult<T>.Fail(
                error: string.IsNullOrWhiteSpace(errorText)
                    ? "API returned an error."
                    : errorText,
                errorCode: "API_ERROR",
                statusCode: response.StatusCode
            );
        }

        /// <summary>
        /// Sends a GET request and returns the result as <see cref="OperationResult{T}"/>.
        /// </summary>
        public Task<OperationResult<T>> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return ExecuteAsync<T>(request);
        }

        /// <summary>
        /// Sends a POST request with a JSON body and returns the result as <see cref="OperationResult{T}"/>.
        /// </summary>
        public Task<OperationResult<T>> PostAsync<T>(string url, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(body, options: _json)
            };

            return ExecuteAsync<T>(request);
        }

        /// <summary>
        /// Sends a PUT request with a JSON body and returns the result as <see cref="OperationResult{T}"/>.
        /// </summary>
        public Task<OperationResult<T>> PutAsync<T>(string url, object body)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = JsonContent.Create(body, options: _json)
            };

            return ExecuteAsync<T>(request);
        }

        /// <summary>
        /// Sends a DELETE request and returns the result as <see cref="OperationResult"/>.
        /// </summary>
        public async Task<OperationResult> DeleteAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var result = await ExecuteAsync<object?>(request);

            return result.Success
                ? OperationResult.Ok()
                : OperationResult.Fail(result.Error!, result.ErrorCode, result.StatusCode, result.Exception);
        }
    }
}
