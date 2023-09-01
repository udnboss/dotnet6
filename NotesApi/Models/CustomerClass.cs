using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("customer")]
public class Customer : Entity
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("currency")][ForeignKey("CurrencyId")] public Currency? Currency { get; set; }
    [JsonPropertyName("payment_term")][Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
    [JsonPropertyName("sales")][InverseProperty("Customer")] public IEnumerable<Sale>? Sales { get; set; }
}

public record CustomerView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("currency")][ForeignKey("CurrencyId")] public CurrencyView? Currency { get; set; }
    [JsonPropertyName("payment_term")][Column("payment_term")] public int? PaymentTerm { get; set; }
    [JsonPropertyName("sales")][InverseProperty("Customer")] public QueryResult<SaleQuery, SaleView>? Sales { get; set; }
}

public record CustomerQuery : ClientQuery
{
     public string? Name { get; set; } = "";
    public CustomerQuery() { }
}

public record CustomerCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("payment_term")][Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}

public record CustomerUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("payment_term")][Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}

public record CustomerModify : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("payment_term")][Column("payment_term")][Range(int.MinValue, int.MaxValue)] public int? PaymentTerm { get; set; }
}