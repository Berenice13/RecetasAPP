using System.Diagnostics;
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
            try
            {
                // Convertir el objeto a JSON
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Hacer la solicitud POST
                Debug.WriteLine("intentando solicitud POST");
                Debug.WriteLine(endpoint);
                
                var response = await _httpClient.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("respuesta POST");
                Debug.WriteLine(responseContent);
                
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(responseContent);

                Debug.WriteLine(apiResponse);

                if (response.IsSuccessStatusCode && apiResponse?.Status == 200)
                {
                    // Si es éxito, devolver el mensaje y los datos del servidor
                    return new ApiResponse<T>
                    {
                        Response = true,
                        Msg = apiResponse.Msg,
                        Data = apiResponse.Data
                    };
                }
                else
                {
                    // Si hay error, devolver el mensaje del servidor o uno genérico
                    return new ApiResponse<T>
                    {
                        Response = false,
                        Msg = apiResponse?.Msg,
                        Data = default
                    };
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"Request error: {httpEx.Message}");
                if (httpEx.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {httpEx.InnerException.Message}");
                }
                return new ApiResponse<T>
                {
                    Response = false,
                    Msg = $"Error de conexión: {httpEx.Message}",
                    Data = default
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error inesperado: {ex.Message}");
                return new ApiResponse<T>
                {
                    Response = false,
                    Msg = $"Error inesperado: {ex.Message}",
                    Data = default
                };
            }


        }
    }
}