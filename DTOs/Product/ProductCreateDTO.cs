namespace NguyenNgocPhuongNguyen_2122110413.DTOs.Product
{
    public class ProductCreateDTO
    {

        public string Name { get; set; }

        public decimal Price { get; set; }


        public string UserCreate { get; set; }


        // Khoá ngoại đến bảng Category
        public int CategoryId { get; set; }
    }
}
