using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CategorysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly MyContext _context;
        private CategoryBusiness _business;

        public CategoryController(MyContext context)
        {
            _context = context;
            _business = new CategoryBusiness(_context);
        }

        // GET: api/Category
        [HttpGet]
        [RequiredPermissions(AppPermission.CategoryRead)]
        public ActionResult<QueryResult<ClientQuery, CategoryView>> GetCategorys([FromQuery] CategoryQuery query)
        {
            var dataQuery = _business.ConvertToDataQuery(query);

            var result = _business.GetAll(query, dataQuery);
            return result;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [RequiredPermissions(AppPermission.CategoryRead)]
        public ActionResult<CategoryView> GetCategory(string id)
        {
            var category = _business.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [RequiredPermissions(AppPermission.CategoryUpdate)]
        public ActionResult<CategoryView> PutCategory(string id, CategoryUpdate category)
        {
            try 
            {
                var existingCategory = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Update(id, category);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // PATCH: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        [RequiredPermissions(AppPermission.CategoryUpdate)]
        public ActionResult<CategoryView> PatchCategory(string id, JsonElement category)
        {
            try 
            {
                var existingCategory = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            try
            {
                var updated = _business.Modify(id, category);
                return updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [RequiredPermissions(AppPermission.CategoryCreate)]
        public ActionResult<CategoryView> PostCategory(CategoryCreate category)
        {
            var created = _business.Create(category);

            return created;
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [RequiredPermissions(AppPermission.CategoryDelete)]
        public ActionResult<CategoryView> DeleteCategory(string id)
        {
            try 
            {
                var existingCategory = _business.GetById(id);
            } 
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            var deleted =_business.Delete(id);

            return deleted;
        }

        private bool CategoryExists(string id)
        {
            return _context.Set<Category>().Any(e => e.Id == id);
        }
    }
}
