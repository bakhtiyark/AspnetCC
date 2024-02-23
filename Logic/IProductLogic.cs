using CarvedRock.Models;

namespace CarvedRock.Logic;

public interface IProductLogic
{
    Task<List<ProductModel>> GetAllProducts();
    Task<ProductModel?> GetProductById(int productId);
    Task AddNewProduct(ProductModel productToAdd);
    Task RemoveProduct(int productIdToRemove); 
    Task UpdateProduct(ProductModel productToUpdate);
}