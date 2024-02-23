using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Data;

public class ProductContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    
    public string DbPath { get; set; }

    public ProductContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, "carved-rock.db");
    }

    public void SeedInitialData()
    {
        if (Products.Any())
        {
            Products.RemoveRange(Products);
            SaveChanges();
        }

        Products.Add(new Product { Id = 1, Description = "Shoes", IsActive=true, Name="Adidas", Price= 199.99m });
        Products.Add(new Product { Id = 2, Description = "Shoes", IsActive = true, Name = "Nike", Price = 299.99m });
        Products.Add(new Product { Id = 3, Description = "Shoes", IsActive = true, Name = "NB", Price = 169.99m });
        Products.Add(new Product { Id = 4, Description = "Shoes", IsActive = true, Name = "Bata", Price = 149.99m });
        Products.Add(new Product { Id = 5, Description = "Shoes", IsActive = true, Name = "Asics", Price = 189.99m });
        SaveChanges();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}