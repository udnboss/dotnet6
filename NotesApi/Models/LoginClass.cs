using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("login")]
public class Login : Entity
{
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("password")][Column("password")][Required] public string Password { get; set; }
}

public record LoginView : IRecord
{
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("password")][Column("password")] public string? Password { get; set; }
}

public record LoginQuery : ClientQuery
{
     public string? Email { get; set; } = "";
    public LoginQuery() { }
}

public record LoginCreate : IRecord
{
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("password")][Column("password")][Required] public string Password { get; set; }
}

public record LoginUpdate : IRecord
{
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("password")][Column("password")][Required] public string Password { get; set; }
}

public record LoginModify : IRecord
{
    [JsonPropertyName("email")][Column("email")][EmailAddress] public string? Email { get; set; }
    [JsonPropertyName("password")][Column("password")] public string? Password { get; set; }
}