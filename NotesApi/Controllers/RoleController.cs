using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RolesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly MyContext _context;
        private RoleBusiness _business;

        public RoleController(MyContext context)
        {
            _context = context;
            _business = new RoleBusiness(_context);
        }

        // GET: api/Role
        [HttpGet]
        [RequiredPermissions(AppPermission.RoleRead)]
        public ActionResult<QueryResult<ClientQuery, RoleView>> GetRoles([FromQuery] RoleQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.RoleRead)]
        public ActionResult<RoleView> GetRole(string id)
        {
            var role = _business.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.RoleUpdate)]
        public ActionResult<RoleView> PutRole(string id, RoleUpdate role)
        {
            try 
            {
                var existingRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, role);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.RoleUpdate)]
        public ActionResult<RoleView> PatchRole(string id, JsonElement role)
        {
            try 
            {
                var existingRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, role);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.RoleCreate)]
        public ActionResult<RoleView> PostRole(RoleCreate role)
        {
            var created = _business.Create(role);

            return created;
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.RoleDelete)]
        public ActionResult<RoleView> DeleteRole(string id)
        {
            try 
            {
                var existingRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool RoleExists(string id)
        {
            return _context.Set<Role>().Any(e => e.Id == id);
        }
    }
}
