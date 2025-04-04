using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string UserCreate { get; set; }

        public string UserUpdate { get; set; }

        public string UserDelete { get; set; }

        // Khoá ngoại đến bảng Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
