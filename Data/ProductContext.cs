using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Data;

public class ProductContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    
    public string DbPath { get; set; }

    public ProductContext(IConfiguration config)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, config.GetConnectionString("ProductDbFileName"));
    }

    public void SeedInitialData()
    {
        if (Products.Any())
        {
            Products.RemoveRange(Products);
            SaveChanges();
        }
        if (Categories.Any())
        {
            Categories.RemoveRange(Categories);
            SaveChanges();
        }

        var footwearCategory = new Category{Id=1000, Name = "Footwear"};
        var equipmentCategory = new Category{Id=2000, Name = "Equipment"};

        Products.Add(new Product { Id = 1, Description = "Shoes", IsActive=true, Category = footwearCategory, Name="Adidas", Price= 199.99m });
        Products.Add(new Product { Id = 2, Description = "Shoes", IsActive = true, Category = footwearCategory, Name = "Nike", Price = 299.99m });
        Products.Add(new Product { Id = 3, Description = "Shoes", IsActive = true, Category = footwearCategory, Name = "NB", Price = 169.99m });
        Products.Add(new Product { Id = 4, Description = "Shoes", IsActive = true, Category = footwearCategory, Name = "Bata", Price = 149.99m });
        Products.Add(new Product { Id = 5, Description = "Shoes", IsActive = true, Category = footwearCategory, Name = "Asics", Price = 189.99m });
        Products.Add(new Product { Id = 6, Description = "Great for brewing old school tea", IsActive = true, Category = equipmentCategory, Name = "Primus", Price = 189.99m });
        Categories.Add(footwearCategory);
        Categories.Add(equipmentCategory);
        SaveChanges();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}