using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("account")]
public class Account : Entity
{
    [Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountView : IRecord
{
    [Column("label")] public string? Label { get; set; }
    [Column("bank_name")] public string? BankName { get; set; }
    [Column("bank_address")] public string? BankAddress { get; set; }
    [Column("bank_swift")] public string? BankSwift { get; set; }
    [Column("account_name")] public string? AccountName { get; set; }
    [Column("account_iban")] public string? AccountIban { get; set; }
    [Column("account_address")] public string? AccountAddress { get; set; }
}

public record AccountQuery : ClientQuery
{
    [MinLength(3)][MaxLength(100)] public string? Label { get; set; } = "";
    [MinLength(3)][MaxLength(100)] public string? BankName { get; set; } = "";
    public AccountQuery() { }
}

public record AccountCreate : IRecord
{
    [Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountUpdate : IRecord
{
    [Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountModify : IRecord
{
    [Column("label")][MinLength(3)][MaxLength(100)] public string? Label { get; set; }
    [Column("bank_name")][MinLength(3)][MaxLength(100)] public string? BankName { get; set; }
    [Column("bank_address")][MinLength(3)][MaxLength(100)] public string? BankAddress { get; set; }
    [Column("bank_swift")][MinLength(3)][MaxLength(100)] public string? BankSwift { get; set; }
    [Column("account_name")][MinLength(3)][MaxLength(100)] public string? AccountName { get; set; }
    [Column("account_iban")][MinLength(3)][MaxLength(100)] public string? AccountIban { get; set; }
    [Column("account_address")][MinLength(3)][MaxLength(200)] public string? AccountAddress { get; set; }
}