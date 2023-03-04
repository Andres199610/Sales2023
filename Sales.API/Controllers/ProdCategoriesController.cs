using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/prodcategories")]
    public class ProdCategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        public ProdCategoriesController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.ProdCategories.ToListAsync());

        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var prodcategory = await _context.ProdCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (prodcategory == null)
            {
                return NotFound();

            }
            return Ok(prodcategory);

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ProdCategory prodcategory)
        {
            try
            {
                _context.Add(prodcategory);
                await _context.SaveChangesAsync();
                return Ok(prodcategory);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una categorá con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PustAsync(ProdCategory prodcategory)
        {
            try
            {
                _context.Update(prodcategory);
                await _context.SaveChangesAsync();
                return Ok(prodcategory);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una categoria con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var prodcategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (prodcategory == null)
            {
                return NotFound();

            }
            _context.Remove(prodcategory);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
