namespace Tasks;

using Spectre.Console;

public class AppUI
{
    private static string nl = Environment.NewLine;  // save space
    public void Show()
    {
        string entryChoice;
        
        do
        {
            TargetManager targetManager = new TargetManager();
            ActionManager actionManager = new ActionManager();
            TaskManager taskManager = new TaskManager();
            
            taskManager.TodayRecap();
            
            entryChoice = MakeChoice(new List<string>
            {
                "Review tasks","Review targets","Review actions","Review supplies","Exit"
            });
            
            if (entryChoice == "Review tasks")
            {
                Console.Clear();
                string mode = MakeChoice(new List<string> { "Add task", "List tasks", "Complete task", "Quit" });
        
                if (mode == "Add task")
                {
                    AppTask task = TaskManager.AskForTask();
                    taskManager.AddTask(task);

                } else if (mode == "List tasks")
                {
                    Console.Clear();
                    taskManager.ListTasks();
                    // pause hard-coded into ListTasks, see note, don't use Wait()
                    
                } else if (mode == "Complete task")
                {
                    Console.Clear();
                    taskManager.CompleteTask();
                    Wait();
                }
                
            } else if (entryChoice == "Review targets")
            {
                Console.Clear();
                Console.WriteLine("Targets:" + nl + "--------");
                foreach (TaskTarget taskTarget in targetManager.TaskTargets)
                {
                    Console.WriteLine(taskTarget);
                }
                
                string targetChoice = MakeChoice(new List<string> { "Add target", "Remove target", "Quit" });
                if (targetChoice == "Add target")
                {
                    string newTarget = RequestInput("What would you like to add? ");
                    targetManager.AddTarget(new TaskTarget(newTarget));
                } else if (targetChoice == "Remove target")
                {
                    Console.WriteLine(nl + "Not implemented yet");
                    Wait();
                    // Proving difficult to get to work...
                }
                Console.Clear();
                
            } else if (entryChoice == "Review actions")
            {
                Console.Clear();
                Console.WriteLine("Actions:" + nl + "--------");
                foreach (TaskAction taskAction in actionManager.TaskActions)
                {
                    Console.WriteLine(taskAction);
                }
                
                string actionChoice = MakeChoice(new List<string> { "Add action", "Remove action", "Quit" });
                if (actionChoice == "Add action")
                {
                    string newAction = RequestInput("What would you like to add? ");
                    actionManager.AddAction(new TaskAction(newAction));
                } else if (actionChoice == "Remove action")
                {
                    Console.WriteLine(nl + "Not implemented yet");
                    Wait();
                }
                Console.Clear();
                
            }  else if (entryChoice == "Review supplies")
            {
                Console.WriteLine(nl + "Not implemented yet");
                Wait();
            }
        } while (entryChoice != "Exit");
        
    }
    
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
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Make a selection:")
                .AddChoices(choices));
        return choice;
    }
}