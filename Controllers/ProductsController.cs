using CarvedRock.Logic;
namespace CarvedRock.Controllers;

public class ProductsController(IProductLogic logic) : Controller
{
    public List<ProductModel> Products { get; set; } = null!;

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
        if (id == null) return NotFound();

        var product = await logic.GetProductById(id.Value);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsActive")] ProductModel product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await logic.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var product = await logic.GetProductById(id.Value);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }
}