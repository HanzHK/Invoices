using Invoices.Shared.Models.Person;
using Invoices.Shared.Models.Common;
using Invoices.Blazor.Utils.Converters;
using System.Net.Http.Json;
using System.Text.Json;

namespace Invoices.Blazor.Services
{
    public class PersonService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public PersonService(HttpClient http)
        {
            _http = http;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new UpperCaseEnumConverter<Country>() }
            };
        }

        // GET /api/persons
        public async Task<List<PersonDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PersonDto>>("api/persons", _jsonOptions) 
                   ?? new List<PersonDto>();
        }

        // GET /api/persons/{id}
        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PersonDto>($"api/persons/{id}", _jsonOptions); 
        }

        // POST /api/persons
        public async Task<PersonDto?> CreateAsync(PersonDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/persons", dto, _jsonOptions); 
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PersonDto>(_jsonOptions); 
            }
            return null;
        }

        // PUT /api/persons/{id}
        public async Task<PersonDto?> ReplaceAsync(int id, PersonDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/persons/{id}", dto, _jsonOptions); 
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PersonDto>(_jsonOptions); 
            }
            return null;
        }

        // DELETE /api/persons/{id}
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/persons/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}