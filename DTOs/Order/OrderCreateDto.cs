using NguyenNgocPhuongNguyen_2122110413.DTOs.OrderDetail;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Order
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string StatusPayment { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
    }
}
