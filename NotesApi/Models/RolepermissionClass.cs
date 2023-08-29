using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("rolePermission")]
public class RolePermission : Entity
{
    [Column("role_id")][Required] public Guid RoleId { get; set; }
    [Column("permission_id")][Required] public Guid PermissionId { get; set; }
    [ForeignKey("RoleId")] public Role? Role { get; set; }
    [ForeignKey("PermissionId")] public Permission? Permission { get; set; }
}

public record RolePermissionView : IRecord
{
    [Column("role_id")] public Guid? RoleId { get; set; }
    [Column("permission_id")] public Guid? PermissionId { get; set; }
    [ForeignKey("RoleId")] public RoleView? Role { get; set; }
    [ForeignKey("PermissionId")] public PermissionView? Permission { get; set; }
}

public record RolePermissionQuery : ClientQuery
{
    
    public RolePermissionQuery() { }
}

public record RolePermissionCreate : IRecord
{
    [Column("role_id")][Required] public Guid RoleId { get; set; }
    [Column("permission_id")][Required] public Guid PermissionId { get; set; }
}

public record RolePermissionUpdate : IRecord
{
    [Column("role_id")][Required] public Guid RoleId { get; set; }
    [Column("permission_id")][Required] public Guid PermissionId { get; set; }
}

public record RolePermissionModify : IRecord
{
    [Column("role_id")] public Guid? RoleId { get; set; }
    [Column("permission_id")] public Guid? PermissionId { get; set; }
}