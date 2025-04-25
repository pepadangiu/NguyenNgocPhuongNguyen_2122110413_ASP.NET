using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Product
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; } // 🆕 Mô tả sản phẩm

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; } // 🆕 Số lượng tồn kho

        public IFormFile ImageFile { get; set; } // 👈 File thực tế

        public string Brand { get; set; } // 🆕 Thương hiệu

        public bool IsAvailable { get; set; } = true; // 🆕 Mặc định là true

      

        // Khóa ngoại đến bảng Category
        [Required]
        public int CategoryId { get; set; }
    }
}
