using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenNgocPhuongNguyen_2122110413.Data;
using NguyenNgocPhuongNguyen_2122110413.DTOs.Order;
using NguyenNgocPhuongNguyen_2122110413.DTOs.OrderDetail;
using NguyenNgocPhuongNguyen_2122110413.Model;

namespace NguyenNgocPhuongNguyen_2122110413.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                PaymentMethod = dto.PaymentMethod,
                StatusPayment = dto.StatusPayment,
                Status = "PENDING",
                OrderDetails = dto.OrderDetails.Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            order.TotalAmount = order.OrderDetails.Sum(d => d.Quantity * d.UnitPrice);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created successfully", order.Id });
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrdersByUserId(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    PaymentMethod = o.PaymentMethod,
                    StatusPayment = o.StatusPayment,
                    TotalAmount = o.TotalAmount,
                    OrderDetails = o.OrderDetails.Select(d => new OrderDetailResponseDto
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                }).ToListAsync();

            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    StatusPayment = o.StatusPayment,
                    PaymentMethod = o.PaymentMethod,
                    TotalAmount = o.TotalAmount,
                    OrderDetails = o.OrderDetails.Select(d => new OrderDetailResponseDto
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                }).ToListAsync();

            return Ok(orders);
        }

    }
}
