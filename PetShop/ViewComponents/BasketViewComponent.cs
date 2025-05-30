﻿
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.DataContext;
using PetShop.Models;

namespace Pb304PetShop.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public BasketViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var basket = Request.Cookies["Basket"];
            if (string.IsNullOrEmpty(basket))
            {
                return Content("0");
            }

            var basketItems = JsonConvert.DeserializeObject<List<BasketItem>>(basket);

            var cart = new CartViewModel();
            var cartItemList = new List<CartItemViewModel>();

            foreach (var item in basketItems ?? [])
            {
                var product = _dbContext.Products.Find(item.ProductId);

                if (product == null) continue;

                cartItemList.Add(new CartItemViewModel
                {
                    Name = product.Name,
                    Description = product.Name,
                    Price = product.Price,
                    Quantity = item.Quantity
                });
            }

            cart.Items = cartItemList;
            cart.Quantity = cartItemList.Sum(x => x.Quantity);
            cart.Total = cartItemList.Sum(x => x.Quantity * x.Price);
            return View("~/Views/Shared/Basket/Default.cshtml", cart); 
        }
    }
}
