using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecetasApp.Models;

public class Valoracion
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("RecetaId")]
    public Receta? Receta { get; set; }

    public int Puntuacion { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaValoracion { get; set; } = DateTime.Now; 

}
