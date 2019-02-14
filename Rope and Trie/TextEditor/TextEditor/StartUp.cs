using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static ITextEditor textEditor;
    public static UserManager userManager;

    public static void Main(string[] args)
    {
        textEditor = new TextEditor();
        userManager = new UserManager(textEditor);

        string command = Console.ReadLine();

        while (command != "end")
        {
            ProcessCommand(command);
            command = Console.ReadLine();
        }
    }

    private static void ProcessCommand(string command)
    {
        string[] data = command.Split();

        if (command.StartsWith("login"))
        {
            userManager.LogIn(data[1]);
            textEditor.Login(data[1]);
        }
        else if (command.StartsWith("logout"))
        {
            userManager.LogOut(data[1]);
            textEditor.Logout(data[1]);
        }
        else if (command.StartsWith("users"))
        {
            string prefix = "";

            if (data.Length > 1)
            {
                prefix = data[1];
            }

            IEnumerable<string> users = textEditor.Users(prefix);
            PrintAllUsers(users);
        }
        else
        {
            string username = data[0];

            if (userManager.IsLoged(username))
            {
                ProcessUserCommand(data);
            }
        }
    }

    private static void PrintAllUsers(IEnumerable<string> users)
    {
        Console.WriteLine(string.Join(Environment.NewLine, users));
    }

    private static void ProcessUserCommand(string[] data)
    {
        string username = data[0];
        string commandType = data[1];
        string text = "";
        int startIndex = 0;
        int length = 0;
        string result = null;

        switch (commandType)
        {
            case "insert":
                int index = int.Parse(data[2]);
                text = string.Join(" ", data.Skip(3)).Trim('\"', '\"');
                textEditor.Insert(username, index, text);
                break;
            case "prepend":
                text = text = string.Join(" ", data.Skip(2)).Trim('\"', '\"');
                textEditor.Prepend(username, text);
                break;
            case "substring":
                startIndex = int.Parse(data[2]);
                length = int.Parse(data[3]);
                textEditor.Substring(username, startIndex, length);
                break;
            case "delete":
                startIndex = int.Parse(data[2]);
                length = int.Parse(data[3]);
                textEditor.Delete(username, startIndex, length);
                break;
            case "clear":
                textEditor.Clear(username);
                break;
            case "length":
                result = textEditor.Length(username).ToString();
                break;
            case "print":
                result = textEditor.Print(username);
                break;
            case "undo":
                textEditor.Undo(username);
                break;
        }

        if (result != null)
        {
            Console.WriteLine(result);
        }
    }
}
