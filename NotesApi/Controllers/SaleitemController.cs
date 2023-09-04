using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SaleItemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleItemController : ControllerBase
    {
        private readonly MyContext _context;
        private SaleItemBusiness _business;

        public SaleItemController(MyContext context)
        {
            _context = context;
            _business = new SaleItemBusiness(_context);
        }

        // GET: api/SaleItem
        [HttpGet]
        [RequiredPermissions(AppPermission.SaleItemRead)]
        public ActionResult<QueryResult<ClientQuery, SaleItemView>> GetSaleItems([FromQuery] SaleItemQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/SaleItem/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.SaleItemRead)]
        public ActionResult<SaleItemView> GetSaleItem(Guid id)
        {
            var saleItem = _business.GetById(id);

            if (saleItem == null)
            {
                return NotFound();
            }

            return saleItem;
        }

        // PUT: api/SaleItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.SaleItemUpdate)]
        public ActionResult<SaleItemView> PutSaleItem(Guid id, SaleItemUpdate saleItem)
        {
            try 
            {
                var existingSaleItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, saleItem);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/SaleItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.SaleItemUpdate)]
        public ActionResult<SaleItemView> PatchSaleItem(Guid id, JsonElement saleItem)
        {
            try 
            {
                var existingSaleItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, saleItem);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/SaleItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.SaleItemCreate)]
        public ActionResult<SaleItemView> PostSaleItem(SaleItemCreate saleItem)
        {
            var created = _business.Create(saleItem);

            return created;
        }

        // DELETE: api/SaleItem/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.SaleItemDelete)]
        public ActionResult<SaleItemView> DeleteSaleItem(Guid id)
        {
            try 
            {
                var existingSaleItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool SaleItemExists(Guid id)
        {
            return _context.Set<SaleItem>().Any(e => e.Id == id);
        }
    }
}
