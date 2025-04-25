using NguyenNgocPhuongNguyen_2122110413.DTOs.OrderDetail;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Order
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string StatusPayment { get; set; }
        public List<OrderDetailResponseDto> OrderDetails { get; set; }
    }
}
