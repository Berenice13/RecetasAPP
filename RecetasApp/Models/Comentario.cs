using System;
using System.ComponentModel.DataAnnotations;

namespace RecetasApp.Models;

public class Comentario
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UsuarioId")]
    public Usuario? Usuario { get; set; }

    [ForeignKey("RecetaId")]
    public Receta? Receta { get; set; }

    public string Contenido { get; set; }

    [DataType(DataType.Date)]
    public DateTime FechaComentario { get; set; } = DateTime.Now; 

}
