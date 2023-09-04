using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
            if(c.Column == "SaleId" && c.Values is not null) clientQuery.SaleId = c.Values.Cast<Guid?>();
            if(c.Column == "ItemId" && c.Values is not null) clientQuery.ItemId = c.Values.Cast<Guid?>();
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

        existing.SaleId = entity.SaleId;
        existing.ItemId = entity.ItemId;
        existing.Description = entity.Description;
        existing.Quantity = entity.Quantity;
        existing.Price = entity.Price;

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

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "sale_id") existing.SaleId = prop.Value.GetGuid()!;
            else if (propName == "item_id") existing.ItemId = prop.Value.GetGuid()!;
            else if (propName == "description") existing.Description = prop.Value.GetString()!;
            else if (propName == "quantity") existing.Quantity = prop.Value.GetInt32()!;
            else if (propName == "price") existing.Price = prop.Value.GetDecimal()!;
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

    public override QueryResult<ClientQuery, SaleItemView> GetAll(int maxDepth = 2)
    {
        return GetAll(new SaleItemQuery(), new DataQuery(), maxDepth);
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
                if (c.Column == "SaleId" && c.Operator == Operators.IsIn && c.Values != null) 
                {
                    var v = c.Values.Cast<Guid?>().ToList();
                    q = q.Where(x => v.Contains(x.SaleId));
                }
                else if (c.Column == "ItemId" && c.Operator == Operators.IsIn && c.Values != null) 
                {
                    var v = c.Values.Cast<Guid?>().ToList();
                    q = q.Where(x => v.Contains(x.ItemId));
                }                   
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

