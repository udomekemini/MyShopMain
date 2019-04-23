using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository() { 

            products = cache["products"] as List<Product>;
            //constructor to check if there is a cache called products then it returns a list of products
            if (products == null) {

                products = new List<Product>();

            }
            }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert (Product p)
        {
            products.Add(p);
        }

        public void Update (Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }

            else
            {
                throw new Exception("no product found");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
               return product;
            }

            else
            {
                throw new Exception("no product found");
            }
        }

        //returns product list that can be queried
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete (string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }

            else
            {
                throw new Exception("no product found");
            }
        }
    }
}
