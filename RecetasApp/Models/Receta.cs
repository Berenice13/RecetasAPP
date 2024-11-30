using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecetasApp.Models;

public class Receta
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public required string Titulo { get; set; }

    public string? Descripcion { get; set; }

    public int UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }

    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria? Categoria { get; set; }

    public string? Dificultad { get; set; } // "Fácil", "Medio", "Difícil"

    public string? TiempoPreparacion { get; set; } 

    [DataType(DataType.Date)]
    public DateTime FechaPublicacion { get; set; } = DateTime.Now; 

    public string? Imagen { get; set; } 
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}


public class RecetaResponse
{
    [JsonPropertyName("recetas")]
    public List<Receta>? Recetas { get; set; }
}
