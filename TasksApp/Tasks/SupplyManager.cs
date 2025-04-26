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
            "detergent;true;100;false" + _nl +
            "soap;true;100;false" + _nl +
            "rags;false;100;false" + _nl +
            "car wax;true;100;false" + _nl +
            "broom;false;100;false" + _nl
            );
        
        ActionSupplies = new List<ActionSupply>();
        var suppliesFileContent = File.ReadAllLines(_suppliesFile);
        foreach (var line in suppliesFileContent)
        {
            string[] split = line.Split(";");

            var name = new string(split[0]);
            var amountCanChange = bool.Parse(split[1]);
            var amount = int.Parse(split[2]);
            var onReorder = bool.Parse(split[3]);
            
            ActionSupplies.Add(new ActionSupply(name, amountCanChange, amount, onReorder));
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
                                      supply.Amount + ";" +
                                      supply.OnReorder + ";" + _nl;
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
        string supply = Helpers.RequestString("What Supply would you like to add? ");
        var canChange = AnsiConsole.Prompt(
            new TextPrompt<bool>("Is this a permanent tool or a consumable?")
                .AddChoice(false)
                .AddChoice(true)
                .WithConverter(choice => choice ? "consumable": "tool"));
        ActionSupply newSupply = new ActionSupply (supply, canChange, 100, false);
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
                int newAmount = Helpers.RequestInteger(
                    $"{oldSupply.Name} currently is {currentAmount}% full." + _nl + 
                    "What is its new value? ");
                ActionSupplies.RemoveAt(iSupply);
                ActionSupply newSupply = new ActionSupply(oldSupply.Name, oldSupply.AmountCanChange, newAmount, oldSupply.OnReorder);
                ActionSupplies.Add(newSupply);
                SyncSupplies();
            }
        }
    }

    public List<string> CheckSuppliesForReorder()
    {
        List<string> reorderSupplies = new List<string>();
        for (int i = 0; i < ActionSupplies.Count; i++)
        {
            if (ActionSupplies[i].Amount <= 20 && !ActionSupplies[i].OnReorder)
            {
                // update in supplies, flag onReorder == true
                ActionSupply currentSupply = ActionSupplies[i]; 
                reorderSupplies.Add(currentSupply.Name);
                // update to flag onReorder
                ActionSupplies[i] = new ActionSupply(
                    currentSupply.Name, currentSupply.AmountCanChange, currentSupply.Amount, true
                );
                SyncSupplies();
            }
        }
        return reorderSupplies;
    }

    public void ResetSupply(string reorderSupplies)
    {
        //Console.WriteLine("first pred true");
        for (int i = 0; i < ActionSupplies.Count; i++)
        {
            if (reorderSupplies.Contains(ActionSupplies[i].Name))
            {
                //Console.WriteLine("second pred true");
                ActionSupply currentSupply = ActionSupplies[i];
                ActionSupplies[i] = new ActionSupply(
                    currentSupply.Name, currentSupply.AmountCanChange, 100, false
                );
            }
        }
        SyncSupplies();
    }
}