using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Ecommerce.Controllers
{
    [Authorize(Roles= "Admin")]
    public class BrandsController : Controller
    {

        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        public BrandsController(DataContext context, IBlobHelper blobHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
        }




        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }


        // GET: BrandsController/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }


        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "brands");
                }

               

                //No entiendo que hacer aqui con el codigo ya que quiero que mi brand tenga el nombre y el imageId
                var brand = new Brand
                {
                    Name = model.Name,
                    ImageId = imageId,
                };




                try
                {
                    _context.Brands.Add(brand);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException?.Message.Contains("duplicate") == true)
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Marca con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException?.Message ?? "Error al guardar en la base de datos.");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }


        // GET: BrandsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            // Mapeo de Brand a BrandViewModel
            var model = new BrandViewModel
            {
                Id = brand.Id,  // Asegúrate de incluir el I
                Name = brand.Name,
                ImageId = brand.ImageId
            };

            return View(model);

        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var brand = await _context.Brands.FindAsync(id); 

                    if (brand == null)
                    {
                        return NotFound();
                    }

                    // Si el usuario sube una nueva imagen, la actualizamos
                    if (model.ImageFile != null)
                    {
                        brand.ImageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "brands");
                    }

                    brand.Name = model.Name; 

                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException?.Message.Contains("duplicate") == true)
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Marca con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException?.Message ?? "Error al guardar en la base de datos.");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        // GET: BrandsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brans = await _context.Brands
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brans == null)
            {
                return NotFound();
            }

            return View(brans);
        }

        // POST: BrandsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brans = await _context.Brands.FindAsync(id);
            if (brans != null)
            {
                _context.Brands.Remove(brans);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
