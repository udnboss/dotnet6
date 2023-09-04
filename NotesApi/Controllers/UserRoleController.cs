using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UserRolesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        private readonly MyContext _context;
        private UserRoleBusiness _business;

        public UserRoleController(MyContext context)
        {
            _context = context;
            _business = new UserRoleBusiness(_context);
        }

        // GET: api/UserRole
        [HttpGet]
        [RequiredPermissions(AppPermission.UserRoleRead)]
        public ActionResult<QueryResult<ClientQuery, UserRoleView>> GetUserRoles([FromQuery] UserRoleQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/UserRole/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.UserRoleRead)]
        public ActionResult<UserRoleView> GetUserRole(Guid id)
        {
            var userRole = _business.GetById(id);

            if (userRole == null)
            {
                return NotFound();
            }

            return userRole;
        }

        // PUT: api/UserRole/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.UserRoleUpdate)]
        public ActionResult<UserRoleView> PutUserRole(Guid id, UserRoleUpdate userRole)
        {
            try 
            {
                var existingUserRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, userRole);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/UserRole/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.UserRoleUpdate)]
        public ActionResult<UserRoleView> PatchUserRole(Guid id, JsonElement userRole)
        {
            try 
            {
                var existingUserRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, userRole);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/UserRole
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.UserRoleCreate)]
        public ActionResult<UserRoleView> PostUserRole(UserRoleCreate userRole)
        {
            var created = _business.Create(userRole);

            return created;
        }

        // DELETE: api/UserRole/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.UserRoleDelete)]
        public ActionResult<UserRoleView> DeleteUserRole(Guid id)
        {
            try 
            {
                var existingUserRole = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool UserRoleExists(Guid id)
        {
            return _context.Set<UserRole>().Any(e => e.Id == id);
        }
    }
}
