using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

public class SaleItemBusiness : Business<SaleItem, SaleItemView, SaleItemUpdate, SaleItemModify, SaleItemCreate, SaleItemQuery>
{
    public SaleItemBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(SaleItemQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "SaleId", _operator: Operators.IsIn, value: query.SaleId));
            
            dataQuery.Where.Add(new Condition(column: "ItemId", _operator: Operators.IsIn, value: query.ItemId));
            

        return dataQuery;
    }

    public override SaleItemQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            
            if(c.Column == "SaleId") clientQuery.SaleId = c.Value as Guid?;
            
            if(c.Column == "ItemId") clientQuery.ItemId = c.Value as Guid?;
            
        }        

        return clientQuery;
    }
    
    public override SaleItemView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<SaleItem>()
            .Select(x => new SaleItemView { 
                Id = x.Id,
                  SaleId = x.SaleId,
                  ItemId = x.ItemId,
                  Description = x.Description,
                  Quantity = x.Quantity,
                  Price = x.Price,
                  Total = x.Total,
                  Sale = new SaleView { Id = x.Sale!.Id,
                      CompanyId = x.Sale!.CompanyId,
                      AccountId = x.Sale!.AccountId,
                      CustomerId = x.Sale!.CustomerId,
                      CurrencyId = x.Sale!.CurrencyId,
                      Place = x.Sale!.Place,
                      Number = x.Sale!.Number,
                      Date = x.Sale!.Date,
                      Total = x.Sale!.Total,
                      TotalItems = x.Sale!.TotalItems,
                      Reference = x.Sale!.Reference,
                      Confirmed = x.Sale!.Confirmed,
                      ReferenceDate = x.Sale!.ReferenceDate,
                      DueDate = x.Sale!.DueDate },
                  Item = new ItemView { Id = x.Item!.Id,
                      Name = x.Item!.Name,
                      CategoryId = x.Item!.CategoryId }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override SaleItemView Create(SaleItemCreate entity)
    {
        var dbSet = Db.Set<SaleItem>();
        var dbEntity = new SaleItem {
            Id = new Guid(),
            SaleId = entity.SaleId, ItemId = entity.ItemId, Description = entity.Description, Quantity = entity.Quantity, Price = entity.Price
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new SaleItemView { 
                Id = x.Id,
                  SaleId = x.SaleId,
                  ItemId = x.ItemId,
                  Description = x.Description,
                  Quantity = x.Quantity,
                  Price = x.Price,
                  Total = x.Total,
                  Sale = new SaleView { Id = x.Sale!.Id,
                      CompanyId = x.Sale!.CompanyId,
                      AccountId = x.Sale!.AccountId,
                      CustomerId = x.Sale!.CustomerId,
                      CurrencyId = x.Sale!.CurrencyId,
                      Place = x.Sale!.Place,
                      Number = x.Sale!.Number,
                      Date = x.Sale!.Date,
                      Total = x.Sale!.Total,
                      TotalItems = x.Sale!.TotalItems,
                      Reference = x.Sale!.Reference,
                      Confirmed = x.Sale!.Confirmed,
                      ReferenceDate = x.Sale!.ReferenceDate,
                      DueDate = x.Sale!.DueDate },
                  Item = new ItemView { Id = x.Item!.Id,
                      Name = x.Item!.Name,
                      CategoryId = x.Item!.CategoryId }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override SaleItemView Update(Guid id, SaleItemUpdate entity)
    {
        var dbSet = Db.Set<SaleItem>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        var inputProps = typeof(SaleItemUpdate).GetProperties();
        var outputProps = typeof(SaleItem).GetProperties();

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

    public override SaleItemView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<SaleItem>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }
      
        var validProps = typeof(SaleItemModify).GetProperties();
        var outputProps = typeof(SaleItem).GetProperties();

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

    public override SaleItemView Delete(Guid id)
    {
        var dbSet = Db.Set<SaleItem>();
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

    public override QueryResult<ClientQuery, SaleItemView> GetAll(SaleItemQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<SaleItem>().Skip(query.Offset);
                           
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

        IOrderedQueryable<SaleItem>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "ItemName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Item!.Name) : sortedQ.ThenBy(x => x.Item!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Item!.Name) : sortedQ.ThenByDescending(x => x.Item!.Name);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new SaleItemView { Id = x.Id,
                  SaleId = x.SaleId,
                  ItemId = x.ItemId,
                  Description = x.Description,
                  Quantity = x.Quantity,
                  Price = x.Price,
                  Total = x.Total,
                  Sale = new SaleView { Id = x.Sale!.Id,
                      CompanyId = x.Sale!.CompanyId,
                      AccountId = x.Sale!.AccountId,
                      CustomerId = x.Sale!.CustomerId,
                      CurrencyId = x.Sale!.CurrencyId,
                      Place = x.Sale!.Place,
                      Number = x.Sale!.Number,
                      Date = x.Sale!.Date,
                      Total = x.Sale!.Total,
                      TotalItems = x.Sale!.TotalItems,
                      Reference = x.Sale!.Reference,
                      Confirmed = x.Sale!.Confirmed,
                      ReferenceDate = x.Sale!.ReferenceDate,
                      DueDate = x.Sale!.DueDate },
                  Item = new ItemView { Id = x.Item!.Id,
                      Name = x.Item!.Name,
                      CategoryId = x.Item!.CategoryId } })
            .ToList();

        var result = new QueryResult<ClientQuery, SaleItemView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

