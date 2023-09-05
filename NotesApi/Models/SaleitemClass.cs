using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("saleItem")]
public class SaleItem :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("sale_id")][Column("sale_id")][Required] public string SaleId { get; set; }
    [JsonPropertyName("item_id")][Column("item_id")][Required] public string ItemId { get; set; }
    [JsonPropertyName("description")][Column("description")] public string? Description { get; set; }
    [JsonPropertyName("quantity")][Column("quantity")][Required][Range(1, int.MaxValue)] public int Quantity { get; set; }
    [JsonPropertyName("price")][Column("price")][Required][Range(0, double.MaxValue)] public decimal Price { get; set; }
    [JsonPropertyName("total")][Column("total")][Range(double.MinValue, double.MaxValue)] public decimal? Total { get; set; }
    [JsonPropertyName("sale")][ForeignKey("SaleId")] public Sale? Sale { get; set; }
    [JsonPropertyName("item")][ForeignKey("ItemId")] public Item? Item { get; set; }
}

public record SaleItemView : IRecord
{
    [JsonPropertyName("sale_id")][Column("sale_id")] public string? SaleId { get; set; }
    [JsonPropertyName("item_id")][Column("item_id")] public string? ItemId { get; set; }
    [JsonPropertyName("description")][Column("description")] public string? Description { get; set; }
    [JsonPropertyName("quantity")][Column("quantity")] public int? Quantity { get; set; }
    [JsonPropertyName("price")][Column("price")] public decimal? Price { get; set; }
    [JsonPropertyName("total")][Column("total")] public decimal? Total { get; set; }
    [JsonPropertyName("sale")][ForeignKey("SaleId")] public SaleView? Sale { get; set; }
    [JsonPropertyName("item")][ForeignKey("ItemId")] public ItemView? Item { get; set; }
}

public record SaleItemQuery : ClientQuery
{
     public IEnumerable<string?> SaleId { get; set; }
     public IEnumerable<string?> ItemId { get; set; }
    public SaleItemQuery() { }
}

public record SaleItemCreate : IRecord
{
    [JsonPropertyName("sale_id")][Column("sale_id")][Required] public string SaleId { get; set; }
    [JsonPropertyName("item_id")][Column("item_id")][Required] public string ItemId { get; set; }
    [JsonPropertyName("description")][Column("description")] public string? Description { get; set; }
    [JsonPropertyName("quantity")][Column("quantity")][Required][Range(1, int.MaxValue)] public int Quantity { get; set; }
    [JsonPropertyName("price")][Column("price")][Required][Range(0, double.MaxValue)] public decimal Price { get; set; }
}

public record SaleItemUpdate : IRecord
{
    [JsonPropertyName("sale_id")][Column("sale_id")][Required] public string SaleId { get; set; }
    [JsonPropertyName("item_id")][Column("item_id")][Required] public string ItemId { get; set; }
    [JsonPropertyName("description")][Column("description")] public string? Description { get; set; }
    [JsonPropertyName("quantity")][Column("quantity")][Required][Range(1, int.MaxValue)] public int Quantity { get; set; }
    [JsonPropertyName("price")][Column("price")][Required][Range(0, double.MaxValue)] public decimal Price { get; set; }
}

public record SaleItemModify : IRecord
{
    [JsonPropertyName("sale_id")][Column("sale_id")] public string? SaleId { get; set; }
    [JsonPropertyName("item_id")][Column("item_id")] public string? ItemId { get; set; }
    [JsonPropertyName("description")][Column("description")] public string? Description { get; set; }
    [JsonPropertyName("quantity")][Column("quantity")][Range(1, int.MaxValue)] public int? Quantity { get; set; }
    [JsonPropertyName("price")][Column("price")][Range(0, double.MaxValue)] public decimal? Price { get; set; }
}