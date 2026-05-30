

string usersRoot = @"C:\Users";
Dictionary<string, Dictionary<string, string>> users = new Dictionary<string, Dictionary<string, string>>();
List<string> filter = [
    Path.Combine(usersRoot,"Default"),
    Path.Combine(usersRoot,"Public"), 
    Path.Combine(usersRoot,"All Users"), 
    Path.Combine(usersRoot,"Default User")];

foreach (string userDir in Directory.GetDirectories(usersRoot))
{
    Dictionary<string, string> directories = new Dictionary<string, string>
    {
        { "desktop", Path.Combine(userDir, "Desktop") },
        { "pictures", Path.Combine(userDir, "Pictures") },
        { "contacts", Path.Combine(userDir, "Contacts") }
    };
    users.Add(userDir, directories);
}
foreach (string target in filter)
{
    users.Remove(target);
}
// foreach (var user in users)
// {
//     Console.WriteLine(user.Key);
// }
// Console.ForegroundColor = ConsoleColor.Yellow;
// Console.WriteLine("select user");
// Console.ForegroundColor = ConsoleColor.White;
// string resp = "";
// while (!users.Keys.Contains(resp))
// {
//     resp = Console.ReadLine();
//     Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
//     Console.WriteLine("                                          ");//indeed
//     Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
// }
// history.Push(resp);
// Console.Clear();
// Console.ForegroundColor = ConsoleColor.DarkYellow;
// Console.WriteLine("back");
// Console.ForegroundColor = ConsoleColor.White;
// foreach (var directory in users[resp])
// {
//     Console.WriteLine(directory.Key);
// }
// Console.ForegroundColor = ConsoleColor.Yellow;
// Console.WriteLine("select directory");
// Console.ForegroundColor = ConsoleColor.White;
// string resp2 = "";
// while (!users[resp].Keys.Contains(resp2) && resp2 != "back")
// {
//     resp2 = Console.ReadLine();
//     Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
//     Console.WriteLine("                                          ");//indeed
//     Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
// }
// Console.WriteLine("!!!!");
// Console.ReadLine();

Stack<string> history = new Stack<string>();

string user = SelectFilePath(users.Keys.ToList(), "select user");

if (user == "back") return;

history.Push(user);

while (true)
{
    string dir = SelectFilePath(users[user].Keys.ToList(), "select directory");

    if (dir == "back")
    {
        if (history.Count > 1)
        {
            history.Pop();          // remove user
            user = history.Peek();  //back
            Console.Clear();
            continue;
        }
        else
        {
            Console.WriteLine("No more history");
            break;
        }
    }

    history.Push(dir);

    Console.WriteLine(users[user][dir]);
}

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
    while (!names.Contains(resp)&& resp != "back")
    {
        resp = Console.ReadLine().ToLower();

        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write("                                ");//indeed
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    Console.Clear();
    if (resp == "back") return "back";
    return current[names.IndexOf(resp)];
}