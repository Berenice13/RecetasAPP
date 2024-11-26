using System.Text.Json.Serialization;

namespace RecetasApp.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        public bool Response { get; set; }
    }

}
