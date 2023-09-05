using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class PermissionBusiness : Business<Permission, PermissionView, PermissionUpdate, PermissionModify, PermissionCreate, PermissionQuery>
{
    public PermissionBusiness(DbContext db) : base(db)
    {
    }

    public override DataQuery ConvertToDataQuery(PermissionQuery query)
    {
        var dataQuery = base.ConvertToDataQuery(query);

        
            dataQuery.Where.Add(new Condition(column: "Name", _operator: Operators.Contains, value: query.Name));
            
            dataQuery.Where.Add(new Condition(column: "Code", _operator: Operators.Contains, value: query.Code));
            
            dataQuery.Where.Add(new Condition(column: "Entity", _operator: Operators.IsIn, value: query.Entity));
            
            dataQuery.Where.Add(new Condition(column: "Action", _operator: Operators.IsIn, value: query.Action));
            

        return dataQuery;
    }

    public override PermissionQuery ConvertToClientQuery(DataQuery query)
    {
        var clientQuery = base.ConvertToClientQuery(query);

        foreach(var c in query.Where)
        {
            if(c.Column == "Name") clientQuery.Name = c.Value as string;
            if(c.Column == "Code") clientQuery.Code = c.Value as string;
            if(c.Column == "Entity") clientQuery.Entity = c.Value as string;
            if(c.Column == "Action") clientQuery.Action = c.Value as string;
        }        

        return clientQuery;
    }
    
    public override PermissionView GetById(string id, int maxDepth = 2)
    {
        var query = Db.Set<Permission>()
            .Select(x => new PermissionView { 
                Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  Entity = x.Entity,
                  Action = x.Action,
                  Roles = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, PermissionId = new List<string?>() { x.Id } }) { Result = x.Roles!.Select(y1 => new RolePermissionView { Id = y1.Id,
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

    public override PermissionView Create(PermissionCreate entity)
    {
        var dbSet = Db.Set<Permission>();
        var dbEntity = new Permission {
            Id = new Guid().ToString(),
            Code = entity.Code, Name = entity.Name, Entity = entity.Entity, Action = entity.Action
        };
        dbSet.Add(dbEntity);
        Db.SaveChanges();
        var added = dbSet.Select(x => new PermissionView { 
                Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  Entity = x.Entity,
                  Action = x.Action,
                  Roles = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, PermissionId = new List<string?>() { x.Id } }) { Result = x.Roles!.Select(y1 => new RolePermissionView { Id = y1.Id,
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

    public override PermissionView Update(string id, PermissionUpdate entity)
    {
        var dbSet = Db.Set<Permission>();
        var existing = dbSet.Find(id);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Could not find an existing {entityName} entity with the given id.");
        }

        existing.Code = entity.Code;
        existing.Name = entity.Name;
        existing.Entity = entity.Entity;
        existing.Action = entity.Action;

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override PermissionView Modify(string id, JsonElement entity)
    {
        var dbSet = Db.Set<Permission>();
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
            else if (propName == "entity") existing.Entity = prop.Value.GetString()!;
            else if (propName == "action") existing.Action = prop.Value.GetString()!;
        }

        Db.SaveChanges();
        var updated = GetById(id);
        return updated;
    }

    public override PermissionView Delete(string id)
    {
        var dbSet = Db.Set<Permission>();
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

    public override QueryResult<ClientQuery, PermissionView> GetAll(int maxDepth = 2)
    {
        return GetAll(new PermissionQuery(), new DataQuery(), maxDepth);
    }

    public override QueryResult<ClientQuery, PermissionView> GetAll(PermissionQuery clientQuery, DataQuery query, int maxDepth = 2)
    {
        var q = Db.Set<Permission>().Skip(query.Offset);
                           
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
                else if (c.Column == "Code" && c.Operator == Operators.Contains && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.Code != null && x.Code.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "Entity" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.Entity != null && x.Entity.ToLower().Contains(v.ToLower()));
                }
                else if (c.Column == "Action" && c.Operator == Operators.IsIn && c.Value != null) 
                {
                    var v = c.Value.ToString();
                    if(!string.IsNullOrWhiteSpace(v))
                        q = q.Where(x => x.Action != null && x.Action.ToLower().Contains(v.ToLower()));
                }                   
            }
        }

        IOrderedQueryable<Permission>? sortedQ = null;
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
            .Select(x => new PermissionView { Id = x.Id,
                  Code = x.Code,
                  Name = x.Name,
                  Entity = x.Entity,
                  Action = x.Action,
                  Roles = new QueryResult<RolePermissionQuery, RolePermissionView>(new RolePermissionQuery() { _Size = 10, _Page = 1, PermissionId = new List<string?>() { x.Id } }) { Result = x.Roles!.Select(y1 => new RolePermissionView { Id = y1.Id,
                      RoleId = y1.RoleId,
                      PermissionId = y1.PermissionId }).Take(10) } })
            .ToList();

        var result = new QueryResult<ClientQuery, PermissionView>(clientQuery)
        {
            Count = data.Count,
            Result = data,
            Total = data.Count
        };

        return result;
    }

}

