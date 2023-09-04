using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PermissionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly MyContext _context;
        private PermissionBusiness _business;

        public PermissionController(MyContext context)
        {
            _context = context;
            _business = new PermissionBusiness(_context);
        }

        // GET: api/Permission
        [HttpGet]
        [RequiredPermissions(AppPermission.PermissionRead)]
        public ActionResult<QueryResult<ClientQuery, PermissionView>> GetPermissions([FromQuery] PermissionQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Permission/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.PermissionRead)]
        public ActionResult<PermissionView> GetPermission(Guid id)
        {
            var permission = _business.GetById(id);

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }

        // PUT: api/Permission/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.PermissionUpdate)]
        public ActionResult<PermissionView> PutPermission(Guid id, PermissionUpdate permission)
        {
            try 
            {
                var existingPermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, permission);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Permission/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.PermissionUpdate)]
        public ActionResult<PermissionView> PatchPermission(Guid id, JsonElement permission)
        {
            try 
            {
                var existingPermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, permission);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Permission
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.PermissionCreate)]
        public ActionResult<PermissionView> PostPermission(PermissionCreate permission)
        {
            var created = _business.Create(permission);

            return created;
        }

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.PermissionDelete)]
        public ActionResult<PermissionView> DeletePermission(Guid id)
        {
            try 
            {
                var existingPermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool PermissionExists(Guid id)
        {
            return _context.Set<Permission>().Any(e => e.Id == id);
        }
    }
}
