﻿using Microsoft.EntityFrameworkCore;
using NguyenNgocPhuongNguyen_2122110413.Model;

namespace NguyenNgocPhuongNguyen_2122110413.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
