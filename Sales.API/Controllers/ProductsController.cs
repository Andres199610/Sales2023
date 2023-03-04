using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class Products : ControllerBase
    {
        private readonly DataContext _context;
        public Products(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Products.ToListAsync());

        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _context.ProdCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();

            }
            return Ok(product);

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(Product product)
        {
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe el producto categorá con el mismo nombre.");
                }
                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> PustAsync(Product product)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe el producto  con el mismo nombre.");
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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();

            }
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
