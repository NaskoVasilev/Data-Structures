using System;

namespace ShoppingCenter
{
    public class Product : IComparable<Product>
    {
        public Product(string name, decimal price, string producer)
        {
            this.Name = name;
            this.Price = price;
            this.Producer = producer;
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Producer { get; private set; }

        public int CompareTo(Product otherProduct)
        {
            int compareByName = this.Name.CompareTo(otherProduct.Name);
            if(compareByName == 0)
            {
                int compareByProducer = this.Producer.CompareTo(otherProduct.Producer);
                if(compareByProducer == 0)
                {
                    return this.Price.CompareTo(otherProduct.Price);
                }

                return compareByProducer;
            }

            return compareByName;
        }

        public override string ToString()
        {
            return "{" + $"{this.Name};{this.Producer};{this.Price:F2}" + "}";
        }
    }
}
