using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TextEditor : ITextEditor
{
    Trie<StringBuilder> users;
    Dictionary<string, Stack<string>> cache;

    public TextEditor()
    {
        this.users = new Trie<StringBuilder>();
        this.cache = new Dictionary<string, Stack<string>>();
    }

    public void Clear(string username)
    {
        AddToCache(username);
        users.GetValue(username).Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        AddToCache(username);
        users.GetValue(username).Remove(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        AddToCache(username);
        users.GetValue(username).Insert(index, str);
    }

    public int Length(string username)
    {
        return users.GetValue(username).Length;
    }

    public void Login(string username)
    {
        users.Insert(username, new StringBuilder());
        cache[username] = new Stack<string>();
    }

    public void Logout(string username)
    {
        cache.Remove(username);
    }

    public void Prepend(string username, string str)
    {
        AddToCache(username);
        users.GetValue(username).Insert(0, str);
    }

    public string Print(string username)
    {
        return GetUserString(username);
    }

    public void Substring(string username, int startIndex, int length)
    {
        AddToCache(username);
        var userString = users.GetValue(username);
        userString.Remove(0, startIndex);
        userString.Remove(length, userString.Length - length);
    }

    public void Undo(string username)
    {
        if(cache[username].Count > 0)
        {
            string lastUserString = cache[username].Pop();
            users.GetValue(username).Clear();
            users.GetValue(username).Append(lastUserString);
        }
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        if(prefix == "")
        {
            return cache.Keys;
        }

        return cache.Keys.Where(x => x.StartsWith(prefix));
    }

    private string GetUserString(string username)
    {
        string result = users.GetValue(username).ToString();
        return result;
    }

    private void AddToCache(string username)
    {
        cache[username].Push(GetUserString(username));
    }
}
