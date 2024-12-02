using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecetasApp.Models;

public class Usuario
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [JsonPropertyName("biografia")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("imagen")]
    public string? Imagen { get; set; } 
}

public class DataUser
{
    [JsonPropertyName("user")]
    public Usuario? User { get; set; }
}
