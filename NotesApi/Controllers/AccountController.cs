using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly MyContext _context;
        private AccountBusiness _business;

        public AccountController(MyContext context)
        {
            _context = context;
            _business = new AccountBusiness(_context);
        }

        // GET: api/Account
        [HttpGet]
        [RequiredPermissions(AppPermission.AccountRead)]
        public ActionResult<QueryResult<ClientQuery, AccountView>> GetAccounts([FromQuery] AccountQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.AccountRead)]
        public ActionResult<AccountView> GetAccount(string id)
        {
            var account = _business.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.AccountUpdate)]
        public ActionResult<AccountView> PutAccount(string id, AccountUpdate account)
        {
            try 
            {
                var existingAccount = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, account);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.AccountUpdate)]
        public ActionResult<AccountView> PatchAccount(string id, JsonElement account)
        {
            try 
            {
                var existingAccount = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, account);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.AccountCreate)]
        public ActionResult<AccountView> PostAccount(AccountCreate account)
        {
            var created = _business.Create(account);

            return created;
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.AccountDelete)]
        public ActionResult<AccountView> DeleteAccount(string id)
        {
            try 
            {
                var existingAccount = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool AccountExists(string id)
        {
            return _context.Set<Account>().Any(e => e.Id == id);
        }
    }
}
