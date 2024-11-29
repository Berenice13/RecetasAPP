using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecetasApp.Models;

public class Token
{
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? TokenValue { get; set; }
    public List<string>? Abilities { get; set; }
    public string? LastUsedAt { get; set; }
    public string? ExpiresAt { get; set; }
}