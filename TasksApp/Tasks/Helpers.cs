namespace Tasks;

using Spectre.Console;

// helper methods centralized
public static class Helpers
{
    public static string RequestInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public static void Wait()
    {
        Console.WriteLine("");
        Console.WriteLine("...Press any key...");
        Console.ReadKey(true);
        Console.Clear();
    }

    public static string MakeChoice(List<string> choices)
    {
        // Using Spectre.Console
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Make a selection:")
                .AddChoices(choices));
        return choice;
    }
}