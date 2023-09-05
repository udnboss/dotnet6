using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class SaleBusiness : Business<Sale, SaleView, SaleUpdate, SaleModify, SaleCreate, SaleQuery>
{
    public SaleBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(SaleQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "CustomerId", _operator: Operators.IsIn, value: query.CustomerId));
            
            dataQuery.Where.Add(new Condition(column: "AccountId", _operator: Operators.IsIn, value: query.AccountId));
            
            dataQuery.Where.Add(new Condition(column: "Number", _operator: Operators.Contains, value: query.Number));
            
            dataQuery.Where.Add(new Condition(column: "Date", _operator: Operators.Between, value: query.Date));
            
            dataQuery.Where.Add(new Condition(column: "ReferenceDate", _operator: Operators.Between, value: query.ReferenceDate));
            

        return dataQuery;
    }

    public override SaleQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "CustomerId" && c.Values is not null) clientQuery.CustomerId = c.Values.Cast<string>();
            if(c.Column == "AccountId" && c.Values is not null) clientQuery.AccountId = c.Values.Cast<string>();
            if(c.Column == "Number") clientQuery.Number = c.Value as int?;
            if(c.Column == "Date") clientQuery.Date = c.Value as DateTime?;
            if(c.Column == "ReferenceDate") clientQuery.ReferenceDate = c.Value as DateTime?;
        }        

        return clientQuery;
    }
    
    public override SaleView GetById(string id, int maxDepth = 2)
    {
        var query = Db.Set<Sale>()
            .Select(x => new SaleView { 
                Id = x.Id,
                  CompanyId = x.CompanyId,
                  AccountId = x.AccountId,
                  CustomerId = x.CustomerId,
                  CurrencyId = x.CurrencyId,
                  Place = x.Place,
                  Number = x.Number,
                  Date = x.Date,
                  Total = x.Total,
                  TotalItems = x.TotalItems,
                  Reference = x.Reference,
                  Confirmed = x.Confirmed,
                  ReferenceDate = x.ReferenceDate,
                  DueDate = x.DueDate,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  Customer = new CustomerView { Id = x.Customer!.Id,
                      Name = x.Customer!.Name,
                      Address = x.Customer!.Address,
                      Contact = x.Customer!.Contact,
                      CurrencyId = x.Customer!.CurrencyId,
                      PaymentTerm = x.Customer!.PaymentTerm },
                  Account = new AccountView { Id = x.Account!.Id,
                      Label = x.Account!.Label,
                      BankName = x.Account!.BankName,
                      BankAddress = x.Account!.BankAddress,
                      BankSwift = x.Account!.BankSwift,
                      AccountName = x.Account!.AccountName,
                      AccountIban = x.Account!.AccountIban,
                      AccountAddress = x.Account!.AccountAddress },
                  Company = new CompanyView { Id = x.Company!.Id,
                      Name = x.Company!.Name,
                      Address = x.Company!.Address,
                      Crn = x.Company!.Crn,
                      Trn = x.Company!.Trn,
                      Contact = x.Company!.Contact,
                      Mobile = x.Company!.Mobile,
                      Email = x.Company!.Email },
                  Items = new QueryResult<SaleItemQuery, SaleItemView>(new SaleItemQuery() { _Size = 10, _Page = 1, SaleId = new List<string?>() { x.Id } }) { Result = x.Items!.Select(y1 => new SaleItemView { Id = y1.Id,
                      SaleId = y1.SaleId,
                      ItemId = y1.ItemId,
                      Description = y1.Description,
                      Quantity = y1.Quantity,
                      Price = y1.Price,
                      Total = y1.Total }).Take(10) }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override SaleView Create(SaleCreate entity)
    {
        var dbSet = Db.Set<Sale>();
        var dbEntity = new Sale {
            Id = new Guid().ToString(),
            CompanyId = entity.CompanyId, AccountId = entity.AccountId, CustomerId = entity.CustomerId, CurrencyId = entity.CurrencyId, Place = entity.Place, Date = entity.Date, Reference = entity.Reference, Confirmed = entity.Confirmed, ReferenceDate = entity.ReferenceDate, DueDate = entity.DueDate
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new SaleView { 
                Id = x.Id,
                  CompanyId = x.CompanyId,
                  AccountId = x.AccountId,
                  CustomerId = x.CustomerId,
                  CurrencyId = x.CurrencyId,
                  Place = x.Place,
                  Number = x.Number,
                  Date = x.Date,
                  Total = x.Total,
                  TotalItems = x.TotalItems,
                  Reference = x.Reference,
                  Confirmed = x.Confirmed,
                  ReferenceDate = x.ReferenceDate,
                  DueDate = x.DueDate,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  Customer = new CustomerView { Id = x.Customer!.Id,
                      Name = x.Customer!.Name,
                      Address = x.Customer!.Address,
                      Contact = x.Customer!.Contact,
                      CurrencyId = x.Customer!.CurrencyId,
                      PaymentTerm = x.Customer!.PaymentTerm },
                  Account = new AccountView { Id = x.Account!.Id,
                      Label = x.Account!.Label,
                      BankName = x.Account!.BankName,
                      BankAddress = x.Account!.BankAddress,
                      BankSwift = x.Account!.BankSwift,
                      AccountName = x.Account!.AccountName,
                      AccountIban = x.Account!.AccountIban,
                      AccountAddress = x.Account!.AccountAddress },
                  Company = new CompanyView { Id = x.Company!.Id,
                      Name = x.Company!.Name,
                      Address = x.Company!.Address,
                      Crn = x.Company!.Crn,
                      Trn = x.Company!.Trn,
                      Contact = x.Company!.Contact,
                      Mobile = x.Company!.Mobile,
                      Email = x.Company!.Email },
                  Items = new QueryResult<SaleItemQuery, SaleItemView>(new SaleItemQuery() { _Size = 10, _Page = 1, SaleId = new List<string?>() { x.Id } }) { Result = x.Items!.Select(y1 => new SaleItemView { Id = y1.Id,
                      SaleId = y1.SaleId,
                      ItemId = y1.ItemId,
                      Description = y1.Description,
                      Quantity = y1.Quantity,
                      Price = y1.Price,
                      Total = y1.Total }).Take(10) }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override SaleView Update(string id, SaleUpdate entity)
    {
        var dbSet = Db.Set<Sale>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.CompanyId = entity.CompanyId;
        existing.AccountId = entity.AccountId;
        existing.CustomerId = entity.CustomerId;
        existing.CurrencyId = entity.CurrencyId;
        existing.Place = entity.Place;
        existing.Reference = entity.Reference;
        existing.Confirmed = entity.Confirmed;
        existing.ReferenceDate = entity.ReferenceDate;
        existing.DueDate = entity.DueDate;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override SaleView Modify(string id, JsonElement entity)
    {
        var dbSet = Db.Set<Sale>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "company_id") existing.CompanyId = prop.Value.GetString()!;
            else if (propName == "account_id") existing.AccountId = prop.Value.GetString()!;
            else if (propName == "customer_id") existing.CustomerId = prop.Value.GetString()!;
            else if (propName == "currency_id") existing.CurrencyId = prop.Value.GetString()!;
            else if (propName == "place") existing.Place = prop.Value.GetString()!;
            else if (propName == "reference") existing.Reference = prop.Value.GetString()!;
            else if (propName == "confirmed") existing.Confirmed = prop.Value.GetBoolean()!;
            else if (propName == "reference_date") existing.ReferenceDate = prop.Value.GetDateTime()!;
            else if (propName == "due_date") existing.DueDate = prop.Value.GetDateTime()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override SaleView Delete(string id)
    {
        var dbSet = Db.Set<Sale>();
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

    public override QueryResult<ClientQuery, SaleView> GetAll(int maxDepth = 2)
    {
        return GetAll(new SaleQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, SaleView> GetAll(SaleQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Sale>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                if (c.Column == "CustomerId" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.CustomerId != null && x.CustomerId.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "AccountId" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.AccountId != null && x.AccountId.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "Number" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = (int)c.Value;
                    q = q.Where(x => x.Number >= v);
                    
                    if (c.Value2 is not null)
                    {
                        var v2 = (int)c.Value2;
                        q = q.Where(x => x.Number <= v2);
                    }                                            
                }
                else if (c.Column == "Date" && c.Operator == Operators.Between && c.Value != null) 
                {
                    var v = (DateTime)c.Value;
                    q = q.Where(x => x.Date >= v);
                                            
                    if (c.Value2 is not null)
                    {
                        var v2 = (DateTime)c.Value2;
                        q = q.Where(x => x.Date <= v2);
                    }                    
                }
                else if (c.Column == "ReferenceDate" && c.Operator == Operators.Between && c.Value != null) 
                {
                    var v = (DateTime)c.Value;
                    q = q.Where(x => x.ReferenceDate >= v);
                                            
                    if (c.Value2 is not null)
                    {
                        var v2 = (DateTime)c.Value2;
                        q = q.Where(x => x.ReferenceDate <= v2);
                    }                    
                }                   
            }
        }

        IOrderedQueryable<Sale>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "Number")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Number) : sortedQ.ThenBy(x => x.Number) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Number) : sortedQ.ThenByDescending(x => x.Number);
                }
                


                if (s.Column == "Date")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Date) : sortedQ.ThenBy(x => x.Date) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Date) : sortedQ.ThenByDescending(x => x.Date);
                }
                


                if (s.Column == "CustomerName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Customer!.Name) : sortedQ.ThenBy(x => x.Customer!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Customer!.Name) : sortedQ.ThenByDescending(x => x.Customer!.Name);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new SaleView { Id = x.Id,
                  CompanyId = x.CompanyId,
                  AccountId = x.AccountId,
                  CustomerId = x.CustomerId,
                  CurrencyId = x.CurrencyId,
                  Place = x.Place,
                  Number = x.Number,
                  Date = x.Date,
                  Total = x.Total,
                  TotalItems = x.TotalItems,
                  Reference = x.Reference,
                  Confirmed = x.Confirmed,
                  ReferenceDate = x.ReferenceDate,
                  DueDate = x.DueDate,
                  Currency = new CurrencyView { Id = x.Currency!.Id,
                      Name = x.Currency!.Name,
                      Symbol = x.Currency!.Symbol },
                  Customer = new CustomerView { Id = x.Customer!.Id,
                      Name = x.Customer!.Name,
                      Address = x.Customer!.Address,
                      Contact = x.Customer!.Contact,
                      CurrencyId = x.Customer!.CurrencyId,
                      PaymentTerm = x.Customer!.PaymentTerm },
                  Account = new AccountView { Id = x.Account!.Id,
                      Label = x.Account!.Label,
                      BankName = x.Account!.BankName,
                      BankAddress = x.Account!.BankAddress,
                      BankSwift = x.Account!.BankSwift,
                      AccountName = x.Account!.AccountName,
                      AccountIban = x.Account!.AccountIban,
                      AccountAddress = x.Account!.AccountAddress },
                  Company = new CompanyView { Id = x.Company!.Id,
                      Name = x.Company!.Name,
                      Address = x.Company!.Address,
                      Crn = x.Company!.Crn,
                      Trn = x.Company!.Trn,
                      Contact = x.Company!.Contact,
                      Mobile = x.Company!.Mobile,
                      Email = x.Company!.Email },
                  Items = new QueryResult<SaleItemQuery, SaleItemView>(new SaleItemQuery() { _Size = 10, _Page = 1, SaleId = new List<string?>() { x.Id } }) { Result = x.Items!.Select(y1 => new SaleItemView { Id = y1.Id,
                      SaleId = y1.SaleId,
                      ItemId = y1.ItemId,
                      Description = y1.Description,
                      Quantity = y1.Quantity,
                      Price = y1.Price,
                      Total = y1.Total }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, SaleView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

