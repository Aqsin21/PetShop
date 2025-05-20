using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using PetShop.DataContext;

namespace PetShop.Controllers
{
    public class BasketController(AppDbContext dbContext) : Controller
    {
        private readonly AppDbContext _dbContext = dbContext;
        private const string BasketCookieKey = "Basket";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToBasket(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return BadRequest();
            }

            var basket= GetBasketItems();

            basket.Add(id);
            var basketJson = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append(BasketCookieKey ,basketJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1),
              
            });

            return RedirectToAction("Index", "Home");
        }

        private List<int> GetBasketItems()
        {
            var basket = Request.Cookies[BasketCookieKey];
            if (string.IsNullOrEmpty(basket))
            {
                return new List<int>();
            }
            var basketItems =JsonConvert.DeserializeObject<List<int>>(basket);
            if (basketItems == null)
            {
                return new List<int>();
            }

            return basketItems;
        }
    }


}

