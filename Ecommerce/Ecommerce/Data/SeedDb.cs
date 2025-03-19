using Ecommerce.Data.Entities;
using Ecommerce.Enums;
using Ecommerce.Helpers;
using Microsoft.EntityFrameworkCore;

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
            await CheckBrandsAsync();
            await CheckRolesAsync();
            await CheckProductsAsync();
            await CheckUserAsync("44134", "Facundo", "Banegaz", "facu@yopmail.com", "567 488 1156", "Calle Siempre Viva", "admin.png", UserType.Admin);
            await CheckUserAsync("33699", "Francisco", "Banegaz", "fran@yopmail.com", "154 344 6691", "Calle 25 de Mayo", "user.webp", UserType.User);

        }


        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Xtrenght - Bcaa Pro En Cápsulas Nutrition De 200g", 140000M, 2, "Ena", new List<string>() { "Amino / BCAA" }, new List<string>() { "aminoacidos_bcaa.webp" });
                await AddProductAsync("Xtrenght - Best Whey de 907g Proteina con creatina", 260000M, 17, "HTN", new List<string>() { "Proteínas" }, new List<string>() { "proteinabest.webp" });
                await AddProductAsync("Xtrenght - Creatina 250grs Micronizada", 150000M, 9, "Ena", new List<string>() { "Creatina" }, new List<string>() { "creatina_monohidratada.webp" });
                await AddProductAsync("Cellucor - C4 Pre Workout X 60 Servicios", 220000M, 7, "Ena", new List<string>() { "Pre Entreno" }, new List<string>() { "pre_entreno_c4.webp" });
                await AddProductAsync("Star nutrition - Colageno Hydrolizado X210 Gr. Suplemento", 180000M, 77, "Ena", new List<string>() { "Colágeno" }, new List<string>() { "colageno_hidrolizado.webp" });
                await AddProductAsync("Star Nutri - Iron Pack Multivitamin Powder - 44 Serv - 383 Gr - Sabor Fruit Punch", 140000M, 55, "Ena", new List<string>() { "Vitaminas" }, new List<string>() { "multivitaminas.webp" });

                await AddProductAsync("Nutremax - Hydromax Sport Drink Bebida Deportiva Isotonica En Pote 1,5 Kg", 50000M, 5, "Ena", new List<string>() { "Bebidas Hidratantes" }, new List<string>() { "bebida_isotonica.webp" });
                await AddProductAsync("Star nutrition - Oxido Nítrico Steam N.O Arginina 312 Grs", 210000M, 42, "Ena", new List<string>() { "Óxido Nítrico" }, new List<string>() { "oxido_nitroso.webp" });
                await AddProductAsync("Star nutrition - Glutamina Micronizada X 300grs", 130000M, 4, "Ena", new List<string>() { "Glutamina" }, new List<string>() { "startNutrition.webp" });
                await AddProductAsync("Combo Advance Whey + Creatina Xtrenght 250gr", 300000M, 1, "Ena", new List<string>() { "Combos" }, new List<string>() { "combo_suplementos.webp" });
                await AddProductAsync("Xtrenght - Nitrogain 1.5kg Ganador De Peso", 400000M, 111, "Ena", new List<string>() { "Ganadores de Peso" }, new List<string>() { "proteinabest.webp" });
                await AddProductAsync("Que lo Paleo Caja de Barritas x 24 Unidades", 20000M, 11, "Ena", new List<string>() { "Barritas Proteicas" }, new List<string>() { "barritas_proteicas_x.webp" });
                await AddProductAsync("Nutrex Research Series Lipo-6 Black Ultra Concentrate en pote con 60 cápsulas sin sabor", 250000M, 22, "", new List<string>() { "Quemadores de Grasa" }, new List<string>() { "quemadores.webp" });
                await AddProductAsync("Star Nutrition - 2x1 - V8", 150000M, 12, "HTN", new List<string>() { "Energizantes" }, new List<string>() { "energizante.webp" });
            }



            await _context.SaveChangesAsync();
        }




        private async Task AddProductAsync(string name, decimal price, int stock, string brandName, List<string> categories, List<string> images)
        {
            // 🔎 Buscar la marca en la base de datos
            Brand? brand = await _context.Brands.FirstOrDefaultAsync(b => b.Name == brandName);

            // 🚨 Validar si la marca no existe
            if (brand == null)
            {
                Console.WriteLine($"⚠️ Advertencia: La marca '{brandName}' no existe en la base de datos.");
                return; // 🔴 Evita agregar el producto si la marca no existe
            }


            Product prodcut = new()
            {
                TitleDescription = name,
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                Estate = stock > 0,
                IsPromoted = true,
                IsFeatured = true,
                Brand = brand,
                DiscountPercentage = 0,
                Ratings = new List<Rating>(),
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
                _context.Categories.Add(new Category { Name = "Amino / BCAA" });
                _context.Categories.Add(new Category { Name = "Proteínas" });
                _context.Categories.Add(new Category { Name = "Creatina" });
                _context.Categories.Add(new Category { Name = "Pre Entreno" });
                _context.Categories.Add(new Category { Name = "Colágeno" });
                _context.Categories.Add(new Category { Name = "Vitaminas" });
                _context.Categories.Add(new Category { Name = "Bebidas Hidratantes" });
                _context.Categories.Add(new Category { Name = "Óxido Nítrico" });
                _context.Categories.Add(new Category { Name = "Glutamina" });
                _context.Categories.Add(new Category { Name = "Combos" });
                _context.Categories.Add(new Category { Name = "Ganadores de Peso" });
                _context.Categories.Add(new Category { Name = "Barritas Proteicas" });
                _context.Categories.Add(new Category { Name = "Quemadores de Grasa" });
                _context.Categories.Add(new Category { Name = "Energizantes" });


            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckBrandsAsync()
        {
            if (!_context.Brands.Any())
            {
                // Lista de marcas con sus respectivas imágenes
                var brandsData = new List<(string Name, string Image)>
        {
            ("Universal Nutrition", "universal.webp"),
            ("HTN", "htn.webp"),
            ("Nutrex Research", "nutreReserch.webp"),
            ("Ena", "ena.webp"),
            ("Gold Nutrition Sport", "goldNutrition.webp"),
            ("Hoch Sport", "HochSport.webp"),
            ("Muscletech", "Muscletech.webp"),
            ("Que Lo Paleó", "queLoPaleo.webp"),
            ("Star Nutrition", "starNutrition.webp"),
            ("BSN", "bsn.webp"),
            ("HardCore Nutrition", "HardcoreNutrition.png"),
            ("ADN Nutrition", "adn.webp"),
            ("Spx Nutrition Max", "logo-spx.png")
        };

                List<Brand> brands = new();

                foreach (var (name, image) in brandsData)
                {
                    // Verificar si la marca ya existe en la base de datos
                    if (await _context.Brands.AnyAsync(b => b.Name == name))
                    {
                        continue; // Si la marca ya existe, la omitimos
                    }

                    Guid imageId = Guid.Empty; // Por defecto, si falla la subida de imagen

                    try
                    {
                        imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\brands\\{image}", "brands");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al subir la imagen {image}: {ex.Message}");
                    }

                    brands.Add(new Brand
                    {
                        Name = name,
                        ImageId = imageId // Se almacena el ID de la imagen subida o Guid.Empty si falló
                    });
                }

                if (brands.Any()) // Solo guardar si hay marcas nuevas
                {
                    _context.Brands.AddRange(brands);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States = new List<State>()
            {
                new State()
                {
                    Name = "Buenos Aires",
                    Cities = new List<City>()
                    {
                        new City() { Name = "La Plata" },
                        new City() { Name = "Mar del Plata" },
                        new City() { Name = "Bahía Blanca" },
                        new City() { Name = "Tigre" },
                        new City() { Name = "San Nicolás" },
                        new City() { Name = "Pergamino" },
                        new City() { Name = "Morón" },
                    }
                },
                new State()
                {
                    Name = "CABA",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Ciudad Autónoma de Buenos Aires" }
                    }
                },
                new State()
                {
                    Name = "Córdoba",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Córdoba" },
                        new City() { Name = "Villa María" },
                        new City() { Name = "Río Cuarto" },
                        new City() { Name = "Carlos Paz" },
                        new City() { Name = "Alta Gracia" },
                        new City() { Name = "Cruz del Eje" },
                    }
                },
                new State()
                {
                    Name = "Santa Fe",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Rosario" },
                        new City() { Name = "Santa Fe" },
                        new City() { Name = "Rafaela" },
                        new City() { Name = "Venado Tuerto" },
                        new City() { Name = "Reconquista" },
                        new City() { Name = "San Lorenzo" },
                    }
                },
                new State()
                {
                    Name = "Mendoza",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Mendoza" },
                        new City() { Name = "San Rafael" },
                        new City() { Name = "Godoy Cruz" },
                        new City() { Name = "Maipú" },
                        new City() { Name = "Tunuyán" },
                    }
                },
                new State()
                {
                    Name = "Tucumán",
                    Cities = new List<City>()
                    {
                        new City() { Name = "San Miguel de Tucumán" },
                        new City() { Name = "Tafí Viejo" },
                        new City() { Name = "Concepción" },
                        new City() { Name = "Yerba Buena" },
                    }
                },
                new State()
                {
                    Name = "Salta",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Salta" },
                        new City() { Name = "San José de Metán" },
                        new City() { Name = "Rosario de la Frontera" },
                        new City() { Name = "Cafayate" },
                    }
                },
                new State()
                {
                    Name = "Chaco",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Resistencia" },
                        new City() { Name = "Sáenz Peña" },
                        new City() { Name = "Villa Ángela" },
                        new City() { Name = "Charata" },
                    }
                },
                new State()
                {
                    Name = "Corrientes",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Corrientes" },
                        new City() { Name = "Goya" },
                        new City() { Name = "Mercedes" },
                        new City() { Name = "Esquina" },
                    }
                },
                new State()
                {
                    Name = "Misiones",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Posadas" },
                        new City() { Name = "Oberá" },
                        new City() { Name = "Eldorado" },
                        new City() { Name = "Apóstoles" },
                    }
                },
                new State()
                {
                    Name = "Neuquén",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Neuquén" },
                        new City() { Name = "Plottier" },
                        new City() { Name = "Zapala" },
                        new City() { Name = "Chos Malal" },
                    }
                },
                new State()
                {
                    Name = "Río Negro",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Viedma" },
                        new City() { Name = "Cipolletti" },
                        new City() { Name = "General Roca" },
                        new City() { Name = "Catriel" },
                    }
                },
                new State()
                {
                    Name = "San Juan",
                    Cities = new List<City>()
                    {
                        new City() { Name = "San Juan" },
                        new City() { Name = "Rivadavia" },
                        new City() { Name = "Chimbas" },
                        new City() { Name = "Rawson" },
                    }
                },
                new State()
                {
                    Name = "San Luis",
                    Cities = new List<City>()
                    {
                        new City() { Name = "San Luis" },
                        new City() { Name = "Villa Mercedes" },
                        new City() { Name = "Merlo" },
                        new City() { Name = "La Punta" },
                    }
                },
                new State()
                {
                    Name = "La Pampa",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Santa Rosa" },
                        new City() { Name = "General Pico" },
                        new City() { Name = "Toay" },
                        new City() { Name = "Realicó" },
                    }
                },
                new State()
                {
                    Name = "La Rioja",
                    Cities = new List<City>()
                    {
                        new City() { Name = "La Rioja" },
                        new City() { Name = "Chilecito" },
                        new City() { Name = "Villa Union" },
                        new City() { Name = "Famatina" },
                    }
                },
                new State()
                {
                    Name = "Santiago del Estero",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Santiago del Estero" },
                        new City() { Name = "Termas de Río Hondo" },
                        new City() { Name = "Añatuya" },
                        new City() { Name = "Frías" },
                    }
                },
                new State()
                {
                    Name = "Formosa",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Formosa" },
                        new City() { Name = "Pirané" },
                        new City() { Name = "Clorinda" },
                        new City() { Name = "Herradura" },
                    }
                },
                new State()
                {
                    Name = "Chubut",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Rawson" },
                        new City() { Name = "Trelew" },
                        new City() { Name = "Comodoro Rivadavia" },
                        new City() { Name = "Puerto Madryn" },
                    }
                },
                new State()
                {
                    Name = "Santa Cruz",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Río Gallegos" },
                        new City() { Name = "El Calafate" },
                        new City() { Name = "Puerto Deseado" },
                    }
                },
                new State()
                {
                    Name = "Tierra del Fuego",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Ushuaia" },
                        new City() { Name = "Río Grande" },
                    }
                },
                new State()
                {
                    Name = "Jujuy",
                    Cities = new List<City>()
                    {
                        new City() { Name = "San Salvador de Jujuy" },
                        new City() { Name = "Palpalá" },
                        new City() { Name = "Perico" },
                        new City() { Name = "La Quiaca" },
                    }
                },
                new State()
                {
                    Name = "Catamarca",
                    Cities = new List<City>()
                    {
                        new City() { Name = "San Fernando del Valle de Catamarca" },
                        new City() { Name = "Chumbicha" },
                        new City() { Name = "Belén" },
                        new City() { Name = "Andalgalá" },
                    }
                },

            }
                });

                await _context.SaveChangesAsync();
            }
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
                    ImageId = imageId,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                if (userType == UserType.Admin)
                {

                    var product = await _context.Products.FirstOrDefaultAsync();

                    if (product != null)
                    {

                        var rating = new Rating
                        {
                            Value = RatingValue.Excellent,
                            Comment = "Excelente producto",
                            Product = product,
                            User = user,
                            Date = DateTime.UtcNow
                        };

                        user.Ratings.Add(rating);
                    }
                    else
                    {

                        Console.WriteLine("Producto no encontrado");
                    }
                }
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
