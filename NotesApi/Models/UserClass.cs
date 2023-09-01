using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("user")]
public class User : Entity
{
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public Guid LoginId { get; set; }
    [JsonPropertyName("login")][ForeignKey("LoginId")] public Login? Login { get; set; }
}

public record UserView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")] public Guid? LoginId { get; set; }
    [JsonPropertyName("login")][ForeignKey("LoginId")] public LoginView? Login { get; set; }
}

public record UserQuery : ClientQuery
{
    [MinLengthIfNotNull(3)] public string? Name { get; set; } = "";
     public string? Email { get; set; } = "";
    public UserQuery() { }
}

public record UserCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public Guid LoginId { get; set; }
}

public record UserUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public Guid LoginId { get; set; }
}

public record UserModify : IRecord
{
    [JsonPropertyName("name")][Column("name")][MinLength(3)] public string? Name { get; set; }
    [JsonPropertyName("email")][Column("email")][EmailAddress] public string? Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")] public Guid? LoginId { get; set; }
}