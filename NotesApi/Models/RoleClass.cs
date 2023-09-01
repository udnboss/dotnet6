using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("role")]
public class Role : Entity
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("rolePermissions")][InverseProperty("Role")] public IEnumerable<RolePermission>? RolePermissions { get; set; }
}

public record RoleView : IRecord
{
    [JsonPropertyName("code")][Column("code")] public string? Code { get; set; }
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("rolePermissions")][InverseProperty("Role")] public QueryResult<RolePermissionQuery, RolePermissionView>? RolePermissions { get; set; }
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