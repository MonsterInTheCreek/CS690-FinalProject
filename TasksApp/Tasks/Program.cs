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
        Console.WriteLine("Version 2.0.0 - Beta Release");
        Console.WriteLine("Known current limitations:");
        Console.WriteLine("--------------------------");
        Console.WriteLine("All Use Cases and Functional Requirements met");
        Console.WriteLine("Some kludge remains, but pragmatically project is successful.");
        Console.WriteLine("My unit tests aren't complete, could be fleshed out more.");
        Helpers.Wait();
    }
}