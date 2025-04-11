using NguyenNgocPhuongNguyen_2122110413.DTOs.Category;
using NguyenNgocPhuongNguyen_2122110413.Model;

namespace NguyenNgocPhuongNguyen_2122110413.Mapper
{
    public class CategoryMapper
    {
        public static Category ToCategory(CategoryCreateDTO dto)
        {
            return new Category
            {
                Name = dto.Name,
                //UserCreate = dto.UserCreate
            };
        }

        public static void UpdateCategory(Category category, CategoryUpdateDTO dto)
        {
            category.Name = dto.Name;
        }
    }
}
