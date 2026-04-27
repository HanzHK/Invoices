using Invoices.Shared.Models.Person;
using Invoices.Shared.Results;
using Invoices.Blazor.Infrastructure;

namespace Invoices.Blazor.Services
{
    /// <summary>
    /// Service responsible for performing CRUD operations on Person resources
    /// via the backend API. Uses <see cref="ApiResultHandler"/> to ensure
    /// consistent error handling and unified response structure across the application.
    /// </summary>
    public class PersonService
    {
        private readonly ApiResultHandler _api;

        /// <summary>
        /// Initializes a new instance of <see cref="PersonService"/>.
        /// The <see cref="ApiResultHandler"/> is injected via DI and provides
        /// unified HTTP request execution and error handling.
        /// </summary>
        public PersonService(ApiResultHandler api)
        {
            _api = api;
        }

        /// <summary>
        /// Retrieves all persons from the API.
        /// Returns an <see cref="OperationResult{T}"/> containing a list of persons.
        /// </summary>
        public Task<OperationResult<List<PersonDto>>> GetAllAsync()
            => _api.GetAsync<List<PersonDto>>("api/persons");

        /// <summary>
        /// Retrieves a single person by ID.
        /// Returns an <see cref="OperationResult{T}"/> containing the person if found.
        /// </summary>
        public Task<OperationResult<PersonDto>> GetByIdAsync(int id)
            => _api.GetAsync<PersonDto>($"api/persons/{id}");

        /// <summary>
        /// Creates a new person using the provided DTO.
        /// Returns an <see cref="OperationResult{T}"/> containing the created person.
        /// </summary>
        public Task<OperationResult<PersonDto>> CreateAsync(PersonDto dto)
            => _api.PostAsync<PersonDto>("api/persons", dto);

        /// <summary>
        /// Replaces an existing person with the provided DTO.
        /// Returns an <see cref="OperationResult{T}"/> containing the updated person.
        /// </summary>
        public Task<OperationResult<PersonDto>> ReplaceAsync(int id, PersonDto dto)
            => _api.PutAsync<PersonDto>($"api/persons/{id}", dto);

        /// <summary>
        /// Deletes a person by ID.
        /// Returns an <see cref="OperationResult"/> indicating success or failure.
        /// </summary>
        public Task<OperationResult> DeleteAsync(int id)
            => _api.DeleteAsync($"api/persons/{id}");
    }
}
