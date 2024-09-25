using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ecommerce.Data
{
    public class EcommerceContext : DbContext
    {
        public  EcommerceContext(DbContextOptions<EcommerceContext> options) : base (options)
         
        {
        
        }
            
            
    }
}
