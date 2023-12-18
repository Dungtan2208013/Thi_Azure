using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppShopping.Models;
using static NuGet.Packaging.PackagingConstants;

namespace WebAppShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrdersController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var existingOrder = await _context.OrderTbl.FindAsync(id)
;

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Chỉ cập nhật các trường cụ thể từ đối tượng updatedOrder
            existingOrder.OrderDelivery = updatedOrder.OrderDelivery;
            existingOrder.OrderAddress = updatedOrder.OrderAddress;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Orders

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'OrderContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }


        [HttpPut("orders/{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == id);

            if (existingOrder == null)
            {
                return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy đơn hàng
            }

            // Cập nhật thông tin đơn hàng
            existingOrder.OrderDelivery = updatedOrder.OrderDelivery;
            existingOrder.OrderAddress = updatedOrder.OrderAddress;

            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return Ok(existingOrder); // Trả về đơn hàng đã được cập nhật
        }
        


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
