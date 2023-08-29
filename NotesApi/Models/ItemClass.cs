using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("item")]
public class Item : Entity
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("category_id")] public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public Category? Category { get; set; }
}

public record ItemView : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [Column("category_id")] public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")] public CategoryView? Category { get; set; }
}

public record ItemQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public Guid? CategoryId { get; set; } = null;
    public ItemQuery() { }
}

public record ItemCreate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("category_id")] public Guid? CategoryId { get; set; }
}

public record ItemUpdate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [Column("category_id")] public Guid? CategoryId { get; set; }
}

public record ItemModify : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [Column("category_id")] public Guid? CategoryId { get; set; }
}