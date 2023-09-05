using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("user")]
public class User :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)][MaxLength(100)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public string LoginId { get; set; }
    [JsonPropertyName("login")][ForeignKey("LoginId")] public Login? Login { get; set; }
    [JsonPropertyName("userRoles")][InverseProperty("User")] public IEnumerable<UserRole>? UserRoles { get; set; }
}

public record UserView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")] public string? LoginId { get; set; }
    [JsonPropertyName("login")][ForeignKey("LoginId")] public LoginView? Login { get; set; }
    [JsonPropertyName("userRoles")][InverseProperty("User")] public QueryResult<UserRoleQuery, UserRoleView>? UserRoles { get; set; }
}

public record UserQuery : ClientQuery
{
    [MinLengthIfNotNull(3)][MaxLength(100)] public string? Name { get; set; } = "";
    [MinLengthIfNotNull(3)][MaxLength(100)] public string? Email { get; set; } = "";
    public UserQuery() { }
}

public record UserCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)][MaxLength(100)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public string LoginId { get; set; }
}

public record UserUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required][MinLength(3)][MaxLength(100)] public string Name { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public string Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")][Required] public string LoginId { get; set; }
}

public record UserModify : IRecord
{
    [JsonPropertyName("name")][Column("name")][MinLength(3)][MaxLength(100)] public string? Name { get; set; }
    [JsonPropertyName("email")][Column("email")][EmailAddress][MinLength(3)][MaxLength(100)] public string? Email { get; set; }
    [JsonPropertyName("login_id")][Column("login_id")] public string? LoginId { get; set; }
}