using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("userRole")]
public class UserRole :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("user_id")][Column("user_id")][Required] public string UserId { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
    [JsonPropertyName("user")][ForeignKey("UserId")] public User? User { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public Role? Role { get; set; }
}

public record UserRoleView : IRecord
{
    [JsonPropertyName("user_id")][Column("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")] public string? RoleId { get; set; }
    [JsonPropertyName("user")][ForeignKey("UserId")] public UserView? User { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public RoleView? Role { get; set; }
}

public record UserRoleQuery : ClientQuery
{
     public IEnumerable<string?> UserId { get; set; }
     public IEnumerable<string?> RoleId { get; set; }
    public UserRoleQuery() { }
}

public record UserRoleCreate : IRecord
{
    [JsonPropertyName("user_id")][Column("user_id")][Required] public string UserId { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
}

public record UserRoleUpdate : IRecord
{
    [JsonPropertyName("user_id")][Column("user_id")][Required] public string UserId { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
}

public record UserRoleModify : IRecord
{
    [JsonPropertyName("user_id")][Column("user_id")] public string? UserId { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")] public string? RoleId { get; set; }
}