using DataLayer.Entites;

namespace Core.Services.Interfaces;

public interface IProduct
{
    Task<List<Product?>> GetProducts();

    Task<Product?> GetProductById(int id);
}