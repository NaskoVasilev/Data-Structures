using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SweepAndProne
{
    public class GameObjectContainer
    {
        private int tickCount = 0;
        private List<GameObject> gameObjects;

        public GameObjectContainer()
        {
            this.gameObjects = new List<GameObject>();
            this.tickCount = 0;
        }

        public void Add(GameObject gameObject)
        {
            this.gameObjects.Add(gameObject);
        }

        public string SweepAndProne()
        {
            this.tickCount++;
            StringBuilder sb = new StringBuilder();
            this.gameObjects = this.gameObjects.OrderBy(x => x.X1).ToList();

            for (int i = 0; i < gameObjects.Count - 1; i++)
            {
                GameObject current = gameObjects[i];
                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    GameObject next = gameObjects[j];
                    if (current.X2 < next.X1)
                    {
                        break;
                    }
                    else if (next.Y1 <= current.Y2 && next.Y2 >= current.Y1)
                    {
                        sb.AppendLine($"({tickCount}) {current} collides with {next}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public void Move(string name, int x1, int x2)
        {
            GameObject gameObject = this.gameObjects.FirstOrDefault(x => x.Name == name);
            gameObject.X1 = x1;
            gameObject.Y1 = x2;
        }
    }
}
