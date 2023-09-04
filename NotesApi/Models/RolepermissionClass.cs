using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("rolePermission")]
public class RolePermission :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    [JsonPropertyName("role_id")][Column("role_id")][Required] public Guid RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public Guid PermissionId { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public Role? Role { get; set; }
    [JsonPropertyName("permission")][ForeignKey("PermissionId")] public Permission? Permission { get; set; }
}

public record RolePermissionView : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")] public Guid? RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")] public Guid? PermissionId { get; set; }
    [JsonPropertyName("role")][ForeignKey("RoleId")] public RoleView? Role { get; set; }
    [JsonPropertyName("permission")][ForeignKey("PermissionId")] public PermissionView? Permission { get; set; }
}

public record RolePermissionQuery : ClientQuery
{
     public IEnumerable<Guid?> RoleId { get; set; }
     public IEnumerable<Guid?> PermissionId { get; set; }
    public RolePermissionQuery() { }
}

public record RolePermissionCreate : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")][Required] public Guid RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public Guid PermissionId { get; set; }
}

public record RolePermissionUpdate : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")][Required] public Guid RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")][Required] public Guid PermissionId { get; set; }
}

public record RolePermissionModify : IRecord
{
    [JsonPropertyName("role_id")][Column("role_id")] public Guid? RoleId { get; set; }
    [JsonPropertyName("permission_id")][Column("permission_id")] public Guid? PermissionId { get; set; }
}