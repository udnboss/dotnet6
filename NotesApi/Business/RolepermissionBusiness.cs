using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

public class RolePermissionBusiness : Business<RolePermission, RolePermissionView, RolePermissionUpdate, RolePermissionModify, RolePermissionCreate, RolePermissionQuery>
{
    public RolePermissionBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(RolePermissionQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "RoleId", _operator: Operators.IsIn, value: query.RoleId));
            
            dataQuery.Where.Add(new Condition(column: "PermissionId", _operator: Operators.IsIn, value: query.PermissionId));
            

        return dataQuery;
    }

    public override RolePermissionQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "RoleId" && c.Values is not null) clientQuery.RoleId = c.Values.Cast<Guid?>();
            if(c.Column == "PermissionId" && c.Values is not null) clientQuery.PermissionId = c.Values.Cast<Guid?>();
        }        

        return clientQuery;
    }
    
    public override RolePermissionView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<RolePermission>()
            .Select(x => new RolePermissionView { 
                Id = x.Id,
                  RoleId = x.RoleId,
                  PermissionId = x.PermissionId,
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name },
                  Permission = new PermissionView { Id = x.Permission!.Id,
                      Code = x.Permission!.Code,
                      Name = x.Permission!.Name,
                      Entity = x.Permission!.Entity,
                      Action = x.Permission!.Action }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override RolePermissionView Create(RolePermissionCreate entity)
    {
        var dbSet = Db.Set<RolePermission>();
        var dbEntity = new RolePermission {
            Id = new Guid(),
            RoleId = entity.RoleId, PermissionId = entity.PermissionId
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new RolePermissionView { 
                Id = x.Id,
                  RoleId = x.RoleId,
                  PermissionId = x.PermissionId,
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name },
                  Permission = new PermissionView { Id = x.Permission!.Id,
                      Code = x.Permission!.Code,
                      Name = x.Permission!.Name,
                      Entity = x.Permission!.Entity,
                      Action = x.Permission!.Action }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override RolePermissionView Update(Guid id, RolePermissionUpdate entity)
    {
        var dbSet = Db.Set<RolePermission>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.RoleId = entity.RoleId;
        existing.PermissionId = entity.PermissionId;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override RolePermissionView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<RolePermission>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "role_id") existing.RoleId = prop.Value.GetGuid()!;
            else if (propName == "permission_id") existing.PermissionId = prop.Value.GetGuid()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override RolePermissionView Delete(Guid id)
    {
        var dbSet = Db.Set<RolePermission>();
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

    public override QueryResult<ClientQuery, RolePermissionView> GetAll(RolePermissionQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<RolePermission>().Skip(query.Offset);
                           
        if ( query.Limit > 0) 
        {
            q = q.Take(query.Limit);
        }    

        if ( query.Where.Count > 0 )
        {
            foreach (var c in query.Where)
            {   
                
                    if (c.Column == "RoleId" && c.Operator == Operators.IsIn && c.Values != null) 
                    {
                        var v = c.Values.Cast<Guid?>().ToList();
                        q = q.Where(x => x.RoleId != null && v.Contains(x.RoleId));
                    }


                    if (c.Column == "PermissionId" && c.Operator == Operators.IsIn && c.Values != null) 
                    {
                        var v = c.Values.Cast<Guid?>().ToList();
                        q = q.Where(x => x.PermissionId != null && v.Contains(x.PermissionId));
                    }                   
            }
        }

        IOrderedQueryable<RolePermission>? sortedQ = null;
        if (query.Sort.Count > 0)
        {
            foreach (var s in query.Sort)
            {
                
                if (s.Column == "RoleName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Role!.Name) : sortedQ.ThenBy(x => x.Role!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Role!.Name) : sortedQ.ThenByDescending(x => x.Role!.Name);
                }
                


                if (s.Column == "PermissionName")
                {
                    sortedQ = s.Direction == SortDirection.Asc ? 
                        sortedQ is null ? q.OrderBy(x => x.Permission!.Name) : sortedQ.ThenBy(x => x.Permission!.Name) 
                        : sortedQ is null ? q.OrderByDescending( x => x.Permission!.Name) : sortedQ.ThenByDescending(x => x.Permission!.Name);
                }
                
            }
        }
        
        var data = (sortedQ ?? q)
            .Select(x => new RolePermissionView { Id = x.Id,
                  RoleId = x.RoleId,
                  PermissionId = x.PermissionId,
                  Role = new RoleView { Id = x.Role!.Id,
                      Code = x.Role!.Code,
                      Name = x.Role!.Name },
                  Permission = new PermissionView { Id = x.Permission!.Id,
                      Code = x.Permission!.Code,
                      Name = x.Permission!.Name,
                      Entity = x.Permission!.Entity,
                      Action = x.Permission!.Action } })
            .ToList();

        var result = new QueryResult<ClientQuery, RolePermissionView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

