using System.ComponentModel.DataAnnotations;

namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Category
{
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }

        //public string UserCreate { get; set; }
    }
}
