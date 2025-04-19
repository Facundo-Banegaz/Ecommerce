using Ecommerce.Common;
using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IOrderHelper _orderHelper;
        public HomeController(ILogger<HomeController> logger, DataContext context, IUserHelper userHelper,IOrderHelper orderHelper)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
            _orderHelper = orderHelper; 
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands.ToListAsync();
        
            var featuredProducts = await _context.Products.Include(p => p.ProductImages).Where(p => p.IsFeatured).Where(s=>s.Stock > 0).ToListAsync(); 
            var promotedProducts = await _context.Products.Include(p => p.ProductImages).Where(p => p.IsPromoted).Where(s => s.Stock > 0).ToListAsync();

            var viewModel = new HomeViewModel
            {
                Brands = brands,
                FeaturedProducts = featuredProducts,
                PromotedProducts = promotedProducts
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Products()
        {
            List<Product> products = await _context.Products
                 .Include(p => p.ProductImages)
                 .Include(p => p.ProductCategories)
                 .Include(b => b.Brand)
                 .OrderBy(p => p.Name)
                 .ToListAsync();

            ProductViewModel model = new() { Products = products };
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user != null)
            {
                model.Quantity = await _context.TemporalSales
                    .Where(ts => ts.User.Id == user.Id)
                    .SumAsync(ts => ts.Quantity);
            }

            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
        //add de carito de compras
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Product product = await _context.Products.Include(b => b.Brand).FirstOrDefaultAsync(p =>p.Id== id);
            if (product == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = new()
            {
                Product = product,
                Quantity = 1,
                User = user
            };

            _context.TemporalSales.Add(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Products));
        }

        //add de Wishlist
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            // Evitar duplicados
            bool exists = await _context.Wishlist
                .AnyAsync(w => w.Product.Id == product.Id && w.User.Id == user.Id);

            if (!exists)
            {
                var item = new Wishlist
                {
                    Product = product,
                    User = user
                };

                _context.Wishlist.Add(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Products));
        }


        //Detalle de producto

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products

                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(b => b.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }


            string categories = string.Empty;
            foreach (ProductCategory? category in product.ProductCategories)
            {
                categories += $"{category.Category.Name}, ";
            }

            categories = categories.Substring(0, categories.Length - 2);

            AddProductToCartViewModel model = new()
            {
                Categories = categories,
                TituloDescription = product.TitleDescription,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsPromoted = product.IsPromoted,
                ProductImages = product.ProductImages,
                DiscountPercentage = product.DiscountPercentage,
                Quantity = 1,
                Stock = product.Stock,
                Brand = product.Brand,

            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AddProductToCartViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Product product = await _context.Products.FindAsync(model.Id);
            if (product == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = new()
            {
                Product = product,
                Quantity = model.Quantity,
                Remarks = model.Remarks,
                User = user
            };

            _context.TemporalSales.Add(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Products));
        }

        [Authorize]
        public async Task<IActionResult> ShowCart()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            List<TemporalSale>? temporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            ShowCartViewModel model = new()
            {
                User = user,
                TemporalSales = temporalSales,
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowCart(ShowCartViewModel model)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            model.User = user;
            model.TemporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            Response response = await _orderHelper.ProcessOrderAsync(model);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(OrderSuccess));
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(model);
        }

        //seccion de favoritos
        [Authorize]
        public async Task<IActionResult> ShowWishlist()
        {
            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var favorites = await _context.Wishlist
                .Include(w => w.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(w => w.User.Id == user.Id)
                .ToListAsync();

            WishlistViewModel model = new()
            {
                User = user,
                Products = favorites.Select(w => w.Product).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var item = await _context.Wishlist
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.Product.Id == id && w.User.Id == user.Id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Wishlist.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowWishlist));
        }



        //acciones del carrito
        public async Task<IActionResult> DecreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            if (temporalSale.Quantity > 1)
            {
                temporalSale.Quantity--;
                _context.TemporalSales.Update(temporalSale);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ShowCart));
        }

        public async Task<IActionResult> IncreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            temporalSale.Quantity++;
            _context.TemporalSales.Update(temporalSale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowCart));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            _context.TemporalSales.Remove(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowCart));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            EditTemporalSaleViewModel model = new()
            {
                Id = temporalSale.Id,
                Quantity = temporalSale.Quantity,
                Remarks = temporalSale.Remarks,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTemporalSaleViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TemporalSale temporalSale = await _context.TemporalSales.FindAsync(id);
                    temporalSale.Quantity = model.Quantity;
                    temporalSale.Remarks = model.Remarks;
                    _context.Update(temporalSale);
                    await _context.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    return View(model);
                }

                return RedirectToAction(nameof(ShowCart));
            }

            return View(model);
        }


        // se proceso el pedido 
        [Authorize]
        public IActionResult OrderSuccess()
        {
            return View();
        }

    }
}
