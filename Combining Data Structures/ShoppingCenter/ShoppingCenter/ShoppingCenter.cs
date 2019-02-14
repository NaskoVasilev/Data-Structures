using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace ShoppingCenter
{
    public class ShoppingCenter
    {
        private const string NotFoundMessage = "No products found";
        private Dictionary<string, OrderedBag<Product>> productsByName;
        private Dictionary<string, OrderedBag<Product>> productsByProducer;
        private OrderedDictionary<decimal, OrderedBag<Product>> productsByPrice;
        private Dictionary<string, OrderedBag<Product>> byNameAndProducer;

        public ShoppingCenter()
        {
            this.productsByName = new Dictionary<string, OrderedBag<Product>>();
            this.productsByProducer = new Dictionary<string, OrderedBag<Product>>();
            this.productsByPrice = new OrderedDictionary<decimal, OrderedBag<Product>>();
            this.byNameAndProducer = new Dictionary<string, OrderedBag<Product>>();
        }

        public string AddProduct(string name, decimal price, string producer)
        {
            Product product = new Product(name, price, producer);
            this.productsByName.AppendValueToKey(name, product);
            this.productsByProducer.AppendValueToKey(producer, product);
            this.productsByPrice.AppendValueToKey(price, product);
            this.byNameAndProducer.AppendValueToKey(name + producer, product);

            return "Product added";
        }

        public string DeleteProducts(string producer)
        {
            if (!this.productsByProducer.ContainsKey(producer))
            {
                return NotFoundMessage;
            }

            OrderedBag<Product> products = this.productsByProducer[producer];
            string result = $"{products.Count} products deleted";

            foreach (var product in products)
            {
                this.productsByPrice[product.Price].Remove(product);
                this.productsByName[product.Name].Remove(product);
                this.byNameAndProducer[product.Name + product.Producer].Remove(product);
            }

            this.productsByProducer.Remove(producer);
            return result;
        }

        public string DeleteProducts(string name, string producer)
        {
            string key = name + producer;
            if(!this.byNameAndProducer.ContainsKey(key))
            {
                return NotFoundMessage;
            }

            OrderedBag<Product> products = this.byNameAndProducer[key];
            string result = $"{products.Count} products deleted";

            foreach (var product in products)
            {
                this.productsByName[product.Name].Remove(product);
                this.productsByPrice[product.Price].Remove(product);
                this.productsByProducer[product.Producer].Remove(product);
            }

            this.byNameAndProducer.Remove(key);
            return result;
        }

        public string FindProductsByName(string name)
        {
            if (!productsByName.ContainsKey(name))
            {
                return NotFoundMessage;
            }

            IEnumerable<Product> products = this.productsByName[name];
            string result = PrintProducts(products);
            return result;
        }

        public string FindProductsByProducer(string producer)
        {
            if (!this.productsByProducer.ContainsKey(producer))
            {
                return NotFoundMessage;
            }

            IEnumerable<Product> products = this.productsByProducer[producer];
            string result = PrintProducts(products);
            return result;
        }

        public string FindProductsByPriceRange(decimal startPrice, decimal endPrice)
        {
            OrderedBag<Product> products = new OrderedBag<Product>();
            var targetProductsByPrice = this.productsByPrice.Range(startPrice, true, endPrice, true);

            foreach (var prodcutByPrice in targetProductsByPrice)
            {
                foreach (var product in prodcutByPrice.Value)
                {
                    products.Add(product);
                }
            }

            string result = this.PrintProducts(products);
            return result;
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            string result = string.Join(Environment.NewLine, products);

            if (string.IsNullOrEmpty(result))
            {
                return NotFoundMessage;
            }
            return result;
        }
    }
}
