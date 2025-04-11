using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Category
{
    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
