using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Helpers
{
    public class CombosHelper : ICombosHelper
    {

        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {

            _context = context;
        }


        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
        {
            List<SelectListItem> list = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            })
             .OrderBy(x => x.Text)
             .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync(IEnumerable<Category> filter)
        {

            List<Category> categories = await _context.Categories.ToListAsync();
            List<Category> categoriesFiltered = new();
            foreach (Category category in categories)
            {
                if (!filter.Any(c => c.Id == category.Id))
                {
                    categoriesFiltered.Add(category);
                }
            }


            List<SelectListItem> list = categoriesFiltered.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            })
            .OrderBy(x => x.Text)
            .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }


        public async Task<IEnumerable<SelectListItem>> GetComboBrandsAsync()
        {
            List<SelectListItem> list = await _context.Brands.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = $"{c.Id}"
            })
             .OrderBy(x => x.Text)
             .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Marca...]",
                Value = "0"
            });

            return list;

        }


        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {

            List<SelectListItem> list = await _context.Cities
             .Where(x => x.State.Id == stateId)
             .Select(x => new SelectListItem
             {
                 Text = x.Name,
                 Value = $"{x.Id}"
             })
             .OrderBy(x => x.Text)
             .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una ciudad...]",
                Value = "0"
            });

            return list;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = await _context.Countries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
              .OrderBy(x => x.Text)
              .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un país...]",
                Value = "0"
            });

            return list;

        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> list = await _context.States
              .Where(x => x.Country.Id == countryId)
              .Select(x => new SelectListItem
              {
                  Text = x.Name,
                  Value = $"{x.Id}"
              })
              .OrderBy(x => x.Text)
              .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una provincia...]",
                Value = "0"
            });

            return list;
        }

    }
}
