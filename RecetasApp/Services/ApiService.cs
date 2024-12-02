using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data, bool useToken)
        {
            try
            {
                // Convertir el objeto a JSON
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Configurar el token si es necesario
                if (useToken)
                {
                    var tokenUser = Preferences.Get("AuthToken", string.Empty);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenUser);
                }

                // Realizar la solicitud POST
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Mostrar respuesta para depuración
                Debug.WriteLine($"Respuesta POST: {responseContent}");

                // Deserializar la respuesta
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ignorar diferencias de mayúsculas/minúsculas en las propiedades
                });

                // Validar el estado HTTP y el status del API
                if (response.IsSuccessStatusCode && (apiResponse?.Status == 200 || apiResponse?.Status == 201))
                {
                    return apiResponse;
                }
                else
                {
                    // Devolver una respuesta de error si falla
                    return new ApiResponse<T>
                    {
                        Status = apiResponse?.Status ?? (int)response.StatusCode,
                        Msg = apiResponse?.Msg ?? "Error desconocido",
                        Data = default
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error inesperado: {ex.Message}");
                return new ApiResponse<T>
                {
                    Status = 500,
                    Msg = $"Error inesperado: {ex.Message}",
                    Data = default
                };
            }
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint, bool useToken)
        {
            try
            {
                if (useToken)
                {
                    var tokenUser = Preferences.Get("AuthToken", string.Empty);
                    Debug.WriteLine($"Token: {tokenUser}");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenUser);
                }

                var response = await _httpClient.GetAsync(endpoint);
                var responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Respuesta GET: {responseContent}");

                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (response.IsSuccessStatusCode && (apiResponse?.Status == 200 || apiResponse?.Status == 201))
                {
                    return apiResponse;
                }
                else
                {
                    return new ApiResponse<T>
                    {
                        Status = apiResponse?.Status ?? (int)response.StatusCode,
                        Msg = apiResponse?.Msg ?? "Error desconocido",
                        Data = default
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error inesperado: {ex.Message}");
                return new ApiResponse<T>
                {
                    Status = 500,
                    Msg = $"Error inesperado: {ex.Message}",
                    Data = default
                };
            }
        }


    }
}