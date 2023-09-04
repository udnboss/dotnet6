using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly MyContext _context;
        private UserBusiness _business;

        public UserController(MyContext context)
        {
            _context = context;
            _business = new UserBusiness(_context);
        }

        // GET: api/User
        [HttpGet]
        [RequiredPermissions(AppPermission.UserRead)]
        public ActionResult<QueryResult<ClientQuery, UserView>> GetUsers([FromQuery] UserQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.UserRead)]
        public ActionResult<UserView> GetUser(Guid id)
        {
            var user = _business.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.UserUpdate)]
        public ActionResult<UserView> PutUser(Guid id, UserUpdate user)
        {
            try 
            {
                var existingUser = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, user);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.UserUpdate)]
        public ActionResult<UserView> PatchUser(Guid id, JsonElement user)
        {
            try 
            {
                var existingUser = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, user);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.UserCreate)]
        public ActionResult<UserView> PostUser(UserCreate user)
        {
            var created = _business.Create(user);

            return created;
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.UserDelete)]
        public ActionResult<UserView> DeleteUser(Guid id)
        {
            try 
            {
                var existingUser = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool UserExists(Guid id)
        {
            return _context.Set<User>().Any(e => e.Id == id);
        }
    }
}
