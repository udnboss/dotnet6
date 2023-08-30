using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("customer")]
public class Customer : Entity
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("address")] public string? Address { get; set; }
    [Column("contact")] public string? Contact { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [ForeignKey("CurrencyId")] public Currency? Currency { get; set; }
    [Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
    [InverseProperty("Customer")] public IEnumerable<Sale>? Sales { get; set; }
}

public record CustomerView : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [Column("address")] public string? Address { get; set; }
    [Column("contact")] public string? Contact { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [ForeignKey("CurrencyId")] public CurrencyView? Currency { get; set; }
    [Column("payment_term")] public int? PaymentTerm { get; set; }
    [InverseProperty("Customer")] public QueryResult<SaleQuery, SaleView>? Sales { get; set; }
}

public record CustomerQuery : ClientQuery
{
     public string? Name { get; set; } = "";
    public CustomerQuery() { }
}

public record CustomerCreate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("address")] public string? Address { get; set; }
    [Column("contact")] public string? Contact { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}

public record CustomerUpdate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("address")] public string? Address { get; set; }
    [Column("contact")] public string? Contact { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}

public record CustomerModify : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [Column("address")] public string? Address { get; set; }
    [Column("contact")] public string? Contact { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}