using Ecommerce.Data.Entities;
using Ecommerce.Data;
using Ecommerce.Enums;
using Ecommerce.Models;
using Ecommerce.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Helpers
{
    public class OrderHelper: IOrderHelper
    {
        private readonly DataContext _context;

        public OrderHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<Response> ProcessOrderAsync(ShowCartViewModel model)
        {
            //me fijo si hay un producto en inventario
            Response response = await CheckInventoryAsync(model);
            if (!response.IsSuccess)
            {
                return response;
            }

          Order order = new()
            {
                Date = DateTime.UtcNow,
                User = model.User,
                Remarks = model.Remarks,
                OrderDetails = new List<OrderDetail>(),
                OrderStatus = OrderStatus.Nuevo,
            };

            foreach (TemporalSale? item in model.TemporalSales)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Remarks = item.Remarks,
                });
                Product product = await _context.Products
                    .Include(p => p.Brand)
                    .FirstOrDefaultAsync(p => p.Id == item.Product.Id);

                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    _context.Products.Update(product);
                }

                _context.TemporalSales.Remove(item);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return response;
        }

        private async Task<Response> CheckInventoryAsync(ShowCartViewModel model)
        {
            Response response = new() { IsSuccess = true };
            foreach (TemporalSale? item in model.TemporalSales)
            {
                Product product = await _context.Products
        .Include(p => p.Brand)
        .FirstOrDefaultAsync(p => p.Id == item.Product.Id);

                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"El producto {item.Product.Name}, ya no está disponible";
                    return response;
                }
                if (product.Stock < item.Quantity)
                {
                    response.IsSuccess = false;
                    response.Message = $"Lo sentimos no tenemos existencias suficientes del producto {item.Product.Name}, para tomar su pedido. Por favor disminuir la cantidad o sustituirlo por otro.";
                    return response;
                }
            }
            return response;
        }
        public async Task<Response> CancelOrderAsync(int id)
        {
            Order order = await _context.Orders
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach (OrderDetail saleDetail in order.OrderDetails)
            {
                Product product = await _context.Products.FindAsync(saleDetail.Product.Id);
                if (product != null)
                {
                    product.Stock += saleDetail.Quantity;
                }
            }

            order.OrderStatus = OrderStatus.Cancelado;
            await _context.SaveChangesAsync();
            return new Response { IsSuccess = true };
        }


    }
}
