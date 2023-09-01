using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

public class ItemBusiness : Business<Item, ItemView, ItemUpdate, ItemModify, ItemCreate, ItemQuery>
{
    public ItemBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(ItemQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            
            dataQuery.Where.Add(new Condition(column: "CategoryId", _operator: Operators.IsIn, value: query.CategoryId));
            

        return dataQuery;
    }

    public override ItemQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
            if(c.Column == "CategoryId" && c.Values is not null) clientQuery.CategoryId = c.Values.Cast<Guid?>();
        }        

        return clientQuery;
    }
    
    public override ItemView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Item>()
            .Select(x => new ItemView { 
                Id = x.Id,
                  Name = x.Name,
                  CategoryId = x.CategoryId,
                  Category = new CategoryView { Id = x.Category!.Id,
                      Name = x.Category!.Name }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override ItemView Create(ItemCreate entity)
    {
        var dbSet = Db.Set<Item>();
        var dbEntity = new Item {
            Id = new Guid(),
            Name = entity.Name, CategoryId = entity.CategoryId
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new ItemView { 
                Id = x.Id,
                  Name = x.Name,
                  CategoryId = x.CategoryId,
                  Category = new CategoryView { Id = x.Category!.Id,
                      Name = x.Category!.Name }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override ItemView Update(Guid id, ItemUpdate entity)
    {
        var dbSet = Db.Set<Item>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;
        existing.CategoryId = entity.CategoryId;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override ItemView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Item>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "name") existing.Name = prop.Value.GetString()!;
            else if (propName == "category_id") existing.CategoryId = prop.Value.GetGuid()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override ItemView Delete(Guid id)
    {
        var dbSet = Db.Set<Item>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }
        var beforeDelete = GetById(id);
        dbSet.Remove(existing);
        Db.SaveChanges();

        return beforeDelete;
    }

    public override QueryResult<ClientQuery, ItemView> GetAll(ItemQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Item>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                
                    if (c.Column == "Name" && c.Operator == Operators.Contains && c.Value != null) 
                    {
                        var v = c.Value.ToString();
                        if(!string.IsNullOrWhiteSpace(v))
                            q = q.Where(x => x.Name != null && x.Name.ToLower().Contains(v.ToLower()));
                    }


                    if (c.Column == "CategoryId" && c.Operator == Operators.IsIn && c.Values != null) 
                    {
                        var v = c.Values.Cast<Guid?>().ToList();
                        q = q.Where(x => x.CategoryId != null && v.Contains(x.CategoryId));
                    }                   
            }
        }

        IOrderedQueryable<Item>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "Name")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Name) : sortedQ.ThenBy(x => x.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Name) : sortedQ.ThenByDescending(x => x.Name);
                }
                


                if (s.Column == "CategoryName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Category!.Name) : sortedQ.ThenBy(x => x.Category!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Category!.Name) : sortedQ.ThenByDescending(x => x.Category!.Name);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new ItemView { Id = x.Id,
                  Name = x.Name,
                  CategoryId = x.CategoryId,
                  Category = new CategoryView { Id = x.Category!.Id,
                      Name = x.Category!.Name } })
            .ToList();

        var result = new QueryResult<ClientQuery, ItemView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

