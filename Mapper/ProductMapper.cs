﻿using NguyenNgocPhuongNguyen_2122110413.DTOs.Product;
using NguyenNgocPhuongNguyen_2122110413.Model;

namespace NguyenNgocPhuongNguyen_2122110413.Mapper
{
    
        public static class ProductMapper
        {
            // Chuyển từ DTO sang Model khi tạo mới
            public static Product ToProduct(ProductCreateDTO dto)
            {
                return new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    StockQuantity = dto.StockQuantity,
                    Brand = dto.Brand,
                    IsAvailable = dto.IsAvailable,
                    UserUpdate = "pnguyn",
                    UserDelete = "pnguyn",
                    CreatedAt = DateTime.Now,
                    CategoryId = dto.CategoryId
                };
            }

            // Cập nhật thông tin sản phẩm từ DTO
            public static void UpdateProduct(Product product, ProductUpdateDTO dto)
            {
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Price = dto.Price;
                product.StockQuantity = dto.StockQuantity;
                product.Brand = dto.Brand;
                product.IsAvailable = dto.IsAvailable;
                product.CategoryId = dto.CategoryId;
                product.UserDelete = "pnguyn"; // Có thể bỏ nếu không dùng
                product.UpdatedAt = DateTime.Now;
            }

            // Chuyển từ Product sang DTO để trả về client

        }
    }
