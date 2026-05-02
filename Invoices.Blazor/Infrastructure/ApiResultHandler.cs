using Invoices.Shared.Results;
using System.Net.Http.Json;
using System.Text.Json;

namespace Invoices.Blazor.Infrastructure
{
    /// <summary>
    /// Centralized HTTP handler for all API communication.
    /// Wraps HttpClient and converts all responses and exceptions
    /// into <see cref="OperationResult{T}"/> for consistent error handling
    /// across the entire Blazor application.
    /// </summary>
    public class ApiResultHandler
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _json;

        /// <summary>
        /// Initializes a new instance of <see cref="ApiResultHandler"/>.
        /// Both HttpClient and JsonSerializerOptions are injected via DI.
        /// </summary>
        public ApiResultHandler(HttpClient http, JsonSerializerOptions json)
        {
            _http = http;
            _json = json;
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
                response = await _http.SendAsync(request);
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
        /// Handles 204 No Content responses correctly without attempting JSON deserialization.
        /// </summary>
        public async Task<OperationResult> DeleteAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            try
            {
                var response = await _http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return OperationResult.Ok();

                var errorText = await response.Content.ReadAsStringAsync();
                return OperationResult.Fail(
                    error: string.IsNullOrWhiteSpace(errorText) ? "API returned an error." : errorText,
                    errorCode: "API_ERROR",
                    statusCode: response.StatusCode
                );
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(
                    error: "Unexpected network error.",
                    errorCode: "NETWORK_EXCEPTION",
                    exception: ex
                );
            }
        }
    }
}