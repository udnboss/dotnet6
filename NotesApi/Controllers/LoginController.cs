using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly MyContext _context;
        private LoginBusiness _business;

        public LoginController(MyContext context)
        {
            _context = context;
            _business = new LoginBusiness(_context);
        }

        // GET: api/Login
        [HttpGet]
        [RequiredPermissions(AppPermission.LoginRead)]
        public ActionResult<QueryResult<ClientQuery, LoginView>> GetLogins([FromQuery] LoginQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.LoginRead)]
        public ActionResult<LoginView> GetLogin(string id)
        {
            var login = _business.GetById(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.LoginUpdate)]
        public ActionResult<LoginView> PutLogin(string id, LoginUpdate login)
        {
            try 
            {
                var existingLogin = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, login);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.LoginUpdate)]
        public ActionResult<LoginView> PatchLogin(string id, JsonElement login)
        {
            try 
            {
                var existingLogin = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, login);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.LoginCreate)]
        public ActionResult<LoginView> PostLogin(LoginCreate login)
        {
            var created = _business.Create(login);

            return created;
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.LoginDelete)]
        public ActionResult<LoginView> DeleteLogin(string id)
        {
            try 
            {
                var existingLogin = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool LoginExists(string id)
        {
            return _context.Set<Login>().Any(e => e.Id == id);
        }
    }
}
