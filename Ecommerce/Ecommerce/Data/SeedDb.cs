using Ecommerce.Data.Entities;
using Ecommerce.Enums;
using Ecommerce.Helpers;

namespace Ecommerce.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this._context = context;
            this._userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("44134", "Facundo", "Banegaz", "facu@yopmail.com", "567 488 1156", "Calle Siempre Viva", UserType.Admin);
            await CheckUserAsync("33699", "Francisco", "Banegaz", "fran@yopmail.com", "154 344 6691", "Calle 25 de Mayo", UserType.User);
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnología" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Samsung" });
                _context.Categories.Add(new Category { Name = "Apple" });
            }

            await _context.SaveChangesAsync();
        }
        /*
         Colombia, EEUU, Argentina, Chile,Uruguay, Brasil 
         */
        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
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
                        new City() { Name = "Usaquén" },
                        new City() { Name = "Chapinero" },
                        new City() { Name = "Santa Fe" },
                        new City() { Name = "Usme" },
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

                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Buenos Aires",
                    Cities = new List<City>() {
                        new City() { Name = "La Plata" },
                        new City() { Name = "Mar del Plata" },
                        new City() { Name = "Bahía Blanca" },
                        new City() { Name = "Tigre" },
                        new City() { Name = "San Nicolás" },
                    }
                },
                new State()
                {
                    Name = "Córdoba",
                    Cities = new List<City>() {
                        new City() { Name = "Córdoba" },
                        new City() { Name = "Villa María" },
                        new City() { Name = "Río Cuarto" },
                        new City() { Name = "Carlos Paz" },
                        new City() { Name = "Alta Gracia" },
                    }
                },
            }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Chile",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Región Metropolitana",
                    Cities = new List<City>() {
                        new City() { Name = "Santiago" },
                        new City() { Name = "Puente Alto" },
                        new City() { Name = "Maipú" },
                        new City() { Name = "Las Condes" },
                        new City() { Name = "La Florida" },
                    }
                },
                new State()
                {
                    Name = "Valparaíso",
                    Cities = new List<City>() {
                        new City() { Name = "Valparaíso" },
                        new City() { Name = "Viña del Mar" },
                        new City() { Name = "Quilpué" },
                        new City() { Name = "Villa Alemana" },
                        new City() { Name = "San Antonio" },
                    }
                },
            }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Uruguay",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Montevideo",
                    Cities = new List<City>() {
                        new City() { Name = "Montevideo" },
                        new City() { Name = "Punta Carretas" },
                        new City() { Name = "Ciudad Vieja" },
                        new City() { Name = "Pocitos" },
                        new City() { Name = "Malvín" },
                    }
                },
                new State()
                {
                    Name = "Maldonado",
                    Cities = new List<City>() {
                        new City() { Name = "Punta del Este" },
                        new City() { Name = "Maldonado" },
                        new City() { Name = "San Carlos" },
                        new City() { Name = "Piriápolis" },
                        new City() { Name = "Aiguá" },
                    }
                },
            }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Brasil",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "São Paulo",
                    Cities = new List<City>() {
                        new City() { Name = "São Paulo" },
                        new City() { Name = "Campinas" },
                        new City() { Name = "Santos" },
                        new City() { Name = "São Bernardo do Campo" },
                        new City() { Name = "Ribeirão Preto" },
                    }
                },
                new State()
                {
                    Name = "Río de Janeiro",
                    Cities = new List<City>() {
                        new City() { Name = "Río de Janeiro" },
                        new City() { Name = "Niterói" },
                        new City() { Name = "Nova Iguaçu" },
                        new City() { Name = "Petrópolis" },
                        new City() { Name = "Volta Redonda" },
                    }
                },
            }
                });
            }

            await _context.SaveChangesAsync();
        }


        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "facu1234");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

    

    }
}
