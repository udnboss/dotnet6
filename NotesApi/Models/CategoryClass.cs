using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

[Table("category")]
public class Category : Entity
{
    [Column("name")][Required] public string Name { get; set; }
    [InverseProperty("Category")] public ICollection<Item>? Items { get; set; }
}

public record CategoryView : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [InverseProperty("Category")] public ICollection<Item>? Items { get; set; }
}

public record CategoryQuery : ClientQuery
{
     public string? Name { get; set; } = "";
    public CategoryQuery() { }
}

public record CategoryCreate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [InverseProperty("Category")] public ICollection<Item>? Items { get; set; }
}

public record CategoryUpdate : IRecord
{
    [Column("name")][Required] public string Name { get; set; }
    [InverseProperty("Category")] public ICollection<Item>? Items { get; set; }
}

public record CategoryModify : IRecord
{
    [Column("name")] public string? Name { get; set; }
    [InverseProperty("Category")] public ICollection<Item>? Items { get; set; }
}