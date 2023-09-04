using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("sale")]
public class Sale :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    [JsonPropertyName("company_id")][Column("company_id")][Required] public Guid CompanyId { get; set; }
    [JsonPropertyName("account_id")][Column("account_id")][Required] public Guid AccountId { get; set; }
    [JsonPropertyName("customer_id")][Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [JsonPropertyName("place")][Column("place")] public string? Place { get; set; }
    [JsonPropertyName("number")][Column("number")][Range(int.MinValue, int.MaxValue)] public int? Number { get; set; }
    [JsonPropertyName("date")][Column("date")][Required] public DateTime Date { get; set; }
    [JsonPropertyName("total")][Column("total")][Range(double.MinValue, double.MaxValue)] public decimal? Total { get; set; } = 0;
    [JsonPropertyName("totalItems")][Column("totalItems")][Range(int.MinValue, int.MaxValue)] public int? TotalItems { get; set; } = 0;
    [JsonPropertyName("reference")][Column("reference")] public string? Reference { get; set; }
    [JsonPropertyName("confirmed")][Column("confirmed")][Required] public bool Confirmed { get; set; }
    [JsonPropertyName("reference_date")][Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [JsonPropertyName("due_date")][Column("due_date")] public DateTime? DueDate { get; set; }
    [JsonPropertyName("currency")][ForeignKey("CurrencyId")] public Currency? Currency { get; set; }
    [JsonPropertyName("customer")][ForeignKey("CustomerId")] public Customer? Customer { get; set; }
    [JsonPropertyName("account")][ForeignKey("AccountId")] public Account? Account { get; set; }
    [JsonPropertyName("company")][ForeignKey("CompanyId")] public Company? Company { get; set; }
    [JsonPropertyName("items")][InverseProperty("Sale")] public IEnumerable<SaleItem>? Items { get; set; }
}

public record SaleView : IRecord
{
    [JsonPropertyName("company_id")][Column("company_id")] public Guid? CompanyId { get; set; }
    [JsonPropertyName("account_id")][Column("account_id")] public Guid? AccountId { get; set; }
    [JsonPropertyName("customer_id")][Column("customer_id")] public Guid? CustomerId { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("place")][Column("place")] public string? Place { get; set; }
    [JsonPropertyName("number")][Column("number")] public int? Number { get; set; }
    [JsonPropertyName("date")][Column("date")] public DateTime? Date { get; set; }
    [JsonPropertyName("total")][Column("total")] public decimal? Total { get; set; } = 0;
    [JsonPropertyName("totalItems")][Column("totalItems")] public int? TotalItems { get; set; } = 0;
    [JsonPropertyName("reference")][Column("reference")] public string? Reference { get; set; }
    [JsonPropertyName("confirmed")][Column("confirmed")] public bool? Confirmed { get; set; }
    [JsonPropertyName("reference_date")][Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [JsonPropertyName("due_date")][Column("due_date")] public DateTime? DueDate { get; set; }
    [JsonPropertyName("currency")][ForeignKey("CurrencyId")] public CurrencyView? Currency { get; set; }
    [JsonPropertyName("customer")][ForeignKey("CustomerId")] public CustomerView? Customer { get; set; }
    [JsonPropertyName("account")][ForeignKey("AccountId")] public AccountView? Account { get; set; }
    [JsonPropertyName("company")][ForeignKey("CompanyId")] public CompanyView? Company { get; set; }
    [JsonPropertyName("items")][InverseProperty("Sale")] public QueryResult<SaleItemQuery, SaleItemView>? Items { get; set; }
}

public record SaleQuery : ClientQuery
{
     public IEnumerable<Guid?> CustomerId { get; set; }
     public IEnumerable<Guid?> AccountId { get; set; }
     public int? Number { get; set; } = null;
     public DateTime? Date { get; set; } = null;
     public DateTime? ReferenceDate { get; set; } = null;
    public SaleQuery() { }
}

public record SaleCreate : IRecord
{
    [JsonPropertyName("company_id")][Column("company_id")][Required] public Guid CompanyId { get; set; }
    [JsonPropertyName("account_id")][Column("account_id")][Required] public Guid AccountId { get; set; }
    [JsonPropertyName("customer_id")][Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [JsonPropertyName("place")][Column("place")] public string? Place { get; set; }
    [JsonPropertyName("date")][Column("date")][Required] public DateTime Date { get; set; }
    [JsonPropertyName("reference")][Column("reference")] public string? Reference { get; set; }
    [JsonPropertyName("confirmed")][Column("confirmed")][Required] public bool Confirmed { get; set; }
    [JsonPropertyName("reference_date")][Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [JsonPropertyName("due_date")][Column("due_date")] public DateTime? DueDate { get; set; }
}

public record SaleUpdate : IRecord
{
    [JsonPropertyName("company_id")][Column("company_id")][Required] public Guid CompanyId { get; set; }
    [JsonPropertyName("account_id")][Column("account_id")][Required] public Guid AccountId { get; set; }
    [JsonPropertyName("customer_id")][Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [JsonPropertyName("place")][Column("place")] public string? Place { get; set; }
    [JsonPropertyName("reference")][Column("reference")] public string? Reference { get; set; }
    [JsonPropertyName("confirmed")][Column("confirmed")][Required] public bool Confirmed { get; set; }
    [JsonPropertyName("reference_date")][Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [JsonPropertyName("due_date")][Column("due_date")] public DateTime? DueDate { get; set; }
}

public record SaleModify : IRecord
{
    [JsonPropertyName("company_id")][Column("company_id")] public Guid? CompanyId { get; set; }
    [JsonPropertyName("account_id")][Column("account_id")] public Guid? AccountId { get; set; }
    [JsonPropertyName("customer_id")][Column("customer_id")] public Guid? CustomerId { get; set; }
    [JsonPropertyName("currency_id")][Column("currency_id")] public Guid? CurrencyId { get; set; }
    [JsonPropertyName("place")][Column("place")] public string? Place { get; set; }
    [JsonPropertyName("reference")][Column("reference")] public string? Reference { get; set; }
    [JsonPropertyName("confirmed")][Column("confirmed")] public bool? Confirmed { get; set; }
    [JsonPropertyName("reference_date")][Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [JsonPropertyName("due_date")][Column("due_date")] public DateTime? DueDate { get; set; }
}