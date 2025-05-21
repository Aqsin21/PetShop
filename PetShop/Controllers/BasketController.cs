using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using PetShop.DataContext;
using PetShop.Models;

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

            var existBasketItem = basket.Find(x => x.ProductId == id);
            if(existBasketItem == null)
            {
                basket.Add(new BasketItem
                {
                    ProductId = id,
                    Quantity = 1
                });
            }
            else
            {
                existBasketItem.Quantity++;
            }
            var basketJson = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append(BasketCookieKey ,basketJson, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddHours(1),
              
            });

            return RedirectToAction("Index", "Home");
        }

        private List<BasketItem> GetBasketItems()
        {
            var basket = Request.Cookies[BasketCookieKey];
            if (string.IsNullOrEmpty(basket))
            {
                return new List<BasketItem>();
            }
            var basketItems =JsonConvert.DeserializeObject<List<BasketItem>>(basket);
            if (basketItems == null)
            {
                return new List<BasketItem>();
            }

            return basketItems ;
        }
    }


}

