using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

public class RoleBusiness : Business<Role, RoleView, RoleUpdate, RoleModify, RoleCreate, RoleQuery>
{
    public RoleBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(RoleQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            
            dataQuery.Where.Add(new Condition(column: "Code", _operator: Operators.Contains, value: query.Code));
            

        return dataQuery;
    }

    public override RoleQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
            if(c.Column == "Code") clientQuery.Code = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override RoleView GetById(Guid id, int maxDepth = 2)
    {
        var query = Db.Set<Role>()
            .Select(x => new RoleView { 
                Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  RolePermissions = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, RoleId = new List<Guid?>() { x.Id } }) { Result = x.RolePermissions!.Select(y1 => new RolePermissionView { Id = y1.Id,
                      RoleId = y1.RoleId,
                      PermissionId = y1.PermissionId }).Take(10) }  
            })
            .AsQueryable();

        if (maxDepth > 0)
        {
            maxDepth--;
        }

        var entity = query.FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"No {entityName} entity found for given {id}");
        return entity;
    }

    public override RoleView Create(RoleCreate entity)
    {
        var dbSet = Db.Set<Role>();
        var dbEntity = new Role {
            Id = new Guid(),
            Code = entity.Code, Name = entity.Name
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new RoleView { 
                Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  RolePermissions = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, RoleId = new List<Guid?>() { x.Id } }) { Result = x.RolePermissions!.Select(y1 => new RolePermissionView { Id = y1.Id,
                      RoleId = y1.RoleId,
                      PermissionId = y1.PermissionId }).Take(10) }
            })
            .FirstOrDefault(x => x.Id == dbEntity.Id);
        
        if (added is null)
        {
            throw new Exception($"Could not retrieve created {entityName} entity.");
        }
        return added;
    }

    public override RoleView Update(Guid id, RoleUpdate entity)
    {
        var dbSet = Db.Set<Role>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Code = entity.Code;
        existing.Name = entity.Name;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override RoleView Modify(Guid id, JsonElement entity)
    {
        var dbSet = Db.Set<Role>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        foreach (JsonProperty prop in entity.EnumerateObject())
        {
            var propName = prop.Name.ToLower();
            if (propName == "id") continue;
            else if (propName == "code") existing.Code = prop.Value.GetString()!;
            else if (propName == "name") existing.Name = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override RoleView Delete(Guid id)
    {
        var dbSet = Db.Set<Role>();
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

    public override QueryResult<ClientQuery, RoleView> GetAll(RoleQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Role>().Skip(query.Offset);
                           
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


                    if (c.Column == "Code" && c.Operator == Operators.Contains && c.Value != null) 
                    {
                        var v = c.Value.ToString();
                        if(!string.IsNullOrWhiteSpace(v))
                            q = q.Where(x => x.Code != null && x.Code.ToLower().Contains(v.ToLower()));
                    }                   
            }
        }

        IOrderedQueryable<Role>? sortedQ = null;
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
            .Select(x => new RoleView { Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  RolePermissions = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, RoleId = new List<Guid?>() { x.Id } }) { Result = x.RolePermissions!.Select(y1 => new RolePermissionView { Id = y1.Id,
                      RoleId = y1.RoleId,
                      PermissionId = y1.PermissionId }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, RoleView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

