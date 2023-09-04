using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class UserBusiness : Business<User, UserView, UserUpdate, UserModify, UserCreate, UserQuery>
{
    public UserBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(UserQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            
            dataQuery.Where.Add(new Condition(column: "Email", _operator: Operators.Contains, value: query.Email));
            

        return dataQuery;
    }

    public override UserQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
            if(c.Column == "Email") clientQuery.Email = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override UserView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<User>()
            .Select(x => new UserView { 
                Id = x.Id,
                  Name = x.Name,
                  Email = x.Email,
                  LoginId = x.LoginId,
                  Login = new LoginView { Id = x.Login!.Id,
                      UserName = x.Login!.UserName,
                      NormalizedUserName = x.Login!.NormalizedUserName,
                      PasswordHash = x.Login!.PasswordHash,
                      SecurityStamp = x.Login!.SecurityStamp,
                      AccessFailedCount = x.Login!.AccessFailedCount,
                      ConcurrencyStamp = x.Login!.ConcurrencyStamp,
                      Email = x.Login!.Email,
                      EmailConfirmed = x.Login!.EmailConfirmed,
                      LockoutEnabled = x.Login!.LockoutEnabled,
                      LockoutEnd = x.Login!.LockoutEnd,
                      NormalizedEmail = x.Login!.NormalizedEmail,
                      PhoneNumber = x.Login!.PhoneNumber,
                      PhoneNumberConfirmed = x.Login!.PhoneNumberConfirmed,
                      TwoFactorEnabled = x.Login!.TwoFactorEnabled },
                  UserRoles = new QueryResult<UserRoleQuery, UserRoleView>(new UserRoleQuery() { _Size = 10, _Page = 1, UserId = new List<Guid?>() { x.Id } }) { Result = x.UserRoles!.Select(y1 => new UserRoleView { Id = y1.Id,
                      UserId = y1.UserId,
                      RoleId = y1.RoleId }).Take(10) }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override UserView Create(UserCreate entity)
    {
        var dbSet = Db.Set<User>();
        var dbEntity = new User {
            Id = new Guid(),
            Name = entity.Name, Email = entity.Email, LoginId = entity.LoginId
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new UserView { 
                Id = x.Id,
                  Name = x.Name,
                  Email = x.Email,
                  LoginId = x.LoginId,
                  Login = new LoginView { Id = x.Login!.Id,
                      UserName = x.Login!.UserName,
                      NormalizedUserName = x.Login!.NormalizedUserName,
                      PasswordHash = x.Login!.PasswordHash,
                      SecurityStamp = x.Login!.SecurityStamp,
                      AccessFailedCount = x.Login!.AccessFailedCount,
                      ConcurrencyStamp = x.Login!.ConcurrencyStamp,
                      Email = x.Login!.Email,
                      EmailConfirmed = x.Login!.EmailConfirmed,
                      LockoutEnabled = x.Login!.LockoutEnabled,
                      LockoutEnd = x.Login!.LockoutEnd,
                      NormalizedEmail = x.Login!.NormalizedEmail,
                      PhoneNumber = x.Login!.PhoneNumber,
                      PhoneNumberConfirmed = x.Login!.PhoneNumberConfirmed,
                      TwoFactorEnabled = x.Login!.TwoFactorEnabled },
                  UserRoles = new QueryResult<UserRoleQuery, UserRoleView>(new UserRoleQuery() { _Size = 10, _Page = 1, UserId = new List<Guid?>() { x.Id } }) { Result = x.UserRoles!.Select(y1 => new UserRoleView { Id = y1.Id,
                      UserId = y1.UserId,
                      RoleId = y1.RoleId }).Take(10) }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override UserView Update(Guid id, UserUpdate entity)
    {
        var dbSet = Db.Set<User>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Name = entity.Name;
        existing.Email = entity.Email;
        existing.LoginId = entity.LoginId;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override UserView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<User>();
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
            else if (propName == "email") existing.Email = prop.Value.GetString()!;
            else if (propName == "login_id") existing.LoginId = prop.Value.GetGuid()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override UserView Delete(Guid id)
    {
        var dbSet = Db.Set<User>();
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

    public override QueryResult<ClientQuery, UserView> GetAll(int maxDepth = 2)
    {
        return GetAll(new UserQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, UserView> GetAll(UserQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<User>().Skip(query.Offset);
                           
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
                else if (c.Column == "Email" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.Email != null && x.Email.ToLower().Contains(v.ToLower()));
                }                   
            }
        }

        IOrderedQueryable<User>? sortedQ = null;
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
                


                if (s.Column == "Email")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Email) : sortedQ.ThenBy(x => x.Email) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Email) : sortedQ.ThenByDescending(x => x.Email);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new UserView { Id = x.Id,
                  Name = x.Name,
                  Email = x.Email,
                  LoginId = x.LoginId,
                  Login = new LoginView { Id = x.Login!.Id,
                      UserName = x.Login!.UserName,
                      NormalizedUserName = x.Login!.NormalizedUserName,
                      PasswordHash = x.Login!.PasswordHash,
                      SecurityStamp = x.Login!.SecurityStamp,
                      AccessFailedCount = x.Login!.AccessFailedCount,
                      ConcurrencyStamp = x.Login!.ConcurrencyStamp,
                      Email = x.Login!.Email,
                      EmailConfirmed = x.Login!.EmailConfirmed,
                      LockoutEnabled = x.Login!.LockoutEnabled,
                      LockoutEnd = x.Login!.LockoutEnd,
                      NormalizedEmail = x.Login!.NormalizedEmail,
                      PhoneNumber = x.Login!.PhoneNumber,
                      PhoneNumberConfirmed = x.Login!.PhoneNumberConfirmed,
                      TwoFactorEnabled = x.Login!.TwoFactorEnabled },
                  UserRoles = new QueryResult<UserRoleQuery, UserRoleView>(new UserRoleQuery() { _Size = 10, _Page = 1, UserId = new List<Guid?>() { x.Id } }) { Result = x.UserRoles!.Select(y1 => new UserRoleView { Id = y1.Id,
                      UserId = y1.UserId,
                      RoleId = y1.RoleId }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, UserView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

