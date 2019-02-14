using System.Collections.Generic;

public class UserManager
{
    public Dictionary<string, bool> LogedInUsers { get; private set; }
    private ITextEditor textEditor;

    public UserManager(ITextEditor textEditor)
    {
        this.textEditor = textEditor;
        this.LogedInUsers = new Dictionary<string, bool>();
    }

    public void LogIn(string username)
    {
        if(LogedInUsers.ContainsKey(username))
        {
            textEditor.Clear(username);
        }
        else
        {
            this.LogedInUsers.Add(username, true);
        }
    }

    public void LogOut(string username)
    {
        this.LogedInUsers.Remove(username);
    }

    public bool IsLoged(string username)
    {
        return this.LogedInUsers.ContainsKey(username);
    }
}
