using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("account")]
public class Account : Entity
{
    [Column("label")]
    [MinLength(3)]
    public string Label { get; set; } = string.Empty;
}

public record AccountSimple
(
    Guid Id,
    string Label
) : IRecord;

public record AccountView(Guid Id, string Label) : IRecord
{
    public AccountView() : this(Guid.Empty, string.Empty) { }
}

public record AccountQuery : ClientQuery
{
    [MinLength(3)]
    public string? Label { get; set; }
    public AccountQuery() { }
}

public record AccountCreate
(
    [Required] Guid Id,
    [Required][MinLength(3)] string Label
) : IRecord;

public record AccountUpdate
(
    [Required] Guid Id,
    [Required][MinLength(3)] string Label
) : IRecord;

public record AccountModify
(
    Guid Id,
    [MinLength(3)] string? Label
) : IRecord;