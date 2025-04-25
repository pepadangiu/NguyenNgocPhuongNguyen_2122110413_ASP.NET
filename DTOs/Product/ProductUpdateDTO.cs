
namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Product
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Brand { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; } // 👈 File thực tế

    }
}
