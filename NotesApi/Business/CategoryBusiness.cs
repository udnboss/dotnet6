using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class CategoryBusiness : Business<Category, CategoryView, CategoryUpdate, CategoryModify, CategoryCreate, CategoryQuery>
{
    public CategoryBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(CategoryQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            

        return dataQuery;
    }

    public override CategoryQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override CategoryView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Category>()
            .Select(x => new CategoryView { 
                Id = x.Id,
                  Name = x.Name,
                  Items = new QueryResult<ItemQuery, ItemView>(new ItemQuery() { _Size = 10, _Page = 1, CategoryId = new List<Guid?>() { x.Id } }) { Result = x.Items!.Select(y1 => new ItemView { Id = y1.Id,
                      Name = y1.Name,
                      CategoryId = y1.CategoryId }).Take(10) }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override CategoryView Create(CategoryCreate entity)
    {
        var dbSet = Db.Set<Category>();
        var dbEntity = new Category {
            Id = new Guid(),
            Name = entity.Name
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new CategoryView { 
                Id = x.Id,
                  Name = x.Name,
                  Items = new QueryResult<ItemQuery, ItemView>(new ItemQuery() { _Size = 10, _Page = 1, CategoryId = new List<Guid?>() { x.Id } }) { Result = x.Items!.Select(y1 => new ItemView { Id = y1.Id,
                      Name = y1.Name,
                      CategoryId = y1.CategoryId }).Take(10) }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override CategoryView Update(Guid id, CategoryUpdate entity)
    {
        var dbSet = Db.Set<Category>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CategoryView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Category>();
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
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CategoryView Delete(Guid id)
    {
        var dbSet = Db.Set<Category>();
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

    public override QueryResult<ClientQuery, CategoryView> GetAll(CategoryQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Category>().Skip(query.Offset);
                           
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
            }
        }

        IOrderedQueryable<Category>? sortedQ = null;
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
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new CategoryView { Id = x.Id,
                  Name = x.Name,
                  Items = new QueryResult<ItemQuery, ItemView>(new ItemQuery() { _Size = 10, _Page = 1, CategoryId = new List<Guid?>() { x.Id } }) { Result = x.Items!.Select(y1 => new ItemView { Id = y1.Id,
                      Name = y1.Name,
                      CategoryId = y1.CategoryId }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, CategoryView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

