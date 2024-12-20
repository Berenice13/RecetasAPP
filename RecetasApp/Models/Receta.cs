using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecetasApp.Models;

public class Receta
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("titulo")]
    public required string Titulo { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }

    [JsonPropertyName("categoriaId")]
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria? Categoria { get; set; }

    [JsonPropertyName("dificultad")]
    public string? Dificultad { get; set; } // "Fácil", "Medio", "Difícil"

    [JsonPropertyName("tiempoPreparacion")]
    public string? TiempoPreparacion { get; set; } 

    [JsonPropertyName("fechaPublicacion")]
    public DateTime FechaPublicacion { get; set; } = DateTime.Now; 

    [JsonPropertyName("imagen")]
    public string? Imagen { get; set; } 

    [JsonPropertyName("puntuacionMedia")]
    public double? PuntuacionMedia { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}


public class RecetaResponse
{
    [JsonPropertyName("recetas")]
    public List<Receta>? Recetas { get; set; }
}

public class RecetaDetailResponse
{
    [JsonPropertyName("recetas")]
    public Receta? Recetas { get; set; }
}
