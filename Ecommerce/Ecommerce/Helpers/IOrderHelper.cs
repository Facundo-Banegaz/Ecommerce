using Ecommerce.Common;
using Ecommerce.Models;

namespace Ecommerce.Helpers
{
    public interface IOrderHelper
    {
        Task<Response> ProcessOrderAsync(ShowCartViewModel model);
        Task<Response> CancelOrderAsync(int id);


    }
}
