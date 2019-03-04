using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private List<Product> byInsertion;
    private SortedDictionary<string, Product> byLabel;
    OrderedDictionary<double, List<Product>> byPrice;
    Dictionary<int, HashSet<Product>> byQuantity;

    public Instock()
    {
        this.byInsertion = new List<Product>();
        this.byLabel = new SortedDictionary<string, Product>();
        this.byPrice = new OrderedDictionary<double, List<Product>>((x, y) => y.CompareTo(x));
        this.byQuantity = new Dictionary<int, HashSet<Product>>();
    }

    public int Count => this.byInsertion.Count;

    public void Add(Product product)
    {
        this.byInsertion.Add(product);
        this.byLabel.Add(product.Label, product);

        if (!this.byPrice.ContainsKey(product.Price))
        {
            this.byPrice[product.Price] = new List<Product>();
        }
        this.byPrice[product.Price].Add(product);

        AddByQuantity(product);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.byLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var targetProduct = this.byLabel[product];
        this.byQuantity[targetProduct.Quantity].Remove(targetProduct);
        this.byLabel[product].Quantity = quantity;
        this.AddByQuantity(targetProduct);
    }

    public bool Contains(Product product)
    {
        return this.byLabel.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (this.byInsertion.Count <= index || index < 0)
        {
            throw new IndexOutOfRangeException();
        }
        return this.byInsertion[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        if (!this.byPrice.ContainsKey(price))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byPrice[price];
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (!this.byQuantity.ContainsKey(quantity))
        {
            return Enumerable.Empty<Product>();
        }

        return this.byQuantity[quantity];
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        List<Product> products = new List<Product>();
        foreach (var productByPrice in this.byPrice.Range(hi, true, lo, false))
        {
            products.AddRange(productByPrice.Value);
        }

        return products;
    }

    public Product FindByLabel(string label)
    {
        if (!this.byLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.byLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (this.byLabel.Count < count || count < 0)
        {
            throw new ArgumentException();
        }

        var products = new List<Product>();
        foreach (var product in this.byLabel)
        {
            if (count == 0)
            {
                break;
            }

            products.Add(product.Value);
            count--;
        }

        return products;
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count < 0 || count > this.Count)
        {
            throw new ArgumentException();
        }

        foreach (var productByPrice in this.byPrice)
        {
            if (count <= 0)
            {
                break;
            }
            foreach (var product in productByPrice.Value)
            {
                if (count <= 0)
                {
                    break;
                }
                yield return product;
                count--;
            }
        }
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return this.byInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private void AddByQuantity(Product product)
    {
        if (!this.byQuantity.ContainsKey(product.Quantity))
        {
            this.byQuantity.Add(product.Quantity, new HashSet<Product>());
        }
        this.byQuantity[product.Quantity].Add(product);
    }
}