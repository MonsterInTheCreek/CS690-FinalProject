namespace Tasks;

class Program
{
    static void Main(string[] args)
    {
        Version();
        AppUI appUI = new AppUI();
        appUI.Show();
    }

    static void Version()
    {
        Console.Clear();
        Console.WriteLine("Version 3.0.0 - Release Candidate");
        Console.WriteLine("");
        Console.WriteLine("All Use Cases and Functional Requirements met");
        Console.WriteLine("Replaced most kludge, although a tiny amount remains");
        Helpers.Wait();
    }
}