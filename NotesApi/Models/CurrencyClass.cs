using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("currency")]
public class Currency :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("symbol")][Column("symbol")][Required] public string Symbol { get; set; }
}

public record CurrencyView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("symbol")][Column("symbol")] public string? Symbol { get; set; }
}

public record CurrencyQuery : ClientQuery
{
    
    public CurrencyQuery() { }
}

public record CurrencyCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("symbol")][Column("symbol")][Required] public string Symbol { get; set; }
}

public record CurrencyUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("symbol")][Column("symbol")][Required] public string Symbol { get; set; }
}

public record CurrencyModify : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("symbol")][Column("symbol")] public string? Symbol { get; set; }
}