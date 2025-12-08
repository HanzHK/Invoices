using Invoices.Blazor.Models; 
using System.Net.Http.Json;

namespace Invoices.Blazor.Services
{
    public class PersonService
    {
        private readonly HttpClient _http;

        public PersonService(HttpClient http)
        {
            _http = http;
        }

        // GET /api/persons
        public async Task<List<PersonDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PersonDto>>("api/persons")
                   ?? new List<PersonDto>();
        }

        // GET /api/persons/{id}
        public async Task<PersonDto?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PersonDto>($"api/persons/{id}");
        }

        // POST /api/persons
        public async Task<PersonDto?> CreateAsync(PersonDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/persons", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PersonDto>();
            }
            return null;
        }

        // PUT /api/persons/{id}
        public async Task<PersonDto?> ReplaceAsync(int id, PersonDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/persons/{id}", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PersonDto>();
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
