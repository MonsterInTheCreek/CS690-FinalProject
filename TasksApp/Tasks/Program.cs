namespace Tasks;

using Spectre.Console;

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

        AnsiConsole.Write(new Rows(
            new Markup("[red]Version 3.0.0 - Release Candidate[/]"),
            new Markup(" "),
            new Markup("[yellow]!! Shipped App contains dummy data, for example purposes !![/]"),
            new Markup("[yellow]!! Remove dummy data as needed !![/]"),
            new Markup(" "),
            new Markup("[blue]All Use Cases and Functional Requirements met[/]"),
            new Markup("[blue]Replaced most kludge, although a tiny amount remains[/]"),
            new Markup("[blue]See Developer Documentation for known minor bugs[/]")
        ));
        
        Helpers.Wait();        
    }
    
    
    
}