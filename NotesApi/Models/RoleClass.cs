using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("role")]
public class Role :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("rolePermissions")][InverseProperty("Role")] public IEnumerable<RolePermission>? RolePermissions { get; set; }
    [JsonPropertyName("roleUsers")][InverseProperty("Role")] public IEnumerable<UserRole>? RoleUsers { get; set; }
}

public record RoleView : IRecord
{
    [JsonPropertyName("code")][Column("code")] public string? Code { get; set; }
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("rolePermissions")][InverseProperty("Role")] public QueryResult<RolePermissionQuery, RolePermissionView>? RolePermissions { get; set; }
    [JsonPropertyName("roleUsers")][InverseProperty("Role")] public QueryResult<UserRoleQuery, UserRoleView>? RoleUsers { get; set; }
}

public record RoleQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public string? Code { get; set; } = "";
    public RoleQuery() { }
}

public record RoleCreate : IRecord
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
}

public record RoleUpdate : IRecord
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
}

public record RoleModify : IRecord
{
    [JsonPropertyName("code")][Column("code")] public string? Code { get; set; }
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
}