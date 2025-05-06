using Ecommerce.Data;
using Ecommerce.Data.Entities;
using Ecommerce.Enums;
using Ecommerce.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vereyon.Web;


namespace Ecommerce.Controllers
{
  
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly IFlashMessage _flashMessage;
        private readonly IOrderHelper _orderHelper;

        public OrdersController(DataContext context, IFlashMessage flashMessage,IOrderHelper orderHelper)
        {
            _context = context;
            _flashMessage = flashMessage;
            _orderHelper = orderHelper;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {


            var res = await _context.Orders
                .Include(s => s.User)
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .ToListAsync();


            return View(res);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders
                .Include(s => s.User)
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dispatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Nuevo)
            {
                _flashMessage.Danger("Solo se pueden despachar pedidos que estén en estado 'nuevo'.");


            }
            else
            {
                order.OrderStatus = OrderStatus.Despachado;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'despachado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Send(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Despachado)
            {
                _flashMessage.Danger("Solo se pueden enviar pedidos que estén en estado 'despachado'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Enviado;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'enviado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus != OrderStatus.Enviado)
            {
                _flashMessage.Danger("Solo se pueden confirmar pedidos que estén en estado 'enviado'.");
            }
            else
            {
                order.OrderStatus = OrderStatus.Confirmado;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'confirmado'.");
            }

            return RedirectToAction(nameof(Details), new { Id = order.Id });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderStatus == OrderStatus.Cancelado)
            {
                _flashMessage.Danger("No se puede cancelar un pedido que esté en estado 'cancelado'.");
            }
            else
            {
                await _orderHelper.CancelOrderAsync(order.Id);
                _flashMessage.Confirmation("El estado del pedido ha sido cambiado a 'cancelado'.");
            }

            return RedirectToAction(nameof(Details), new { order.Id });
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyOrders()
        {
            return View(await _context.Orders
                .Include(s => s.User)
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .Where(s => s.User.UserName == User.Identity.Name)
                .ToListAsync());
        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order sale = await _context.Orders
                .Include(s => s.User)
                .Include(s => s.OrderDetails)
                .ThenInclude(sd => sd.Product)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }


    }
}
