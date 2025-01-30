using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync();

        Task<IEnumerable<SelectListItem>> GetComboCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetComboStatesAsync(Guid countryId);

        Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(Guid stateId);

    }
}
