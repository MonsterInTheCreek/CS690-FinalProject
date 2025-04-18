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
        Console.WriteLine("Version 1.0.1 - Alpha Release");
        Console.WriteLine("Known current limitations:");
        Console.WriteLine("--------------------------");
        Console.WriteLine("Target/Action - not used internally for new task creation");
        Console.WriteLine("Target/Action - cannot remove items");
        Console.WriteLine("Low priority Supplies functionality not implemented at all");
        Helpers.Wait();
    }
}