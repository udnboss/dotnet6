using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("rolePermission")]
public class RolePermission :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public string PermissionId { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public Role? Role { get; set; }
    [JsonPropertyName("permission")][ForeignKey("PermissionId")] public Permission? Permission { get; set; }
}

public record RolePermissionView : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")] public string? RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")] public string? PermissionId { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public RoleView? Role { get; set; }
    [JsonPropertyName("permission")][ForeignKey("PermissionId")] public PermissionView? Permission { get; set; }
}

public record RolePermissionQuery : ClientQuery
{
     public IEnumerable<string?> RoleId { get; set; }
     public IEnumerable<string?> PermissionId { get; set; }
    public RolePermissionQuery() { }
}

public record RolePermissionCreate : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public string PermissionId { get; set; }
}

public record RolePermissionUpdate : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")][Required] public string RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public string PermissionId { get; set; }
}

public record RolePermissionModify : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")] public string? RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")] public string? PermissionId { get; set; }
}