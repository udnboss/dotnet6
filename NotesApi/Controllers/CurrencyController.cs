using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly MyContext _context;
        private CurrencyBusiness _business;

        public CurrencyController(MyContext context)
        {
            _context = context;
            _business = new CurrencyBusiness(_context);
        }

        // GET: api/Currency
        [HttpGet]
        [RequiredPermissions(AppPermission.CurrencyRead)]
        public ActionResult<QueryResult<ClientQuery, CurrencyView>> GetCurrencys([FromQuery] CurrencyQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Currency/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.CurrencyRead)]
        public ActionResult<CurrencyView> GetCurrency(string id)
        {
            var currency = _business.GetById(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.CurrencyUpdate)]
        public ActionResult<CurrencyView> PutCurrency(string id, CurrencyUpdate currency)
        {
            try 
            {
                var existingCurrency = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, currency);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Currency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.CurrencyUpdate)]
        public ActionResult<CurrencyView> PatchCurrency(string id, JsonElement currency)
        {
            try 
            {
                var existingCurrency = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, currency);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Currency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.CurrencyCreate)]
        public ActionResult<CurrencyView> PostCurrency(CurrencyCreate currency)
        {
            var created = _business.Create(currency);

            return created;
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.CurrencyDelete)]
        public ActionResult<CurrencyView> DeleteCurrency(string id)
        {
            try 
            {
                var existingCurrency = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool CurrencyExists(string id)
        {
            return _context.Set<Currency>().Any(e => e.Id == id);
        }
    }
}
