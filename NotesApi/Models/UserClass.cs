using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("user")]
public class User : Entity
{
    [Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [Column("email")][Required][EmailAddress] public string Email { get; set; }
    [Column("login_id")][Required] public Guid LoginId { get; set; }
    [ForeignKey("LoginId")] public Login? Login { get; set; }
}

public record UserView : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [Column("email")] public string? Email { get; set; }
    [Column("login_id")] public Guid? LoginId { get; set; }
    [ForeignKey("LoginId")] public LoginView? Login { get; set; }
}

public record UserQuery : ClientQuery
{
    [MinLength(3)] public string? Name { get; set; } = "";
     public string? Email { get; set; } = "";
    public UserQuery() { }
}

public record UserCreate : IRecord
{
    [Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [Column("email")][Required][EmailAddress] public string Email { get; set; }
    [Column("login_id")][Required] public Guid LoginId { get; set; }
}

public record UserUpdate : IRecord
{
    [Column("name")][Required][MinLength(3)] public string Name { get; set; }
    [Column("email")][Required][EmailAddress] public string Email { get; set; }
    [Column("login_id")][Required] public Guid LoginId { get; set; }
}

public record UserModify : IRecord
{
    [Column("name")][MinLength(3)] public string? Name { get; set; }
    [Column("email")][EmailAddress] public string? Email { get; set; }
    [Column("login_id")] public Guid? LoginId { get; set; }
}