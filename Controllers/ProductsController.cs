using CarvedRock.Logic;
using CarvedRock.Models;

namespace CarvedRock.Controllers;

public class ProductsController(IProductLogic logic, List<ProductModel> products) : Controller
{
    public List<ProductModel> Products { get; set; } = products;

    public async Task<IActionResult> Index()
    {
        var products = await logic.GetAllProducts();
        return View(products);
    }
    public async Task<IActionResult> Details(int id)
    {
        var product = await logic.GetProductById(id);
        return product == null ? NotFound() : View(product);
    }
    
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,IsActive")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await logic.AddNewProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        } 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await logic.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsActive")] ProductModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await logic.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await logic.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await logic.RemoveProduct(id);
            return RedirectToAction(nameof(Index)); 
        }
        
    // private List<ProductModel>? GetSampleProducts()
    // {
    //     return new List<ProductModel> { 
    //     new ProductModel { Id = 1, Description = "Shoes", IsActive=true, Name="Adidas", Price= 199.99m },
    //     new ProductModel { Id = 2, Description = "Shoes", IsActive=true, Name="Nike", Price= 299.99m },
    //     new ProductModel { Id = 3, Description = "Shoes", IsActive=true, Name="NB", Price= 169.99m },
    //     new ProductModel { Id = 4, Description = "Shoes", IsActive=true, Name="Bata", Price= 149.99m },
    //     new ProductModel { Id = 5, Description = "Shoes", IsActive=true, Name="Asics", Price= 189.99m }};
    // }
}
