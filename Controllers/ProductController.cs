using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenNgocPhuongNguyen_2122110413.Data;
using NguyenNgocPhuongNguyen_2122110413.DTOs.Product;
using NguyenNgocPhuongNguyen_2122110413.Mapper;
using NguyenNgocPhuongNguyen_2122110413.Model;

namespace NguyenNgocPhuongNguyen_2122110413.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? categoryId)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            var products = await productsQuery.ToListAsync();
            return Ok(products);
        }
        [HttpGet("phan-trang")]
        public IActionResult GetAll(int? categoryId, int page = 1, int pageSize = 8)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            var totalItems = query.Count();
            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                totalItems,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                data = products
            };

            return Ok(result);
        }
        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDTO dto)
        {
            string imageUrl = null;
            if (dto.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                imageUrl = $"/uploads/{fileName}";
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Brand = dto.Brand,
                IsAvailable = dto.IsAvailable,
                CategoryId = dto.CategoryId,
                ImageUrl = imageUrl,
                UserCreate="pn",
                UserDelete = "pn",
                UserUpdate="pn"
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDTO dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Nếu có ảnh mới thì thay
            if (dto.ImageFile != null)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);
                product.ImageUrl = "/uploads/" + fileName;
            }

            // Update fields
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.Brand = dto.Brand;
            product.IsAvailable = dto.IsAvailable;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(product);
        }
        // DELETE: api/product/5?userDelete=admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Đã xoá sản phẩm." });
        }
    }
}
