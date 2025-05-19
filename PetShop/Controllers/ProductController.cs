    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.DataContext;

namespace PetShop.Controllers
{

    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == Id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
