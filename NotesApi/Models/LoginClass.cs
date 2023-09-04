using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("login")]
public class Login : IdentityUser<Guid>, IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public override Guid Id { get; set; }
    [JsonPropertyName("userName")][Column("userName")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public override string UserName { get; set; }
    [JsonPropertyName("normalizedUserName")][Column("normalizedUserName")][Required][MinLength(3)][MaxLength(256)] public override string NormalizedUserName { get; set; }
    [JsonPropertyName("passwordHash")][Column("passwordHash")][Required][MinLength(8)][MaxLength(512)] public override string PasswordHash { get; set; }
    [JsonPropertyName("securityStamp")][Column("securityStamp")][MinLength(0)][MaxLength(256)] public override string? SecurityStamp { get; set; }
    [JsonPropertyName("accessFailedCount")][Column("accessFailedCount")][Required][Range(int.MinValue, int.MaxValue)] public override int AccessFailedCount { get; set; }
    [JsonPropertyName("concurrencyStamp")][Column("concurrencyStamp")] public override string? ConcurrencyStamp { get; set; }
    [JsonPropertyName("email")][Column("email")] public override string? Email { get; set; }
    [JsonPropertyName("emailConfirmed")][Column("emailConfirmed")][Required] public override bool EmailConfirmed { get; set; }
    [JsonPropertyName("lockoutEnabled")][Column("lockoutEnabled")][Required] public override bool LockoutEnabled { get; set; }
    [JsonPropertyName("lockoutEnd")][Column("lockoutEnd")] public override DateTimeOffset? LockoutEnd { get; set; }
    [JsonPropertyName("normalizedEmail")][Column("normalizedEmail")] public override string? NormalizedEmail { get; set; }
    [JsonPropertyName("phoneNumber")][Column("phoneNumber")] public override string? PhoneNumber { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")][Column("phoneNumberConfirmed")][Required] public override bool PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("twoFactorEnabled")][Column("twoFactorEnabled")][Required] public override bool TwoFactorEnabled { get; set; }
}

public record LoginView : IRecord
{
    [JsonPropertyName("userName")][Column("userName")] public string? UserName { get; set; }
    [JsonPropertyName("normalizedUserName")][Column("normalizedUserName")] public string? NormalizedUserName { get; set; }
    [JsonPropertyName("passwordHash")][Column("passwordHash")] public string? PasswordHash { get; set; }
    [JsonPropertyName("securityStamp")][Column("securityStamp")] public string? SecurityStamp { get; set; }
    [JsonPropertyName("accessFailedCount")][Column("accessFailedCount")] public int? AccessFailedCount { get; set; }
    [JsonPropertyName("concurrencyStamp")][Column("concurrencyStamp")] public string? ConcurrencyStamp { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("emailConfirmed")][Column("emailConfirmed")] public bool? EmailConfirmed { get; set; }
    [JsonPropertyName("lockoutEnabled")][Column("lockoutEnabled")] public bool? LockoutEnabled { get; set; }
    [JsonPropertyName("lockoutEnd")][Column("lockoutEnd")] public DateTimeOffset? LockoutEnd { get; set; }
    [JsonPropertyName("normalizedEmail")][Column("normalizedEmail")] public string? NormalizedEmail { get; set; }
    [JsonPropertyName("phoneNumber")][Column("phoneNumber")] public string? PhoneNumber { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")][Column("phoneNumberConfirmed")] public bool? PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("twoFactorEnabled")][Column("twoFactorEnabled")] public bool? TwoFactorEnabled { get; set; }
}

public record LoginQuery : ClientQuery
{
    [MinLengthIfNotNull(3)][MaxLength(100)] public string? UserName { get; set; } = "";
    public LoginQuery() { }
}

public record LoginCreate : IRecord
{
    [JsonPropertyName("userName")][Column("userName")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public string UserName { get; set; }
    [JsonPropertyName("normalizedUserName")][Column("normalizedUserName")][Required][MinLength(3)][MaxLength(256)] public string NormalizedUserName { get; set; }
    [JsonPropertyName("passwordHash")][Column("passwordHash")][Required][MinLength(8)][MaxLength(512)] public string PasswordHash { get; set; }
    [JsonPropertyName("securityStamp")][Column("securityStamp")][MinLength(0)][MaxLength(256)] public string? SecurityStamp { get; set; }
    [JsonPropertyName("accessFailedCount")][Column("accessFailedCount")][Required][Range(int.MinValue, int.MaxValue)] public int AccessFailedCount { get; set; }
    [JsonPropertyName("concurrencyStamp")][Column("concurrencyStamp")] public string? ConcurrencyStamp { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("emailConfirmed")][Column("emailConfirmed")][Required] public bool EmailConfirmed { get; set; }
    [JsonPropertyName("lockoutEnabled")][Column("lockoutEnabled")][Required] public bool LockoutEnabled { get; set; }
    [JsonPropertyName("lockoutEnd")][Column("lockoutEnd")] public DateTimeOffset? LockoutEnd { get; set; }
    [JsonPropertyName("normalizedEmail")][Column("normalizedEmail")] public string? NormalizedEmail { get; set; }
    [JsonPropertyName("phoneNumber")][Column("phoneNumber")] public string? PhoneNumber { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")][Column("phoneNumberConfirmed")][Required] public bool PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("twoFactorEnabled")][Column("twoFactorEnabled")][Required] public bool TwoFactorEnabled { get; set; }
}

public record LoginUpdate : IRecord
{
    [JsonPropertyName("userName")][Column("userName")][Required][EmailAddress][MinLength(3)][MaxLength(100)] public string UserName { get; set; }
    [JsonPropertyName("normalizedUserName")][Column("normalizedUserName")][Required][MinLength(3)][MaxLength(256)] public string NormalizedUserName { get; set; }
    [JsonPropertyName("passwordHash")][Column("passwordHash")][Required][MinLength(8)][MaxLength(512)] public string PasswordHash { get; set; }
    [JsonPropertyName("securityStamp")][Column("securityStamp")][MinLength(0)][MaxLength(256)] public string? SecurityStamp { get; set; }
    [JsonPropertyName("accessFailedCount")][Column("accessFailedCount")][Required][Range(int.MinValue, int.MaxValue)] public int AccessFailedCount { get; set; }
    [JsonPropertyName("concurrencyStamp")][Column("concurrencyStamp")] public string? ConcurrencyStamp { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("emailConfirmed")][Column("emailConfirmed")][Required] public bool EmailConfirmed { get; set; }
    [JsonPropertyName("lockoutEnabled")][Column("lockoutEnabled")][Required] public bool LockoutEnabled { get; set; }
    [JsonPropertyName("lockoutEnd")][Column("lockoutEnd")] public DateTimeOffset? LockoutEnd { get; set; }
    [JsonPropertyName("normalizedEmail")][Column("normalizedEmail")] public string? NormalizedEmail { get; set; }
    [JsonPropertyName("phoneNumber")][Column("phoneNumber")] public string? PhoneNumber { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")][Column("phoneNumberConfirmed")][Required] public bool PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("twoFactorEnabled")][Column("twoFactorEnabled")][Required] public bool TwoFactorEnabled { get; set; }
}

public record LoginModify : IRecord
{
    [JsonPropertyName("userName")][Column("userName")][EmailAddress][MinLength(3)][MaxLength(100)] public string? UserName { get; set; }
    [JsonPropertyName("normalizedUserName")][Column("normalizedUserName")][MinLength(3)][MaxLength(256)] public string? NormalizedUserName { get; set; }
    [JsonPropertyName("passwordHash")][Column("passwordHash")][MinLength(8)][MaxLength(512)] public string? PasswordHash { get; set; }
    [JsonPropertyName("securityStamp")][Column("securityStamp")][MinLength(0)][MaxLength(256)] public string? SecurityStamp { get; set; }
    [JsonPropertyName("accessFailedCount")][Column("accessFailedCount")][Range(int.MinValue, int.MaxValue)] public int? AccessFailedCount { get; set; }
    [JsonPropertyName("concurrencyStamp")][Column("concurrencyStamp")] public string? ConcurrencyStamp { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
    [JsonPropertyName("emailConfirmed")][Column("emailConfirmed")] public bool? EmailConfirmed { get; set; }
    [JsonPropertyName("lockoutEnabled")][Column("lockoutEnabled")] public bool? LockoutEnabled { get; set; }
    [JsonPropertyName("lockoutEnd")][Column("lockoutEnd")] public DateTimeOffset? LockoutEnd { get; set; }
    [JsonPropertyName("normalizedEmail")][Column("normalizedEmail")] public string? NormalizedEmail { get; set; }
    [JsonPropertyName("phoneNumber")][Column("phoneNumber")] public string? PhoneNumber { get; set; }
    [JsonPropertyName("phoneNumberConfirmed")][Column("phoneNumberConfirmed")] public bool? PhoneNumberConfirmed { get; set; }
    [JsonPropertyName("twoFactorEnabled")][Column("twoFactorEnabled")] public bool? TwoFactorEnabled { get; set; }
}