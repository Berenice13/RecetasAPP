using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecetasApp.Models;

public class TokenDetails
{
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? Token { get; set; }
    public List<string>? Abilities { get; set; }
    public string? LastUsedAt { get; set; }
    public string? ExpiresAt { get; set; }
}

public class TokenResponse
{
    [JsonPropertyName("token")]
    public TokenDetails? Token { get; set; }
}
