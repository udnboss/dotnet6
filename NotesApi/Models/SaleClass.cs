using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("sale")]
public class Sale : Entity
{
    [Column("company_id")][Required] public Guid CompanyId { get; set; }
    [Column("account_id")][Required] public Guid AccountId { get; set; }
    [Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [Column("place")] public string? Place { get; set; }
    [Column("number")][Range(int.MinValue, int.MaxValue)] public int? Number { get; set; }
    [Column("date")][Required] public DateTime Date { get; set; }
    [Column("total")][Range(double.MinValue, double.MaxValue)] public decimal? Total { get; set; } = 0;
    [Column("totalItems")][Range(int.MinValue, int.MaxValue)] public int? TotalItems { get; set; } = 0;
    [Column("reference")] public string? Reference { get; set; }
    [Column("confirmed")][Required] public bool Confirmed { get; set; }
    [Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [Column("due_date")] public DateTime? DueDate { get; set; }
    [ForeignKey("CurrencyId")] public Currency? Currency { get; set; }
    [ForeignKey("CustomerId")] public Customer? Customer { get; set; }
    [ForeignKey("AccountId")] public Account? Account { get; set; }
    [ForeignKey("CompanyId")] public Company? Company { get; set; }
    [InverseProperty("Sale")] public IEnumerable<SaleItem>? Items { get; set; }
}

public record SaleView : IRecord
{
    [Column("company_id")] public Guid? CompanyId { get; set; }
    [Column("account_id")] public Guid? AccountId { get; set; }
    [Column("customer_id")] public Guid? CustomerId { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [Column("place")] public string? Place { get; set; }
    [Column("number")] public int? Number { get; set; }
    [Column("date")] public DateTime? Date { get; set; }
    [Column("total")] public decimal? Total { get; set; } = 0;
    [Column("totalItems")] public int? TotalItems { get; set; } = 0;
    [Column("reference")] public string? Reference { get; set; }
    [Column("confirmed")] public bool? Confirmed { get; set; }
    [Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [Column("due_date")] public DateTime? DueDate { get; set; }
    [ForeignKey("CurrencyId")] public CurrencyView? Currency { get; set; }
    [ForeignKey("CustomerId")] public CustomerView? Customer { get; set; }
    [ForeignKey("AccountId")] public AccountView? Account { get; set; }
    [ForeignKey("CompanyId")] public CompanyView? Company { get; set; }
    [InverseProperty("Sale")] public QueryResult<SaleItemQuery, SaleItemView>? Items { get; set; }
}

public record SaleQuery : ClientQuery
{
     public Guid? CustomerId { get; set; } = null;
     public Guid? AccountId { get; set; } = null;
     public int? Number { get; set; } = null;
     public DateTime? Date { get; set; } = null;
     public DateTime? ReferenceDate { get; set; } = null;
    public SaleQuery() { }
}

public record SaleCreate : IRecord
{
    [Column("company_id")][Required] public Guid CompanyId { get; set; }
    [Column("account_id")][Required] public Guid AccountId { get; set; }
    [Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [Column("place")] public string? Place { get; set; }
    [Column("date")][Required] public DateTime Date { get; set; }
    [Column("reference")] public string? Reference { get; set; }
    [Column("confirmed")][Required] public bool Confirmed { get; set; }
    [Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [Column("due_date")] public DateTime? DueDate { get; set; }
}

public record SaleUpdate : IRecord
{
    [Column("company_id")][Required] public Guid CompanyId { get; set; }
    [Column("account_id")][Required] public Guid AccountId { get; set; }
    [Column("customer_id")][Required] public Guid CustomerId { get; set; }
    [Column("currency_id")][Required] public Guid CurrencyId { get; set; }
    [Column("place")] public string? Place { get; set; }
    [Column("reference")] public string? Reference { get; set; }
    [Column("confirmed")][Required] public bool Confirmed { get; set; }
    [Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [Column("due_date")] public DateTime? DueDate { get; set; }
}

public record SaleModify : IRecord
{
    [Column("company_id")] public Guid? CompanyId { get; set; }
    [Column("account_id")] public Guid? AccountId { get; set; }
    [Column("customer_id")] public Guid? CustomerId { get; set; }
    [Column("currency_id")] public Guid? CurrencyId { get; set; }
    [Column("place")] public string? Place { get; set; }
    [Column("reference")] public string? Reference { get; set; }
    [Column("confirmed")] public bool? Confirmed { get; set; }
    [Column("reference_date")] public DateTime? ReferenceDate { get; set; }
    [Column("due_date")] public DateTime? DueDate { get; set; }
}