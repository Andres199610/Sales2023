using Sales.API.Services;
using Sales.Shared.Entities;

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
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any() )
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

        private async Task CheckCountriesAsync()
        {
           if(!_context.Countries.Any() )
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Antioquia",
                    Cities = new List<City>() {
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Rionegro" },
                    }
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = new List<City>() {
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Useme" },
                        new City() { Name = "Bosa" },
                    }
                },
            }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Florida",
                    Cities = new List<City>() {
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    }
                },
                new State()
                {
                    Name = "Texas",
                    Cities = new List<City>() {
                        new City() { Name = "Houston" },
                        new City() { Name = "San Antonio" },
                        new City() { Name = "Dallas" },
                        new City() { Name = "Austin" },
                        new City() { Name = "El Paso" },
                    }
                },
            }
                });

               

            }

            await _context.SaveChangesAsync();
        }

    }
}
