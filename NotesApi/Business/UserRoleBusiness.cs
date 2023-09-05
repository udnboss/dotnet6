using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class UserRoleBusiness : Business<UserRole, UserRoleView, UserRoleUpdate, UserRoleModify, UserRoleCreate, UserRoleQuery>
{
    public UserRoleBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(UserRoleQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "UserId", _operator: Operators.IsIn, value: query.UserId));
            
            dataQuery.Where.Add(new Condition(column: "RoleId", _operator: Operators.IsIn, value: query.RoleId));
            

        return dataQuery;
    }

    public override UserRoleQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "UserId" && c.Values is not null) clientQuery.UserId = c.Values.Cast<string>();
            if(c.Column == "RoleId" && c.Values is not null) clientQuery.RoleId = c.Values.Cast<string>();
        }        

        return clientQuery;
    }
    
    public override UserRoleView GetById(string id, int maxDepth = 2)
    {
        var query = Db.Set<UserRole>()
            .Select(x => new UserRoleView { 
                Id = x.Id,
                  UserId = x.UserId,
                  RoleId = x.RoleId,
                  User = new UserView { Id = x.User!.Id,
                      Name = x.User!.Name,
                      Email = x.User!.Email,
                      LoginId = x.User!.LoginId },
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override UserRoleView Create(UserRoleCreate entity)
    {
        var dbSet = Db.Set<UserRole>();
        var dbEntity = new UserRole {
            Id = new Guid().ToString(),
            UserId = entity.UserId, RoleId = entity.RoleId
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new UserRoleView { 
                Id = x.Id,
                  UserId = x.UserId,
                  RoleId = x.RoleId,
                  User = new UserView { Id = x.User!.Id,
                      Name = x.User!.Name,
                      Email = x.User!.Email,
                      LoginId = x.User!.LoginId },
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override UserRoleView Update(string id, UserRoleUpdate entity)
    {
        var dbSet = Db.Set<UserRole>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.UserId = entity.UserId;
        existing.RoleId = entity.RoleId;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override UserRoleView Modify(string id, JsonElement entity)
    {
        var dbSet = Db.Set<UserRole>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "user_id") existing.UserId = prop.Value.GetString()!;
            else if (propName == "role_id") existing.RoleId = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override UserRoleView Delete(string id)
    {
        var dbSet = Db.Set<UserRole>();
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

    public override QueryResult<ClientQuery, UserRoleView> GetAll(int maxDepth = 2)
    {
        return GetAll(new UserRoleQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, UserRoleView> GetAll(UserRoleQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<UserRole>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                if (c.Column == "UserId" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.UserId != null && x.UserId.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "RoleId" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.RoleId != null && x.RoleId.ToLower().Contains(v.ToLower()));
                }                   
            }
        }

        IOrderedQueryable<UserRole>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "UserName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.User!.Name) : sortedQ.ThenBy(x => x.User!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.User!.Name) : sortedQ.ThenByDescending(x => x.User!.Name);
                }
                


                if (s.Column == "RoleName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Role!.Name) : sortedQ.ThenBy(x => x.Role!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Role!.Name) : sortedQ.ThenByDescending(x => x.Role!.Name);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new UserRoleView { Id = x.Id,
                  UserId = x.UserId,
                  RoleId = x.RoleId,
                  User = new UserView { Id = x.User!.Id,
                      Name = x.User!.Name,
                      Email = x.User!.Email,
                      LoginId = x.User!.LoginId },
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name } })
            .ToList();

        var result = new QueryResult<ClientQuery, UserRoleView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

