﻿using Microsoft.EntityFrameworkCore;
using Sales.API.Helpers;
using Sales.API.Services;
using Sales.Shared.Entities;
using Sales.Shared.Enums;
using Sales.Shared.Responses;

namespace Sales.API.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)

        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
        }



        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        // await CheckCountriesAsync();
            await ChecCategoriesAsync();
        await CheckRolesAsync();
          await CheckUserAsync("1010", "Andres", "Ramirez", "ramirez@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);

        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }
        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }


        private async Task ChecCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Carne" });
                _context.Categories.Add(new Category { Name = "Pescado" });
                _context.Categories.Add(new Category { Name = "Salmon" });
                _context.Categories.Add(new Category { Name = "Cafe" });
                _context.Categories.Add(new Category { Name = "Chocolate" });
                _context.Categories.Add(new Category { Name = "Jugo" });
                _context.Categories.Add(new Category { Name = "Maiz" });
                _context.Categories.Add(new Category { Name = "Lentejas" });
                _context.Categories.Add(new Category { Name = "Frijol" });
                _context.Categories.Add(new Category { Name = "Galleta" });
                _context.Categories.Add(new Category { Name = "Avena" });
                _context.Categories.Add(new Category { Name = "Legumbre" });
                _context.Categories.Add(new Category { Name = "Cafe tostado" });
                _context.Categories.Add(new Category { Name = "Carnes frescas" });
                _context.Categories.Add(new Category { Name = "Carne refrigerada" });
                _context.Categories.Add(new Category { Name = "Nucita" });
                _context.Categories.Add(new Category { Name = "Helado" });
                _context.Categories.Add(new Category { Name = "Helado Bon" });
                _context.Categories.Add(new Category { Name = "Pastas" });
                _context.Categories.Add(new Category { Name = "Platano" });
                _context.Categories.Add(new Category { Name = "Bocadillo" });
                await _context.SaveChangesAsync();

            }
                
                


        }
        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {



                Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
            //   if (responseCountries.IsSuccess)
            //  {
                    List<CountryResponse> countries = (List<CountryResponse>)responseCountries.Result!;
                    foreach (CountryResponse countryResponse in countries)
                    {
                        Country country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
  //}
}




