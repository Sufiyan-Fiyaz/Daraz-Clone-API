using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Daraz_Clone.Data;
using Daraz_Clone.Models;
using Daraz_Clone.DTOs;

namespace Daraz_Clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orderitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponseDto>>> GetOrderItems()
        {
            var items = await _context.OrderItems
                .Select(i => new OrderItemResponseDto
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToListAsync();

            return Ok(items);
        }

        // GET: api/orderitems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemResponseDto>> GetOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            return new OrderItemResponseDto
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            };
        }

        // POST: api/orderitems/{orderId}
        [HttpPost("{orderId}")]
        public async Task<ActionResult<OrderItemResponseDto>> CreateOrderItem(int orderId, OrderItemCreateDto dto)
        {
            // check if order exists
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return NotFound("Order not found");

            var item = new OrderItem
            {
                OrderId = orderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price
            };

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, new OrderItemResponseDto
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            });
        }

        // PUT: api/orderitems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemCreateDto dto)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            item.ProductId = dto.ProductId;
            item.Quantity = dto.Quantity;
            item.Price = dto.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/orderitems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
