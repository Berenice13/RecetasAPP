namespace RecetasApp.Models
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public T Data { get; set; } // El tipo de 'data' puede variar, por eso es genérico
        public string Error { get; set; }
    }
}
