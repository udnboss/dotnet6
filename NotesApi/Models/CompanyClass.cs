using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#pragma warning disable CS8618

[Table("company")]
public class Company : Entity
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")][Required] public string Address { get; set; }
    [JsonPropertyName("crn")][Column("crn")][Required] public string Crn { get; set; }
    [JsonPropertyName("trn")][Column("trn")][Required] public string Trn { get; set; }
    [JsonPropertyName("contact")][Column("contact")][Required] public string Contact { get; set; }
    [JsonPropertyName("mobile")][Column("mobile")][Required] public string Mobile { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
}

public record CompanyView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("crn")][Column("crn")] public string? Crn { get; set; }
    [JsonPropertyName("trn")][Column("trn")] public string? Trn { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("mobile")][Column("mobile")] public string? Mobile { get; set; }
    [JsonPropertyName("email")][Column("email")] public string? Email { get; set; }
}

public record CompanyQuery : ClientQuery
{
     public string? Name { get; set; } = "";
    public CompanyQuery() { }
}

public record CompanyCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")][Required] public string Address { get; set; }
    [JsonPropertyName("crn")][Column("crn")][Required] public string Crn { get; set; }
    [JsonPropertyName("trn")][Column("trn")][Required] public string Trn { get; set; }
    [JsonPropertyName("contact")][Column("contact")][Required] public string Contact { get; set; }
    [JsonPropertyName("mobile")][Column("mobile")][Required] public string Mobile { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
}

public record CompanyUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("address")][Column("address")][Required] public string Address { get; set; }
    [JsonPropertyName("crn")][Column("crn")][Required] public string Crn { get; set; }
    [JsonPropertyName("trn")][Column("trn")][Required] public string Trn { get; set; }
    [JsonPropertyName("contact")][Column("contact")][Required] public string Contact { get; set; }
    [JsonPropertyName("mobile")][Column("mobile")][Required] public string Mobile { get; set; }
    [JsonPropertyName("email")][Column("email")][Required][EmailAddress] public string Email { get; set; }
}

public record CompanyModify : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("address")][Column("address")] public string? Address { get; set; }
    [JsonPropertyName("crn")][Column("crn")] public string? Crn { get; set; }
    [JsonPropertyName("trn")][Column("trn")] public string? Trn { get; set; }
    [JsonPropertyName("contact")][Column("contact")] public string? Contact { get; set; }
    [JsonPropertyName("mobile")][Column("mobile")] public string? Mobile { get; set; }
    [JsonPropertyName("email")][Column("email")][EmailAddress] public string? Email { get; set; }
}