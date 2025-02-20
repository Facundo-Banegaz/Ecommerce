using Ecommerce.Data.Entities;
using Ecommerce.Enums;
using Ecommerce.Helpers;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            this._context = context;
            this._userHelper = userHelper;
            this._blobHelper = blobHelper;
     
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("44134", "Facundo", "Banegaz", "facu@yopmail.com", "567 488 1156", "Calle Siempre Viva","admin.png", UserType.Admin);
            await CheckUserAsync("33699", "Francisco", "Banegaz", "fran@yopmail.com", "154 344 6691", "Calle 25 de Mayo", "user.webp",UserType.User);
            await CheckProductsAsync();
        }


        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.jpeg" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.jpeg" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.jpeg", "airpos2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.jpeg" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_bmx.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.webp" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png","ipad2.jpg" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "iphone16.png", "iphone16b.png", "iphone16c.webp", "iphone16d.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
       
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.webp" });
              
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.jpeg" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Wey Protein", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "wey-protein.webp" });
                await AddProductAsync("Creatina 250grs Micronizada", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "creatina.webp" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.webp" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.webp" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.jpeg" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
            }
        }


        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product prodcut = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? category in categories)
            {
                prodcut.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\products\\{image}", "products");
                prodcut.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(prodcut);
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
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Mascotas" });

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
            string Image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{Image}", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    ImageId = imageId ,
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
