namespace Tasks;

using Spectre.Console;

public class SupplyManager
{
    private readonly string _nl = Environment.NewLine;  // save space
    public List<ActionSupply> ActionSupplies { get; set; }
    private readonly string _suppliesFile = "supplies.txt";

    public SupplyManager()
    {
        BuildFileIfNull(_suppliesFile,
            "detergent;true;100" + _nl +
            "soap;true;100" + _nl +
            "rags;false;100" + _nl +
            "car wax;true;100" + _nl +
            "broom;false;100" + _nl
            );
        
        ActionSupplies = new List<ActionSupply>();
        var suppliesFileContent = File.ReadAllLines(_suppliesFile);
        foreach (var line in suppliesFileContent)
        {
            string[] split = line.Split(";");

            var name = new string(split[0]);
            var amountCanChange = bool.Parse(split[1]);
            var amount = int.Parse(split[2]);
            
            ActionSupplies.Add(new ActionSupply(name, amountCanChange, amount));
        }
    }
    
    private void BuildFileIfNull(string newFile, string dummyData)
    {
        if (!File.Exists(newFile))
        {
            File.Create(newFile).Close();
            File.AppendAllText(newFile, dummyData);
        }
    }

    private void SyncSupplies()
    {
        File.Delete(_suppliesFile);
        foreach (var supply in ActionSupplies)
        {
            string supplySerialized = supply.Name + ";" +
                                      supply.AmountCanChange + ";" +
                                      supply.Amount + ";" + _nl;
            File.AppendAllText(_suppliesFile, supplySerialized);
        }
    }

    public void AddSupply(ActionSupply supply)
    {
        ActionSupplies.Add(supply);
        SyncSupplies();
    }

    public void RemoveSupply()
    {
        List<string> supplyNames = ActionSupplies.Select(yada => yada.Name).ToList();
        string userChoiceSupply = Helpers.MakeChoice(supplyNames);
        int iSupply = supplyNames.IndexOf(userChoiceSupply);
        ActionSupplies.RemoveAt(iSupply);
        SyncSupplies();
    }

    public static ActionSupply AskForSupply()
    {
        Console.Clear();
        string supply = Helpers.RequestInput("What supply to add? ");
        var canChange = AnsiConsole.Prompt(
            new TextPrompt<bool>("Is this a permanent tool or a consumable?")
                .AddChoice(false)
                .AddChoice(true)
                .WithConverter(choice => choice ? "consumable": "tool"));
        ActionSupply newSupply = new ActionSupply (supply, canChange, 100);
        return newSupply;
    }
    
    public void DisplaySupplies()
    {
        var table = new Table();
        table.AddColumn("[red]Supply[/]");
        table.AddColumn("[red]Is Consumable?[/]");
        table.AddColumn("[red]Amount[/]");
        // somehow list Action(s) this supply is associated with?
        foreach (ActionSupply supply in ActionSupplies)
        {
            table.AddRow(
                supply.Name,
                supply.AmountCanChange.ToString(),
                supply.Amount.ToString()
            );
        }
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("...Press any key...");   // renders wrong if use Wait()
        Console.ReadKey();
        Console.Clear();
    }

    public void UpdateAmount()
    {
        List<string> supplyNames = ActionSupplies.Select(yada => yada.Name).ToList();
        supplyNames.Add("Quit");
        string userChoiceSupply = Helpers.MakeChoice(supplyNames);
        if (userChoiceSupply != "Quit")
        {
            int iSupply = supplyNames.IndexOf(userChoiceSupply);
            ActionSupply oldSupply = ActionSupplies[iSupply];
            if (!oldSupply.AmountCanChange)
            {
                Console.WriteLine($"{oldSupply.Name} is a permanent tool - cannot change amount.");
                Helpers.Wait();
            }
            else
            {
                string currentAmount = oldSupply.Amount.ToString();
                int newAmount = int.Parse(Helpers.RequestInput(
                    $"{oldSupply.Name} currently is {currentAmount}% full." + _nl + 
                    "What is its new value? "));
                ActionSupplies.RemoveAt(iSupply);
                ActionSupply newSupply = new ActionSupply(oldSupply.Name, oldSupply.AmountCanChange, newAmount);
                ActionSupplies.Add(newSupply);
                SyncSupplies();
            }
        }
    }
}