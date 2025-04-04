using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Một Category có nhiều Product
        public List<Product> Products { get; set; }
    }
}
