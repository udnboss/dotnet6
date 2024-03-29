using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly MyContext _context;
        private ItemBusiness _business;

        public ItemController(MyContext context)
        {
            _context = context;
            _business = new ItemBusiness(_context);
        }

        // GET: api/Item
        [HttpGet]
        [RequiredPermissions(AppPermission.ItemRead)]
        public ActionResult<QueryResult<ClientQuery, ItemView>> GetItems([FromQuery] ItemQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.ItemRead)]
        public ActionResult<ItemView> GetItem(string id)
        {
            var item = _business.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.ItemUpdate)]
        public ActionResult<ItemView> PutItem(string id, ItemUpdate item)
        {
            try 
            {
                var existingItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, item);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.ItemUpdate)]
        public ActionResult<ItemView> PatchItem(string id, JsonElement item)
        {
            try 
            {
                var existingItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, item);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.ItemCreate)]
        public ActionResult<ItemView> PostItem(ItemCreate item)
        {
            var created = _business.Create(item);

            return created;
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.ItemDelete)]
        public ActionResult<ItemView> DeleteItem(string id)
        {
            try 
            {
                var existingItem = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool ItemExists(string id)
        {
            return _context.Set<Item>().Any(e => e.Id == id);
        }
    }
}
