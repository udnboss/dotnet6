using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("item")]
public class Item :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public Guid Id { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("category_id")][Column("category_id")] public Guid? CategoryId { get; set; }
    [JsonPropertyName("category")][ForeignKey("CategoryId")] public Category? Category { get; set; }
}

public record ItemView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("category_id")][Column("category_id")] public Guid? CategoryId { get; set; }
    [JsonPropertyName("category")][ForeignKey("CategoryId")] public CategoryView? Category { get; set; }
}

public record ItemQuery : ClientQuery
{
     public string? Name { get; set; } = "";
     public IEnumerable<Guid?> CategoryId { get; set; }
    public ItemQuery() { }
}

public record ItemCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("category_id")][Column("category_id")] public Guid? CategoryId { get; set; }
}

public record ItemUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("category_id")][Column("category_id")] public Guid? CategoryId { get; set; }
}

public record ItemModify : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("category_id")][Column("category_id")] public Guid? CategoryId { get; set; }
}