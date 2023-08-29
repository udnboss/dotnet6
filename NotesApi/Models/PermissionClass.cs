using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("permission")]
public class Permission : Entity
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
    [Column("entity")][Required] public string Entity { get; set; }
    [Column("action")][Required] public string Action { get; set; }
    [InverseProperty("Permission")] public ICollection<RolePermission>? Roles { get; set; }
}

public record PermissionView : IRecord
{
    [Column("code")] public string? Code { get; set; }
    [Column("name")] public string? Name { get; set; }
    [Column("entity")] public string? Entity { get; set; }
    [Column("action")] public string? Action { get; set; }
    [InverseProperty("Permission")] public ICollection<RolePermission>? Roles { get; set; }
}

public record PermissionQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public string? Entity { get; set; } = "";
     public string? Action { get; set; } = "";
    public PermissionQuery() { }
}

public record PermissionCreate : IRecord
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
    [Column("entity")][Required] public string Entity { get; set; }
    [Column("action")][Required] public string Action { get; set; }
    [InverseProperty("Permission")] public ICollection<RolePermission>? Roles { get; set; }
}

public record PermissionUpdate : IRecord
{
    [Column("code")][Required] public string Code { get; set; }
    [Column("name")][Required] public string Name { get; set; }
    [Column("entity")][Required] public string Entity { get; set; }
    [Column("action")][Required] public string Action { get; set; }
    [InverseProperty("Permission")] public ICollection<RolePermission>? Roles { get; set; }
}

public record PermissionModify : IRecord
{
    [Column("code")] public string? Code { get; set; }
    [Column("name")] public string? Name { get; set; }
    [Column("entity")] public string? Entity { get; set; }
    [Column("action")] public string? Action { get; set; }
    [InverseProperty("Permission")] public ICollection<RolePermission>? Roles { get; set; }
}