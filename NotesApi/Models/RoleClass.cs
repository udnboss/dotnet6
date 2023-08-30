using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("role")]
public class Role : Entity
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
    [InverseProperty("Role")] public IEnumerable<RolePermission>? RolePermissions { get; set; }
}

public record RoleView : IRecord
{
    [Column("code")] public string? Code { get; set; }
    [Column("name")] public string? Name { get; set; }
    [InverseProperty("Role")] public QueryResult<RolePermissionQuery, RolePermissionView>? RolePermissions { get; set; }
}

public record RoleQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public string? Code { get; set; } = "";
    public RoleQuery() { }
}

public record RoleCreate : IRecord
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
}

public record RoleUpdate : IRecord
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
}

public record RoleModify : IRecord
{
    [Column("code")] public string? Code { get; set; }
    [Column("name")] public string? Name { get; set; }
}