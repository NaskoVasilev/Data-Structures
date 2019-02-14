using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNode
{
    public class Tree<T>
    {
        public T Value { get; set; }
        public Tree<T> Parent { get; set; }
        public List<Tree<T>> Children { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
            this.Parent = null;
        }
    }
}
