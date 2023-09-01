using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("permission")]
public class Permission : Entity
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("entity")][Column("entity")][Required] public string Entity { get; set; }
    [JsonPropertyName("action")][Column("action")][Required] public string Action { get; set; }
    [JsonPropertyName("roles")][InverseProperty("Permission")] public IEnumerable<RolePermission>? Roles { get; set; }
}

public record PermissionView : IRecord
{
    [JsonPropertyName("code")][Column("code")] public string? Code { get; set; }
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("entity")][Column("entity")] public string? Entity { get; set; }
    [JsonPropertyName("action")][Column("action")] public string? Action { get; set; }
    [JsonPropertyName("roles")][InverseProperty("Permission")] public QueryResult<RolePermissionQuery, RolePermissionView>? Roles { get; set; }
}

public record PermissionQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public string? Code { get; set; } = "";
     public string? Entity { get; set; } = "";
     public string? Action { get; set; } = "";
    public PermissionQuery() { }
}

public record PermissionCreate : IRecord
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("entity")][Column("entity")][Required] public string Entity { get; set; }
    [JsonPropertyName("action")][Column("action")][Required] public string Action { get; set; }
}

public record PermissionUpdate : IRecord
{
    [JsonPropertyName("code")][Column("code")][Required] public string Code { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("entity")][Column("entity")][Required] public string Entity { get; set; }
    [JsonPropertyName("action")][Column("action")][Required] public string Action { get; set; }
}

public record PermissionModify : IRecord
{
    [JsonPropertyName("code")][Column("code")] public string? Code { get; set; }
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("entity")][Column("entity")] public string? Entity { get; set; }
    [JsonPropertyName("action")][Column("action")] public string? Action { get; set; }
}