using System;
using System.Text;

namespace SweepAndProne
{
    public class Game
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            GameObjectContainer gameObjectContainer = new GameObjectContainer();
            string input = "";
            while ((input = Console.ReadLine()) != "start")
            {
                string[] data = input.Split(' ');
                string name = data[1];
                int x1 = int.Parse(data[2]);
                int y1 = int.Parse(data[3]);
                GameObject gameObject = new GameObject(name, x1, y1);
                gameObjectContainer.Add(gameObject);
            }

            while ((input = Console.ReadLine())!= "end")
            {
                string[] data = input.Split(' ');
                string command = data[0];

                if(command == "move")
                {
                    string name = data[1];
                    int x1 = int.Parse(data[2]);
                    int y1 = int.Parse(data[3]);
                    gameObjectContainer.Move(name, x1, y1);
                }

                string collisions = gameObjectContainer.SweepAndProne();
                sb.AppendLine(collisions);
            }

            Console.WriteLine(sb.ToString().Trim());
        }
    }
}
