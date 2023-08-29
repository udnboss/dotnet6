using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("saleItem")]
public class SaleItem : Entity
{
    [Column("sale_id")][Required] public Guid SaleId { get; set; }
    [Column("item_id")][Required] public Guid ItemId { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("quantity")][Required] public int Quantity { get; set; }
    [Column("price")][Required] public decimal Price { get; set; }
    [Column("total")] public decimal? Total { get; set; }
    [ForeignKey("SaleId")] public Sale? Sale { get; set; }
    [ForeignKey("ItemId")] public Item? Item { get; set; }
}

public record SaleItemView : IRecord
{
    [Column("sale_id")] public Guid? SaleId { get; set; }
    [Column("item_id")] public Guid? ItemId { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("quantity")] public int? Quantity { get; set; }
    [Column("price")] public decimal? Price { get; set; }
    [Column("total")] public decimal? Total { get; set; }
    [ForeignKey("SaleId")] public SaleView? Sale { get; set; }
    [ForeignKey("ItemId")] public ItemView? Item { get; set; }
}

public record SaleItemQuery : ClientQuery
{
    
    public SaleItemQuery() { }
}

public record SaleItemCreate : IRecord
{
    [Column("sale_id")][Required] public Guid SaleId { get; set; }
    [Column("item_id")][Required] public Guid ItemId { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("quantity")][Required] public int Quantity { get; set; }
    [Column("price")][Required] public decimal Price { get; set; }
}

public record SaleItemUpdate : IRecord
{
    [Column("sale_id")][Required] public Guid SaleId { get; set; }
    [Column("item_id")][Required] public Guid ItemId { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("quantity")][Required] public int Quantity { get; set; }
    [Column("price")][Required] public decimal Price { get; set; }
}

public record SaleItemModify : IRecord
{
    [Column("sale_id")] public Guid? SaleId { get; set; }
    [Column("item_id")] public Guid? ItemId { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("quantity")] public int? Quantity { get; set; }
    [Column("price")] public decimal? Price { get; set; }
}