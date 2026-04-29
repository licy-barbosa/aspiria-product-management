using Microsoft.EntityFrameworkCore;
using Aspiria.Models;
using Aspiria.Data;

namespace Aspiria.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p=> p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            if (await _context.Products.AnyAsync(p => p.Name == product.Name))
                throw new Exception("El nombre debe ser único");

            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("El nombre ya existe en la base de datos");
            }

            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var exists = await _context.Products.AnyAsync(p => p.Id == product.Id);
            if (!exists) return false;

            var duplicate = await _context.Products
                .AnyAsync(p => p.Name == product.Name && p.Id != product.Id);

            if (duplicate)
                return false; // 👈 en vez de throw

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}