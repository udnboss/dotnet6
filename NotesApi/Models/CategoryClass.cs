using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618

[Table("category")]
public class Category :  IEntity
{
    [Key][Column("id")][JsonPropertyName("id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Id { get; set; }
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
    [JsonPropertyName("items")][InverseProperty("Category")] public IEnumerable<Item>? Items { get; set; }
}

public record CategoryView : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
    [JsonPropertyName("items")][InverseProperty("Category")] public QueryResult<ItemQuery, ItemView>? Items { get; set; }
}

public record CategoryQuery : ClientQuery
{
     public string? Name { get; set; } = "";
    public CategoryQuery() { }
}

public record CategoryCreate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
}

public record CategoryUpdate : IRecord
{
    [JsonPropertyName("name")][Column("name")][Required] public string Name { get; set; }
}

public record CategoryModify : IRecord
{
    [JsonPropertyName("name")][Column("name")] public string? Name { get; set; }
}