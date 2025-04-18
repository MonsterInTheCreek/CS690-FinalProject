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

            DateTime today = DateTime.Now;  // this is temporary, get rid of it after porting
            taskManager.TodayRecap();
            
            entryChoice = MakeChoice(new List<string>
            {
                "Complete task","Review tasks","Review targets","Review actions","Review supplies","Exit"
            });
            
            if (entryChoice == "Complete task")
            {
                Console.Clear();
                int tasksCount = taskManager.AppTasks.Count;
                for (int i = 0; i < tasksCount; i++)
                {
                    AppTask task = taskManager.AppTasks[i]; 
                    Console.WriteLine(
                        "[" + i + "]  " +
                        task.TaskAction.Name + " " +
                        task.TaskTarget.Name + " due " +
                        task.SchedDate.ToString("MM/dd/yy")
                    );
                }

                string iComplete = RequestInput("Which task did you complete?  Choose by number> ");
                Console.WriteLine("Congrats!");  
                // improve by adding statement of what was completed, and when new task is next scheduled
                Wait();
                
                List<string> tasksLines = File.ReadAllLines("tasks-current.txt").ToList();
                tasksLines.RemoveAt(int.Parse(iComplete));
                File.WriteAllLines("tasks-current.txt", tasksLines);
                // Add back updated version of task with new scheduled date and previous date
                AppTask oldTask = taskManager.AppTasks[int.Parse(iComplete)];
                DataWriter dataWriter = new DataWriter("tasks-current.txt");
                AppTask updatedTask = new AppTask(
                    oldTask.TaskAction, oldTask.TaskTarget, today.AddDays(oldTask.Frequency), oldTask.Frequency, today
                );
                dataWriter.AppendData(updatedTask);
                
            } else if (entryChoice == "Review tasks")
            {
                Console.Clear();
                string mode = MakeChoice(new List<string> { "Add task", "List tasks", "Quit" });
        
                if (mode == "Add task")
                {
                    AppTask task = TaskManager.AskForTask();
                    taskManager.AddTask(task);

                } else if (mode == "List tasks")
                {
                    Console.Clear();
                    taskManager.ListTasks();
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