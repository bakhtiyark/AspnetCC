using System.ComponentModel.DataAnnotations;
using CarvedRock.Data;

namespace CarvedRock.Models;
public class ProductModel
{
    public int Id { get; set; }
    [Microsoft.Build.Framework.Required]
    public string Name { get; set; } = null!;

    [Microsoft.Build.Framework.Required] 
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Currency)]
    [Range(0.01, 1000, ErrorMessage = "Value for {0} must be between " + "{1:C} and {2:C}")]
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public static ProductModel FromProduct(Product product)
    {
        return new ProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            IsActive = product.IsActive
        };
    }

    public Product ToProduct()
    {
        return new Product()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            IsActive = IsActive
        };
    }

}
