using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SalesContext _context;
        private Business<Account> _business;

        public AccountController(SalesContext context)
        {
            _context = context;
            _business = new Business<Account>(_context);
        }

        // GET: api/Account
        [HttpGet]
        public ActionResult<QueryResult<ClientQuery, Account>> GetAccounts()
        {

            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var result = _business.GetAll(new ClientQuery(), new DataQuery());
            return result;
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public ActionResult<Account> GetAccount(Guid id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

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
        public ActionResult<Account> PutAccount(Guid id, Account account)
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
        public ActionResult<Account> PatchAccount(Guid id, Account account)
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
        public ActionResult<Account> PostAccount(Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'AccountsContext.Accounts' is null.");
            }

            var created = _business.Create(account);

            return created;
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public ActionResult<Account> DeleteAccount(Guid id)
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

        private bool AccountExists(Guid id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
