namespace Tasks;

using Spectre.Console;

public class AppUI
{
    private DataManager dataManager;
    private static string nl = Environment.NewLine;  // save space

    public AppUI()
    {
        dataManager = new DataManager();
    }
    
    public void Show()
    {
        string entryChoice;
        
        do
        {
            DataManager dataManager = new DataManager();
            DateTime today = DateTime.Now;
            Console.WriteLine("Today is " + DateToString(today));

            List<AppTask> todayTasks = new List<AppTask>();
            
            foreach (AppTask task in dataManager.AppTasks)
            {
                if (task.SchedDate <= today.Date)
                {
                    Console.WriteLine(
                        task.TaskAction + " " + task.TaskTarget + " is due today!"
                    );
                }
            }
            
            entryChoice = MakeChoice(new List<string> {"complete","tasks","targets","actions","supplies","exit"});
            
            if (entryChoice == "supplies")
            {
                Console.WriteLine(nl + "Not implemented yet");
                Wait();
            } else if (entryChoice == "tasks")
            {
                Console.Clear();
                string mode = MakeChoice(new List<string> { "add", "list", "quit" });
        
                if (mode == "add")
                {
                    string addMore;

                    do
                    {
                        AppTask task = AskForTask();

                        DataWriter dataWriter = new DataWriter("tasks-current.txt");
                        dataWriter.AppendData(task);
                        
                        addMore = MakeChoice(new List<string> { "yes", "no" });

                    } while (addMore != "no");
            
                } else if (mode == "list")
                {
                    Console.Clear();
                    foreach (AppTask task in dataManager.AppTasks)
                    {
                        Console.WriteLine(
                            "[" + task.TaskAction.Name + "]" + " " +
                            "[" + task.TaskTarget.Name + "]" + " scheduled for " +
                            "[" + task.SchedDate.ToString("MM/dd/yy") + "]" + ", repeats every " +
                            "[" + task.Frequency + "]" + " days."
                        );
                    }
                    Wait();
                }
            } else if (entryChoice == "targets")
            {
                Console.Clear();
                Console.WriteLine("Targets:" + nl + "--------");
                foreach (TaskTarget taskTarget in dataManager.TaskTargets)
                {
                    Console.WriteLine(taskTarget);
                }
                
                string targetChoice = MakeChoice(new List<string> { "add", "remove", "quit" });
                if (targetChoice == "add")
                {
                    string newTarget = RequestInput("What would you like to add? ");
                    dataManager.AddTarget(new TaskTarget(newTarget));
                } else if (targetChoice == "remove")
                {
                    Console.WriteLine(nl + "Not implemented yet");
                    Wait();
                    // Proving difficult to get to work...
                }
                Console.Clear();
            } else if (entryChoice == "actions")
            {
                Console.Clear();
                Console.WriteLine("Actions:" + nl + "--------");
                foreach (TaskAction taskAction in dataManager.TaskActions)
                {
                    Console.WriteLine(taskAction);
                }
                
                string actionChoice = MakeChoice(new List<string> { "add", "remove", "quit" });
                if (actionChoice == "add")
                {
                    string newAction = RequestInput("What would you like to add? ");
                    dataManager.AddAction(new TaskAction(newAction));
                } else if (actionChoice == "remove")
                {
                    Console.WriteLine(nl + "Not implemented yet");
                    Wait();
                    // As above, haven't figured this out yet
                }
                Console.Clear();
            } else if (entryChoice == "complete")
            {
                Console.Clear();
                int tasksCount = dataManager.AppTasks.Count;
                for (int i = 0; i < tasksCount; i++)
                {
                    AppTask task = dataManager.AppTasks[i]; 
                    Console.WriteLine(
                        "[" + i + "]  " +
                        task.TaskAction.Name + " " +
                        task.TaskTarget.Name + " due " +
                        task.SchedDate.ToString("MM/dd/yy")
                        );
                }

                string iComplete = RequestInput("Which task did you complete?  Choose by number> ");
                Console.WriteLine("Congrats!");
                
                List<string> tasksLines = File.ReadAllLines("tasks-current.txt").ToList();
                tasksLines.RemoveAt(int.Parse(iComplete));
                File.WriteAllLines("tasks-current.txt", tasksLines);
                // Add back updated version of task with new scheduled date and previous date
                AppTask oldTask = dataManager.AppTasks[int.Parse(iComplete)];
                DataWriter dataWriter = new DataWriter("tasks-current.txt");
                AppTask updatedTask = new AppTask(
                    oldTask.TaskAction, oldTask.TaskTarget, today.AddDays(oldTask.Frequency), oldTask.Frequency, today
                    );
                dataWriter.AppendData(updatedTask);


            }
        } while (entryChoice != "exit");
        
    }
    
    public static AppTask AskForTask()
    {
        // Desperately need validations here, but lower priority than just getting to work
        TaskAction taskAction = new TaskAction(RequestInput("What task action? "));
        TaskTarget taskTarget = new TaskTarget(RequestInput("Where will you perform this? "));
        DateTime schedDate = DateTime.Parse(RequestInput("What day to schedule? (mm/dd/yy) "));
        int frequency = int.Parse(RequestInput("What frequency? (in days) "));
        DateTime? prevDate = null;
        AppTask task = new AppTask (taskAction, taskTarget, schedDate, frequency, prevDate);
        return task;
    }
    
    public static string RequestInput(string message)
    // need validations
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public static string DateToString(DateTime date)
    {
        return date.ToString("MM/dd/yy");
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