using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class CurrencyBusiness : Business<Currency, CurrencyView, CurrencyUpdate, CurrencyModify, CurrencyCreate, CurrencyQuery>
{
    public CurrencyBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(CurrencyQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        

        return dataQuery;
    }

    public override CurrencyQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            
        }        

        return clientQuery;
    }
    
    public override CurrencyView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Currency>()
            .Select(x => new CurrencyView { 
                Id = x.Id,
                  Name = x.Name,
                  Symbol = x.Symbol  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override CurrencyView Create(CurrencyCreate entity)
    {
        var dbSet = Db.Set<Currency>();
        var dbEntity = new Currency {
            Id = new Guid(),
            Name = entity.Name, Symbol = entity.Symbol
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new CurrencyView { 
                Id = x.Id,
                  Name = x.Name,
                  Symbol = x.Symbol
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override CurrencyView Update(Guid id, CurrencyUpdate entity)
    {
        var dbSet = Db.Set<Currency>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;
        existing.Symbol = entity.Symbol;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CurrencyView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Currency>();
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
            else if (propName == "symbol") existing.Symbol = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CurrencyView Delete(Guid id)
    {
        var dbSet = Db.Set<Currency>();
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

    public override QueryResult<ClientQuery, CurrencyView> GetAll(int maxDepth = 2)
    {
        return GetAll(new CurrencyQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, CurrencyView> GetAll(CurrencyQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Currency>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                                   
            }
        }

        IOrderedQueryable<Currency>? sortedQ = null;
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
            .Select(x => new CurrencyView { Id = x.Id,
                  Name = x.Name,
                  Symbol = x.Symbol })
            .ToList();

        var result = new QueryResult<ClientQuery, CurrencyView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

