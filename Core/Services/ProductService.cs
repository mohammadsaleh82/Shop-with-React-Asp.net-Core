using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class ProductService:IProduct
    {
        private ShopContext _context;

        public ProductService(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<Product?>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
