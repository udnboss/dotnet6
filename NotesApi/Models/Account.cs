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