using CarvedRock.Models;
using CarvedRock.Repository;

namespace CarvedRock.Logic;

public class ProductLogic : IProductLogic
{
    private readonly ICarvedRockRepository _repo;

    public ProductLogic(ICarvedRockRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        var products = await _repo.GetAllProductsAsync();
        return products.Select(ProductModel.FromProduct).ToList();
    }

    public async Task<ProductModel?> GetProductById(int productId)
    {
        var product = await _repo.GetProductByIdAsync(productId);
        return product == null ? null : ProductModel.FromProduct(product);
    }

    public async Task AddNewProduct(ProductModel productToAdd)
    {
        var productToSave = productToAdd.ToProduct();
        await _repo.AddProductAsync(productToSave);
    }

    public async Task RemoveProduct(int productIdToRemove)
    {
        await _repo.RemoveProductAsync(productIdToRemove);
    }

    public async Task UpdateProduct(ProductModel productToUpdate)
    {
        var product = productToUpdate.ToProduct();
        await _repo.UpdateProductAsync(product);
    }
}