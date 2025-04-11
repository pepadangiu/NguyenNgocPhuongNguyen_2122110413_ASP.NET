using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Product
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        [Required]
        public string UserUpdate { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
