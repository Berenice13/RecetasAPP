using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecetasApp.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public string? Nombre { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; } 
}

public class RegisterData
{
    [JsonPropertyName("user")]
    public Usuario? User { get; set; }
}
