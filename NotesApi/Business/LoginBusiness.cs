using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class LoginBusiness : Business<Login, LoginView, LoginUpdate, LoginModify, LoginCreate, LoginQuery>
{
    public LoginBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(LoginQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "UserName", _operator: Operators.Contains, value: query.UserName));
            

        return dataQuery;
    }

    public override LoginQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "UserName") clientQuery.UserName = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override LoginView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Login>()
            .Select(x => new LoginView { 
                Id = x.Id,
                  UserName = x.UserName,
                  NormalizedUserName = x.NormalizedUserName,
                  PasswordHash = x.PasswordHash,
                  SecurityStamp = x.SecurityStamp,
                  AccessFailedCount = x.AccessFailedCount,
                  ConcurrencyStamp = x.ConcurrencyStamp,
                  Email = x.Email,
                  EmailConfirmed = x.EmailConfirmed,
                  LockoutEnabled = x.LockoutEnabled,
                  LockoutEnd = x.LockoutEnd,
                  NormalizedEmail = x.NormalizedEmail,
                  PhoneNumber = x.PhoneNumber,
                  PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                  TwoFactorEnabled = x.TwoFactorEnabled  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override LoginView Create(LoginCreate entity)
    {
        var dbSet = Db.Set<Login>();
        var dbEntity = new Login {
            Id = new Guid(),
            UserName = entity.UserName, NormalizedUserName = entity.NormalizedUserName, PasswordHash = entity.PasswordHash, SecurityStamp = entity.SecurityStamp, AccessFailedCount = entity.AccessFailedCount, ConcurrencyStamp = entity.ConcurrencyStamp, Email = entity.Email, EmailConfirmed = entity.EmailConfirmed, LockoutEnabled = entity.LockoutEnabled, LockoutEnd = entity.LockoutEnd, NormalizedEmail = entity.NormalizedEmail, PhoneNumber = entity.PhoneNumber, PhoneNumberConfirmed = entity.PhoneNumberConfirmed, TwoFactorEnabled = entity.TwoFactorEnabled
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new LoginView { 
                Id = x.Id,
                  UserName = x.UserName,
                  NormalizedUserName = x.NormalizedUserName,
                  PasswordHash = x.PasswordHash,
                  SecurityStamp = x.SecurityStamp,
                  AccessFailedCount = x.AccessFailedCount,
                  ConcurrencyStamp = x.ConcurrencyStamp,
                  Email = x.Email,
                  EmailConfirmed = x.EmailConfirmed,
                  LockoutEnabled = x.LockoutEnabled,
                  LockoutEnd = x.LockoutEnd,
                  NormalizedEmail = x.NormalizedEmail,
                  PhoneNumber = x.PhoneNumber,
                  PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                  TwoFactorEnabled = x.TwoFactorEnabled
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override LoginView Update(Guid id, LoginUpdate entity)
    {
        var dbSet = Db.Set<Login>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.UserName = entity.UserName;
        existing.NormalizedUserName = entity.NormalizedUserName;
        existing.PasswordHash = entity.PasswordHash;
        existing.SecurityStamp = entity.SecurityStamp;
        existing.AccessFailedCount = entity.AccessFailedCount;
        existing.ConcurrencyStamp = entity.ConcurrencyStamp;
        existing.Email = entity.Email;
        existing.EmailConfirmed = entity.EmailConfirmed;
        existing.LockoutEnabled = entity.LockoutEnabled;
        existing.LockoutEnd = entity.LockoutEnd;
        existing.NormalizedEmail = entity.NormalizedEmail;
        existing.PhoneNumber = entity.PhoneNumber;
        existing.PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
        existing.TwoFactorEnabled = entity.TwoFactorEnabled;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override LoginView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Login>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "userName") existing.UserName = prop.Value.GetString()!;
            else if (propName == "normalizedUserName") existing.NormalizedUserName = prop.Value.GetString()!;
            else if (propName == "passwordHash") existing.PasswordHash = prop.Value.GetString()!;
            else if (propName == "securityStamp") existing.SecurityStamp = prop.Value.GetString()!;
            else if (propName == "accessFailedCount") existing.AccessFailedCount = prop.Value.GetInt32()!;
            else if (propName == "concurrencyStamp") existing.ConcurrencyStamp = prop.Value.GetString()!;
            else if (propName == "email") existing.Email = prop.Value.GetString()!;
            else if (propName == "emailConfirmed") existing.EmailConfirmed = prop.Value.GetBoolean()!;
            else if (propName == "lockoutEnabled") existing.LockoutEnabled = prop.Value.GetBoolean()!;
            else if (propName == "lockoutEnd") existing.LockoutEnd = prop.Value.GetDateTimeOffset()!;
            else if (propName == "normalizedEmail") existing.NormalizedEmail = prop.Value.GetString()!;
            else if (propName == "phoneNumber") existing.PhoneNumber = prop.Value.GetString()!;
            else if (propName == "phoneNumberConfirmed") existing.PhoneNumberConfirmed = prop.Value.GetBoolean()!;
            else if (propName == "twoFactorEnabled") existing.TwoFactorEnabled = prop.Value.GetBoolean()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override LoginView Delete(Guid id)
    {
        var dbSet = Db.Set<Login>();
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

    public override QueryResult<ClientQuery, LoginView> GetAll(int maxDepth = 2)
    {
        return GetAll(new LoginQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, LoginView> GetAll(LoginQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Login>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                if (c.Column == "UserName" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.UserName != null && x.UserName.ToLower().Contains(v.ToLower()));
                }                   
            }
        }

        IOrderedQueryable<Login>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "UserName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.UserName) : sortedQ.ThenBy(x => x.UserName) 
                        : sortedQ is null ? q.OrderByDescending( x => x.UserName) : sortedQ.ThenByDescending(x => x.UserName);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new LoginView { Id = x.Id,
                  UserName = x.UserName,
                  NormalizedUserName = x.NormalizedUserName,
                  PasswordHash = x.PasswordHash,
                  SecurityStamp = x.SecurityStamp,
                  AccessFailedCount = x.AccessFailedCount,
                  ConcurrencyStamp = x.ConcurrencyStamp,
                  Email = x.Email,
                  EmailConfirmed = x.EmailConfirmed,
                  LockoutEnabled = x.LockoutEnabled,
                  LockoutEnd = x.LockoutEnd,
                  NormalizedEmail = x.NormalizedEmail,
                  PhoneNumber = x.PhoneNumber,
                  PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                  TwoFactorEnabled = x.TwoFactorEnabled })
            .ToList();

        var result = new QueryResult<ClientQuery, LoginView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

