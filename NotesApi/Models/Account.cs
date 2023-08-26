using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public interface IRecord
{
    public Guid Id { get; }
}

[Table("account")]
public class Account : Entity
{
    public Account() {
        Label = string.Empty;
    }

    [Column("label")]
    public string Label { get; set; }
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

public record AccountQuery 
(
    string? Label
) : ClientQuery;

public record AccountCreate
(
    [Required] Guid Id,
    [Required] string Label
) : IRecord;

public record AccountUpdate
(
    [Required] Guid Id,
    [Required] string Label
) : IRecord;

public record AccountModify
(
    Guid Id,
    string? Label
) : IRecord;