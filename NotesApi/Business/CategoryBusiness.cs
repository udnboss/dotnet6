using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

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
                Id = x.Id, Name = x.Name  
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
                Id = x.Id, Name = x.Name
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

        var inputProps = typeof(CategoryUpdate).GetProperties();
        var outputProps = typeof(Category).GetProperties();

        foreach (var prop in inputProps)
        {
            if (prop.Name == "Id") continue;
            var match = outputProps.FirstOrDefault(p => p.Name == prop.Name);
            if (match is not null)
            {
                match.SetValue(existing, prop.GetValue(entity));
            }
        }

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
      
        var validProps = typeof(CategoryModify).GetProperties();
        var outputProps = typeof(Category).GetProperties();

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            if (prop.Name.ToLower() == "id") continue;
            var match = outputProps.FirstOrDefault(p => p.Name.ToLower() == prop.Name.ToLower());
            if (match is not null)
            {
                match.SetValue(existing, prop.Value.GetString());//TODO: proper mapping of type
            }
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
                            q = q.Where(x => x.Name != null && x.Name.Contains(v));
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
            .Select(x => new CategoryView { Id = x.Id, Name = x.Name })
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

