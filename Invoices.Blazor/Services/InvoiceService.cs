using Invoices.Shared.Models.Invoice;
using Invoices.Shared.Results;
using Invoices.Blazor.Infrastructure;

namespace Invoices.Blazor.Services
{
    /// <summary>
    /// Service responsible for performing CRUD operations on Invoice resources
    /// via the backend API. Uses <see cref="ApiResultHandler"/> to ensure
    /// consistent error handling and unified response structure across the application.
    /// </summary>
    public class InvoiceService
    {
        private readonly ApiResultHandler _api;

        /// <summary>
        /// Initializes a new instance of <see cref="InvoiceService"/>.
        /// The <see cref="ApiResultHandler"/> is injected via DI and provides
        /// unified HTTP request execution and error handling.
        /// </summary>
        public InvoiceService(ApiResultHandler api)
        {
            _api = api;
        }

        /// <summary>
        /// Retrieves all invoices from the API.
        /// Returns an <see cref="OperationResult{T}"/> containing a list of invoices.
        /// </summary>
        public Task<OperationResult<List<InvoiceGetDto>>> GetAllAsync()
            => _api.GetAsync<List<InvoiceGetDto>>("api/invoices");

        /// <summary>
        /// Retrieves a single invoice by ID.
        /// Returns an <see cref="OperationResult{T}"/> containing the invoice if found.
        /// </summary>
        public Task<OperationResult<InvoiceGetDto>> GetByIdAsync(int id)
            => _api.GetAsync<InvoiceGetDto>($"api/invoices/{id}");

        /// <summary>
        /// Creates a new invoice using the provided DTO.
        /// Returns an <see cref="OperationResult{T}"/> containing the created invoice.
        /// </summary>
        public Task<OperationResult<InvoiceGetDto>> CreateAsync(InvoicePostDto dto)
            => _api.PostAsync<InvoiceGetDto>("api/invoices", dto);

        /// <summary>
        /// Replaces an existing invoice with the provided DTO.
        /// Returns an <see cref="OperationResult{T}"/> containing the updated invoice.
        /// </summary>
        public Task<OperationResult<InvoiceGetDto>> ReplaceAsync(int id, InvoicePostDto dto)
            => _api.PutAsync<InvoiceGetDto>($"api/invoices/{id}", dto);

        /// <summary>
        /// Deletes an invoice by ID.
        /// Returns an <see cref="OperationResult"/> indicating success or failure.
        /// </summary>
        public Task<OperationResult> DeleteAsync(int id)
            => _api.DeleteAsync($"api/invoices/{id}");
    }
}
