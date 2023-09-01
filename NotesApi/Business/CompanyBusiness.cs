using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class CompanyBusiness : Business<Company, CompanyView, CompanyUpdate, CompanyModify, CompanyCreate, CompanyQuery>
{
    public CompanyBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(CompanyQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            

        return dataQuery;
    }

    public override CompanyQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override CompanyView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Company>()
            .Select(x => new CompanyView { 
                Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Crn = x.Crn,
                  Trn = x.Trn,
                  Contact = x.Contact,
                  Mobile = x.Mobile,
                  Email = x.Email  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override CompanyView Create(CompanyCreate entity)
    {
        var dbSet = Db.Set<Company>();
        var dbEntity = new Company {
            Id = new Guid(),
            Name = entity.Name, Address = entity.Address, Crn = entity.Crn, Trn = entity.Trn, Contact = entity.Contact, Mobile = entity.Mobile, Email = entity.Email
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new CompanyView { 
                Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Crn = x.Crn,
                  Trn = x.Trn,
                  Contact = x.Contact,
                  Mobile = x.Mobile,
                  Email = x.Email
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override CompanyView Update(Guid id, CompanyUpdate entity)
    {
        var dbSet = Db.Set<Company>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;
        existing.Address = entity.Address;
        existing.Crn = entity.Crn;
        existing.Trn = entity.Trn;
        existing.Contact = entity.Contact;
        existing.Mobile = entity.Mobile;
        existing.Email = entity.Email;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CompanyView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Company>();
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
            else if (propName == "crn") existing.Crn = prop.Value.GetString()!;
            else if (propName == "trn") existing.Trn = prop.Value.GetString()!;
            else if (propName == "contact") existing.Contact = prop.Value.GetString()!;
            else if (propName == "mobile") existing.Mobile = prop.Value.GetString()!;
            else if (propName == "email") existing.Email = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override CompanyView Delete(Guid id)
    {
        var dbSet = Db.Set<Company>();
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

    public override QueryResult<ClientQuery, CompanyView> GetAll(CompanyQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Company>().Skip(query.Offset);
                           
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

        IOrderedQueryable<Company>? sortedQ = null;
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
            .Select(x => new CompanyView { Id = x.Id,
                  Name = x.Name,
                  Address = x.Address,
                  Crn = x.Crn,
                  Trn = x.Trn,
                  Contact = x.Contact,
                  Mobile = x.Mobile,
                  Email = x.Email })
            .ToList();

        var result = new QueryResult<ClientQuery, CompanyView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

