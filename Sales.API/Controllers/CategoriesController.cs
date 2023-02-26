using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase

    {
        private readonly DataContext _context;
        public CategoriesController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Categories.ToListAsync());

        }



        [HttpPost]
        public async Task<ActionResult> PostAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        } 

    }
}
