using Microsoft.EntityFrameworkCore;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Responses;

namespace Sales.API.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)

        {
            _context = context;
            _apiService = apiService;
        }



        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            // await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {

                _context.Categories.Add(new Category { Name = "Bebidas" });
                _context.Categories.Add(new Category { Name = "Carnes" });
                _context.Categories.Add(new Category { Name = "Vino" });
                _context.Categories.Add(new Category { Name = "Mecato" });
                _context.Categories.Add(new Category { Name = "Galletas" });
                _context.Categories.Add(new Category { Name = "Cafe" });
                _context.Categories.Add(new Category { Name = "Chocolates" });
                _context.Categories.Add(new Category { Name = "Carnes frescas" });
                _context.Categories.Add(new Category { Name = "Cafe molido" });
                _context.Categories.Add(new Category { Name = "Gaseosa" });
                _context.Categories.Add(new Category { Name = "Bocadillos" });
                await _context.SaveChangesAsync();

            }
        }
    }
}
        


