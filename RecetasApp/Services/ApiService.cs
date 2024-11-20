using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RecetasApp.Models;

namespace RecetasApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiRoutes.ApiRoutes.BaseUrl)
            };
        }

        // Método genérico para hacer una solicitud POST y deserializar la respuesta
        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
        {
            // Convertir el objeto a JSON
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Hacer la solicitud POST
            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                // Leer la respuesta y deserializarla en un objeto ApiResponse<T>
                var responseJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseJson);
                return apiResponse;
            }

            // Si no es exitoso, devolver una respuesta de error genérica
            return new ApiResponse<T>
            {
                Status = "Error",
                Data = default, // No hay datos
                Error = "Error al realizar la solicitud"
            };
        }
    }
}
