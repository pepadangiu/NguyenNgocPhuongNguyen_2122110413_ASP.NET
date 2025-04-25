﻿namespace NguyenNgocPhuongNguyen_2122110413.DTOs.OrderDetail
{
    public class OrderDetailResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } // nếu muốn show
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
