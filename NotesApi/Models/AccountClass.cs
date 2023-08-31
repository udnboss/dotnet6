using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("account")]
public class Account : Entity
{
    [JsonPropertyName("label")][Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [JsonPropertyName("bank_name")][Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [JsonPropertyName("bank_address")][Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [JsonPropertyName("bank_swift")][Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [JsonPropertyName("account_name")][Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [JsonPropertyName("account_iban")][Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [JsonPropertyName("account_address")][Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountView : IRecord
{
    [JsonPropertyName("label")][Column("label")] public string? Label { get; set; }
    [JsonPropertyName("bank_name")][Column("bank_name")] public string? BankName { get; set; }
    [JsonPropertyName("bank_address")][Column("bank_address")] public string? BankAddress { get; set; }
    [JsonPropertyName("bank_swift")][Column("bank_swift")] public string? BankSwift { get; set; }
    [JsonPropertyName("account_name")][Column("account_name")] public string? AccountName { get; set; }
    [JsonPropertyName("account_iban")][Column("account_iban")] public string? AccountIban { get; set; }
    [JsonPropertyName("account_address")][Column("account_address")] public string? AccountAddress { get; set; }
}

public record AccountQuery : ClientQuery
{
    [MinLengthIfNotNull(3)][MaxLength(100)] public string? Label { get; set; } = "";
    [MinLengthIfNotNull(3)][MaxLength(100)] public string? BankName { get; set; } = "";
    public AccountQuery() { }
}

public record AccountCreate : IRecord
{
    [JsonPropertyName("label")][Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [JsonPropertyName("bank_name")][Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [JsonPropertyName("bank_address")][Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [JsonPropertyName("bank_swift")][Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [JsonPropertyName("account_name")][Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [JsonPropertyName("account_iban")][Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [JsonPropertyName("account_address")][Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountUpdate : IRecord
{
    [JsonPropertyName("label")][Column("label")][Required][MinLength(3)][MaxLength(100)] public string Label { get; set; }
    [JsonPropertyName("bank_name")][Column("bank_name")][Required][MinLength(3)][MaxLength(100)] public string BankName { get; set; }
    [JsonPropertyName("bank_address")][Column("bank_address")][Required][MinLength(3)][MaxLength(100)] public string BankAddress { get; set; }
    [JsonPropertyName("bank_swift")][Column("bank_swift")][Required][MinLength(3)][MaxLength(100)] public string BankSwift { get; set; }
    [JsonPropertyName("account_name")][Column("account_name")][Required][MinLength(3)][MaxLength(100)] public string AccountName { get; set; }
    [JsonPropertyName("account_iban")][Column("account_iban")][Required][MinLength(3)][MaxLength(100)] public string AccountIban { get; set; }
    [JsonPropertyName("account_address")][Column("account_address")][Required][MinLength(3)][MaxLength(200)] public string AccountAddress { get; set; }
}

public record AccountModify : IRecord
{
    [JsonPropertyName("label")][Column("label")][MinLength(3)][MaxLength(100)] public string? Label { get; set; }
    [JsonPropertyName("bank_name")][Column("bank_name")][MinLength(3)][MaxLength(100)] public string? BankName { get; set; }
    [JsonPropertyName("bank_address")][Column("bank_address")][MinLength(3)][MaxLength(100)] public string? BankAddress { get; set; }
    [JsonPropertyName("bank_swift")][Column("bank_swift")][MinLength(3)][MaxLength(100)] public string? BankSwift { get; set; }
    [JsonPropertyName("account_name")][Column("account_name")][MinLength(3)][MaxLength(100)] public string? AccountName { get; set; }
    [JsonPropertyName("account_iban")][Column("account_iban")][MinLength(3)][MaxLength(100)] public string? AccountIban { get; set; }
    [JsonPropertyName("account_address")][Column("account_address")][MinLength(3)][MaxLength(200)] public string? AccountAddress { get; set; }
}