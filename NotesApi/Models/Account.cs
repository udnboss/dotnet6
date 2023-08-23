using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("account")]
public class Account : Entity
{
    public Account() {
        Label = string.Empty;
    }

    [Column("label")]
    public string Label { get; set; }
}

public record AccountQuery
(
    string? Label
);

public record AccountCreate
(
    [Required] Guid Id,
    [Required] string Label
);

public record AccountUpdate
(
    [Required] Guid Id,
    [Required] string Label
);

public record AccountModify
(
    Guid? Id,
    string? Label
);