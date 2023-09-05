using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class AccountBusiness : Business<Account, AccountView, AccountUpdate, AccountModify, AccountCreate, AccountQuery>
{
    public AccountBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(AccountQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Label", _operator: Operators.Contains, value: query.Label));
            
            dataQuery.Where.Add(new Condition(column: "BankName", _operator: Operators.Contains, value: query.BankName));
            

        return dataQuery;
    }

    public override AccountQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Label") clientQuery.Label = c.Value as string;
            if(c.Column == "BankName") clientQuery.BankName = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override AccountView GetById(string id, int maxDepth = 2)
    {
        var query = Db.Set<Account>()
            .Select(x => new AccountView { 
                Id = x.Id,
                  Label = x.Label,
                  BankName = x.BankName,
                  BankAddress = x.BankAddress,
                  BankSwift = x.BankSwift,
                  AccountName = x.AccountName,
                  AccountIban = x.AccountIban,
                  AccountAddress = x.AccountAddress  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override AccountView Create(AccountCreate entity)
    {
        var dbSet = Db.Set<Account>();
        var dbEntity = new Account {
            Id = new Guid().ToString(),
            Label = entity.Label, BankName = entity.BankName, BankAddress = entity.BankAddress, BankSwift = entity.BankSwift, AccountName = entity.AccountName, AccountIban = entity.AccountIban, AccountAddress = entity.AccountAddress
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new AccountView { 
                Id = x.Id,
                  Label = x.Label,
                  BankName = x.BankName,
                  BankAddress = x.BankAddress,
                  BankSwift = x.BankSwift,
                  AccountName = x.AccountName,
                  AccountIban = x.AccountIban,
                  AccountAddress = x.AccountAddress
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override AccountView Update(string id, AccountUpdate entity)
    {
        var dbSet = Db.Set<Account>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Label = entity.Label;
        existing.BankName = entity.BankName;
        existing.BankAddress = entity.BankAddress;
        existing.BankSwift = entity.BankSwift;
        existing.AccountName = entity.AccountName;
        existing.AccountIban = entity.AccountIban;
        existing.AccountAddress = entity.AccountAddress;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override AccountView Modify(string id, JsonElement entity)
    {
        var dbSet = Db.Set<Account>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "label") existing.Label = prop.Value.GetString()!;
            else if (propName == "bank_name") existing.BankName = prop.Value.GetString()!;
            else if (propName == "bank_address") existing.BankAddress = prop.Value.GetString()!;
            else if (propName == "bank_swift") existing.BankSwift = prop.Value.GetString()!;
            else if (propName == "account_name") existing.AccountName = prop.Value.GetString()!;
            else if (propName == "account_iban") existing.AccountIban = prop.Value.GetString()!;
            else if (propName == "account_address") existing.AccountAddress = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override AccountView Delete(string id)
    {
        var dbSet = Db.Set<Account>();
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

    public override QueryResult<ClientQuery, AccountView> GetAll(int maxDepth = 2)
    {
        return GetAll(new AccountQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, AccountView> GetAll(AccountQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Account>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                if (c.Column == "Label" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.Label != null && x.Label.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "BankName" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.BankName != null && x.BankName.ToLower().Contains(v.ToLower()));
                }                   
            }
        }

        IOrderedQueryable<Account>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "Label")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Label) : sortedQ.ThenBy(x => x.Label) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Label) : sortedQ.ThenByDescending(x => x.Label);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new AccountView { Id = x.Id,
                  Label = x.Label,
                  BankName = x.BankName,
                  BankAddress = x.BankAddress,
                  BankSwift = x.BankSwift,
                  AccountName = x.AccountName,
                  AccountIban = x.AccountIban,
                  AccountAddress = x.AccountAddress })
            .ToList();

        var result = new QueryResult<ClientQuery, AccountView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

