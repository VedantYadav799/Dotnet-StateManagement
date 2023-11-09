using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StateManagement.Models;
namespace StateManagement.Repository
{
    public class ProductRepository
    {
    
    public  List<Product> GetProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 60000 },
            new Product { Id = 2, Name = "Mouse", Price = 500 },
            new Product { Id = 3, Name = "Key-Board", Price = 800 },
            new Product { Id = 4, Name = "Charger", Price = 900 },
            new Product { Id = 5, Name = "HeadPhones", Price = 1220 },
        };
        return products;
    }

    }
}