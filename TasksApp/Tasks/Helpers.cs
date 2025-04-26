using System.Diagnostics.Tracing;

namespace Tasks;

using Spectre.Console;

// helper methods centralized
public static class Helpers
{
    public static void Wait()
    {
        Console.WriteLine("");
        Console.WriteLine("...Press any key...");
        Console.ReadKey(true);
        Console.Clear();
    }

    public static void DisplayNames(string topTitle, List<string> elements) 
    // This took much longer to figure out than I projected...
    {
        Console.Clear();
        var panel = new Panel(
            new Rows(elements.Select(yada => new Text(yada)))
        );
        AnsiConsole.Write(topTitle + Environment.NewLine);
        AnsiConsole.Write(panel);
    }

    public static string MakeChoice(List<string> choices)
    // a form of validation, user can only choose from list
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Make a selection:")
                .AddChoices(choices));
        return choice;
    }

    public static string RequestString(string message)
    // includes validation to prevent user from being too creative
    {
        string userString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .Validate((input) => input switch
                {
                    _ when input.Length == 0 => ValidationResult.Error("Word must be longer than 0 characters"),
                    _ when input.Length > 12 => ValidationResult.Error("Word must be shorter than 12 characters"),
                    _ when !input.All(char.IsLetter) => ValidationResult.Error("Please, no numbers, spaces, or special characters"),
                    
                    _ => ValidationResult.Success()
                }));
        
        return userString;
    }

    public static int RequestInteger(string message)
    {
        // includes validation to prevent user from being too creative
        string userIntAsString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .Validate((input) => input switch
                {
                    _ when !int.TryParse(input, out _) => ValidationResult.Error("Number must be an integer"),
                    _ when input.Length == 0 => ValidationResult.Error("Number must not be null"),
                    _ when int.Parse(input) < 0 => ValidationResult.Error("Number cannot be less than 0"),
                    
                    _ => ValidationResult.Success()
                }));

        return int.Parse(userIntAsString);
    }
    
    public static DateTime RequestDate(string message)
    {
        // includes validation to prevent user from being too creative
        string userDateAsString = AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .Validate((input) => input switch
                {
                    _ when !DateTime.TryParse(input, out _) => ValidationResult.Error("Please use date format mm/dd/yy"),
                    
                    _ => ValidationResult.Success()
                }));

        return DateTime.Parse(userDateAsString);
    }
}