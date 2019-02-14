using System;
using System.Text;

namespace ShoppingCenter
{
    public class Engine
    {
        private ShoppingCenter shoppingCenter;

        public Engine()
        {
            shoppingCenter = new ShoppingCenter();
        }

        public void Run()
        {
            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string input = Console.ReadLine();
                string result = ProcessCommand(input);
                Console.WriteLine(result);
            }
        }

        private string ProcessCommand(string input)
        {
            int spaceIndex = input.IndexOf(' ');
            string command = input.Substring(0, spaceIndex);
            string[] data = input.Substring(spaceIndex + 1).Split(';');
            string producer = "";
            string name = "";
            decimal price = 0M;
            string result = "";

            switch (command)
            {
                case "AddProduct":
                    name = data[0];
                    price = decimal.Parse(data[1]);
                    producer = data[2];
                    result = shoppingCenter.AddProduct(name, price, producer);
                    break;
                case "DeleteProducts":
                    if(data.Length == 1)
                    {
                        producer = data[0];
                        result = shoppingCenter.DeleteProducts(producer);
                    }
                    else
                    {
                        producer = data[1];
                        name = data[0];
                        result = shoppingCenter.DeleteProducts(name, producer);
                    }
                    break;
                case "FindProductsByName":
                    name = data[0];
                    result = shoppingCenter.FindProductsByName(name);
                    break;
                case "FindProductsByProducer":
                    producer = data[0];
                    result = shoppingCenter.FindProductsByProducer(producer);
                    break;
                case "FindProductsByPriceRange":
                    decimal startPrice = decimal.Parse(data[0]);
                    decimal endPrice = decimal.Parse(data[1]);
                    result = shoppingCenter.FindProductsByPriceRange(startPrice, endPrice);
                    break;
            }

            return result;
        }
    }
}
