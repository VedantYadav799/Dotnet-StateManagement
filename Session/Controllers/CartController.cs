using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StateManagement.Models;
using StateManagement.Helpers;
using StateManagement.Repository;

namespace StateManagement.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Retrieve the shopping cart from session
            List<CartItems> cart =
                SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "Cart")
                ?? new List<CartItems>();
            return View(cart);
        }

        public IActionResult AddToCart(int id, string name, decimal price)
        {
            // Retrieve the shopping cart from session
            List<CartItems> cart =
                SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "Cart")
                ?? new List<CartItems>();

            // Check if the item already exists in the cart
            var existItem = cart.FirstOrDefault(item => item.Id == id);

            if (existItem != null)
            {
                Product originalProduct = GetOriginalProduct(id);
                // If it exists, increase the quantity and decrease the price
                existItem.Price = existItem.Price + originalProduct.Price;
                existItem.Quantity++;
            }
            else
            {
                // If it doesn't exist, add a new item to the cart
                var newItem = new CartItems
                {
                    Id = id,
                    Name = name,
                    Price = price,
                    Quantity = 1
                };
                cart.Add(newItem);
            }

            // Update the cart in session
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAllFromCart(int id)
        {
            List<CartItems> cart = SessionHelper.GetObjectFromJson<List<CartItems>>(
                HttpContext.Session,
                "Cart"
            );
            CartItems itemToRemove = cart.FirstOrDefault(item => item.Id == id);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }

            return RedirectToAction("Index");
        }


        public Product GetOriginalProduct(int id)
        {
            ProductRepository r = new ProductRepository();
            var products = r.GetProducts();
            Product originalProduct = products.FirstOrDefault(item => item.Id == id);
            return originalProduct;
        }
    }
}
