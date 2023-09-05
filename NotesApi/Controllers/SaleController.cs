using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly MyContext _context;
        private SaleBusiness _business;

        public SaleController(MyContext context)
        {
            _context = context;
            _business = new SaleBusiness(_context);
        }

        // GET: api/Sale
        [HttpGet]
        [RequiredPermissions(AppPermission.SaleRead)]
        public ActionResult<QueryResult<ClientQuery, SaleView>> GetSales([FromQuery] SaleQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Sale/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.SaleRead)]
        public ActionResult<SaleView> GetSale(string id)
        {
            var sale = _business.GetById(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sale/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.SaleUpdate)]
        public ActionResult<SaleView> PutSale(string id, SaleUpdate sale)
        {
            try 
            {
                var existingSale = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, sale);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Sale/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.SaleUpdate)]
        public ActionResult<SaleView> PatchSale(string id, JsonElement sale)
        {
            try 
            {
                var existingSale = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, sale);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Sale
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.SaleCreate)]
        public ActionResult<SaleView> PostSale(SaleCreate sale)
        {
            var created = _business.Create(sale);

            return created;
        }

        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.SaleDelete)]
        public ActionResult<SaleView> DeleteSale(string id)
        {
            try 
            {
                var existingSale = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool SaleExists(string id)
        {
            return _context.Set<Sale>().Any(e => e.Id == id);
        }
    }
}
