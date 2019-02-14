using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; set; }

    public List<Tree<T>> Children { get; private set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>(children);
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', 2 * indent)}{this.Value}");

        foreach (var child in this.Children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        List<T> result = new List<T>();

        this.OrderDFS(this, result);

        return result;
    }


    private void OrderDFS(Tree<T> node, List<T> result)
    {
        foreach (var child in node.Children)
        {
            OrderDFS(child, result);
        }

        result.Add(node.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        Queue<Tree<T>> queue = new Queue<Tree<T>>();
        List<T> result = new List<T>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            Tree<T> currentTree = queue.Dequeue();
            result.Add(currentTree.Value);

            foreach (Tree<T> child in currentTree.Children)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}
