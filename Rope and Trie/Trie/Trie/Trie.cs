using System;
using System.Collections.Generic;

public class Trie<Value>
{
    private Node root;

    private class Node
    {
        public Node()
        {
            this.Next = new Dictionary<char, Node>();
        }

        public Value Value { get; set; }
        public bool IsTerminal { get; set; }
        public Dictionary<char, Node> Next { get; set; }
    }

    public Trie()
    {
        this.root = new Node();
    }


    public Value GetValue(string key)
    {
        var x = GetNode(root, key, 0);
        if (x == null || !x.IsTerminal)
        {
            throw new InvalidOperationException($"Tire does not contains - {key}");
        }

        return x.Value;
    }

    public bool Contains(string key)
    {
        var node = GetNode(this.root, key, 0);
        return node != null && node.IsTerminal;
    }

    public void Insert(string key, Value value)
    {
        root = Insert(root, key, value, 0);
    }

    public IEnumerable<string> GetByPrefix(string prefix)
    {
        var results = new Queue<string>();
        var x = GetNode(root, prefix, 0);

        this.Collect(x, prefix, results);

        return results;
    }

    private Node GetNode(Node x, string key, int d)
    {
        if (x == null)
        {
            return null;
        }

        if (d == key.Length)
        {
            return x;
        }

        Node node = null;
        char c = key[d];

        if (x.Next.ContainsKey(c))
        {
            node = x.Next[c];
        }

        return GetNode(node, key, d + 1);
    }

    private Node Insert(Node x, string key, Value val, int d)
    {
        if (d == key.Length)
        {
            x.IsTerminal = true;
            x.Value = val;
            return x;
        }

        char c = key[d];
        if (x.Next.ContainsKey(c))
        {
            Insert(x.Next[c], key, val, d + 1);
        }
        else
        {
            Node node = new Node();
            x.Next[c] = node;
            Insert(node, key, val, d + 1);
        }

        return x;
    }

    private void Collect(Node x, string prefix, Queue<string> results)
    {
        if (x == null)
        {
            return;
        }

        if (x.Value != null && x.IsTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (var c in x.Next.Keys)
        {
            Collect(x.Next[c], prefix + c, results);
        }
    }
}