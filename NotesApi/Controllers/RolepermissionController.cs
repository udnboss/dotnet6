using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RolePermissionsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly MyContext _context;
        private RolePermissionBusiness _business;

        public RolePermissionController(MyContext context)
        {
            _context = context;
            _business = new RolePermissionBusiness(_context);
        }

        // GET: api/RolePermission
        [HttpGet]
        public ActionResult<QueryResult<ClientQuery, RolePermissionView>> GetRolePermissions([FromQuery] RolePermissionQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/RolePermission/5
        [HttpGet("{id}")]
        public ActionResult<RolePermissionView> GetRolePermission(Guid id)
        {
            var rolePermission = _business.GetById(id);

            if (rolePermission == null)
            {
                return NotFound();
            }

            return rolePermission;
        }

        // PUT: api/RolePermission/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<RolePermissionView> PutRolePermission(Guid id, RolePermissionUpdate rolePermission)
        {
            try 
            {
                var existingRolePermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, rolePermission);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolePermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/RolePermission/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public ActionResult<RolePermissionView> PatchRolePermission(Guid id, JsonElement rolePermission)
        {
            try 
            {
                var existingRolePermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, rolePermission);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolePermissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/RolePermission
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<RolePermissionView> PostRolePermission(RolePermissionCreate rolePermission)
        {
            var created = _business.Create(rolePermission);

            return created;
        }

        // DELETE: api/RolePermission/5
        [HttpDelete("{id}")]
        public ActionResult<RolePermissionView> DeleteRolePermission(Guid id)
        {
            try 
            {
                var existingRolePermission = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool RolePermissionExists(Guid id)
        {
            return _context.Set<RolePermission>().Any(e => e.Id == id);
        }
    }
}
