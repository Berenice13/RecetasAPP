using System;
using System.ComponentModel.DataAnnotations;

namespace RecetasApp.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [MaxLength(80)]
    public string? Nombre { get; set; }

}
