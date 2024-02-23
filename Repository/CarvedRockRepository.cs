using System.Data;
using CarvedRock.Data;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Repository;

public class CarvedRockRepository(ProductContext context) : ICarvedRockRepository
{
    
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await context.Products.FirstOrDefaultAsync(x => x.Id == productId);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        context.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }
        catch (DBConcurrencyException)
        {
            if (context.Products.Any(e => e.Id == product.Id))
            {
                throw;
            }
        }
    }

    public async Task RemoveProductAsync(int productIdToRemove)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productIdToRemove);
        if (product is not null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}