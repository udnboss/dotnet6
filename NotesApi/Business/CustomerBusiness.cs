using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class CustomerBusiness : Business<Customer, CustomerView, CustomerUpdate, CustomerModify, CustomerCreate, CustomerQuery>
{
    public CustomerBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(CustomerQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            

        return dataQuery;
    }

    public override CustomerQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override CustomerView GetById(string id, int maxDepth = 2)
    {
        var query = Db.Set<Customer>()
            .Select(x => new CustomerView { 
                Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Contact = x.Contact,
                  CurrencyId = x.CurrencyId,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  PaymentTerm = x.PaymentTerm,
                  Sales = new QueryResult<SaleQuery, SaleView>(new SaleQuery() { _Size = 10, _Page = 1, CustomerId = new List<string?>() { x.Id } }) { Result = x.Sales!.Select(y1 => new SaleView { Id = y1.Id,
                      CompanyId = y1.CompanyId,
                      AccountId = y1.AccountId,
                      CustomerId = y1.CustomerId,
                      CurrencyId = y1.CurrencyId,
                      Place = y1.Place,
                      Number = y1.Number,
                      Date = y1.Date,
                      Total = y1.Total,
                      TotalItems = y1.TotalItems,
                      Reference = y1.Reference,
                      Confirmed = y1.Confirmed,
                      ReferenceDate = y1.ReferenceDate,
                      DueDate = y1.DueDate }).Take(10) }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override CustomerView Create(CustomerCreate entity)
    {
        var dbSet = Db.Set<Customer>();
        var dbEntity = new Customer {
            Id = new Guid().ToString(),
            Name = entity.Name, Address = entity.Address, Contact = entity.Contact, CurrencyId = entity.CurrencyId, PaymentTerm = entity.PaymentTerm
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new CustomerView { 
                Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Contact = x.Contact,
                  CurrencyId = x.CurrencyId,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  PaymentTerm = x.PaymentTerm,
                  Sales = new QueryResult<SaleQuery, SaleView>(new SaleQuery() { _Size = 10, _Page = 1, CustomerId = new List<string?>() { x.Id } }) { Result = x.Sales!.Select(y1 => new SaleView { Id = y1.Id,
                      CompanyId = y1.CompanyId,
                      AccountId = y1.AccountId,
                      CustomerId = y1.CustomerId,
                      CurrencyId = y1.CurrencyId,
                      Place = y1.Place,
                      Number = y1.Number,
                      Date = y1.Date,
                      Total = y1.Total,
                      TotalItems = y1.TotalItems,
                      Reference = y1.Reference,
                      Confirmed = y1.Confirmed,
                      ReferenceDate = y1.ReferenceDate,
                      DueDate = y1.DueDate }).Take(10) }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override CustomerView Update(string id, CustomerUpdate entity)
    {
        var dbSet = Db.Set<Customer>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;
        existing.Address = entity.Address;
        existing.Contact = entity.Contact;
        existing.CurrencyId = entity.CurrencyId;
        existing.PaymentTerm = entity.PaymentTerm;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CustomerView Modify(string id, JsonElement entity)
    {
        var dbSet = Db.Set<Customer>();
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
            else if (propName == "address") existing.Address = prop.Value.GetString()!;
            else if (propName == "contact") existing.Contact = prop.Value.GetString()!;
            else if (propName == "currency_id") existing.CurrencyId = prop.Value.GetString()!;
            else if (propName == "payment_term") existing.PaymentTerm = prop.Value.GetInt32()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CustomerView Delete(string id)
    {
        var dbSet = Db.Set<Customer>();
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

    public override QueryResult<ClientQuery, CustomerView> GetAll(int maxDepth = 2)
    {
        return GetAll(new CustomerQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, CustomerView> GetAll(CustomerQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Customer>().Skip(query.Offset);
                           
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

        IOrderedQueryable<Customer>? sortedQ = null;
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
            .Select(x => new CustomerView { Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Contact = x.Contact,
                  CurrencyId = x.CurrencyId,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  PaymentTerm = x.PaymentTerm,
                  Sales = new QueryResult<SaleQuery, SaleView>(new SaleQuery() { _Size = 10, _Page = 1, CustomerId = new List<string?>() { x.Id } }) { Result = x.Sales!.Select(y1 => new SaleView { Id = y1.Id,
                      CompanyId = y1.CompanyId,
                      AccountId = y1.AccountId,
                      CustomerId = y1.CustomerId,
                      CurrencyId = y1.CurrencyId,
                      Place = y1.Place,
                      Number = y1.Number,
                      Date = y1.Date,
                      Total = y1.Total,
                      TotalItems = y1.TotalItems,
                      Reference = y1.Reference,
                      Confirmed = y1.Confirmed,
                      ReferenceDate = y1.ReferenceDate,
                      DueDate = y1.DueDate }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, CustomerView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

