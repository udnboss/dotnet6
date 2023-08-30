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
            
            if(c.Column == "RoleId") clientQuery.RoleId = c.Value as Guid?;
            
            if(c.Column == "PermissionId") clientQuery.PermissionId = c.Value as Guid?;
            
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

        var inputProps = typeof(RolePermissionUpdate).GetProperties();
        var outputProps = typeof(RolePermission).GetProperties();

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

    public override RolePermissionView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<RolePermission>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }
      
        var validProps = typeof(RolePermissionModify).GetProperties();
        var outputProps = typeof(RolePermission).GetProperties();

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

