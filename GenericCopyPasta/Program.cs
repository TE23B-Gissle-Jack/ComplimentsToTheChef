using System.Diagnostics;

string usersRoot = @"C:\Users";
Dictionary<string, Dictionary<string, string>> users = new Dictionary<string, Dictionary<string, string>>();
List<string> filter = ["Default", "Public", "All Users", "Default User"];

foreach (string userDir in Directory.GetDirectories(usersRoot))
{
    Dictionary<string, string> directories = new Dictionary<string, string>
    {
        { "desktop", Path.Combine(userDir, "Desktop") },
        { "pictures", Path.Combine(userDir, "Pictures") },
        { "contacts", Path.Combine(userDir, "Contacts") }
    };
    users.Add(Path.GetFileName(userDir), directories);
}
foreach (string target in filter)
{
    users.Remove(target);
}

Stack<List<string>> history = new();

///////////////////////////////////////////////////////
List<string> userNames = users.Keys.ToList();

while (true)
{
    string selectedUser = SelectFilePath(userNames, "Select user:");

    if (selectedUser == "back")
    {
        if (history.Count == 0) return;
        userNames = history.Pop();
        continue;
    }

    history.Push(userNames);

    var dirs = users[selectedUser];
    List<string> dirKeys = dirs.Keys.ToList();

    while (true)
    {
        string selectedDirKey = SelectFilePath(dirKeys, "Select folder:");

        if (selectedDirKey == "back")
        {
            dirKeys = history.Pop();
            break;
        }

        history.Push(dirKeys);

        List<string> more = Directory.GetFiles(users[selectedUser][selectedDirKey]).ToList();

        while (true)
        {
            string selectedMore = SelectFilePath(more, "Select folder:");

            if (selectedMore == "back")
            {
                more = history.Pop();
                break;
            }

            Process.Start(selectedMore);
            Console.ReadLine();
        }
    }
}
///////////////////////////////////////////////////////

string SelectFilePath(List<string> current, string prompt)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("back");
    Console.ForegroundColor = ConsoleColor.White;

    List<string> names = new List<string>();
    foreach (var item in current)
    {
        string name = Path.GetFileName(item);
        Console.WriteLine(name);
        names.Add(name);
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(prompt);
    Console.ResetColor();

    string resp = "";
    while (!names.Contains(resp) && resp != "back")//mind your caps
    {
        resp = Console.ReadLine();

        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write("                                ");
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    Console.Clear();

    if (resp == "back") return "back";
    return current[names.IndexOf(resp)];
}