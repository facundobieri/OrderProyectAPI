using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DollarApiService
    {
        private readonly HttpClient _httpClient;

        public DollarApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal?> GetOfficialDollarRateAsync()
        {
            var response = await _httpClient.GetAsync("https://dolarapi.com/v1/dolares/oficial");

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<DollarRateDto>(
                stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return result?.Venta; // devolvemos el número, no el DTO
        }
    }
}
