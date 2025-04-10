namespace Tasks;

public class AppUI
{
    private DataManager dataManager;

    public AppUI()
    {
        dataManager = new DataManager();
    }
    
    public void Show()
    {
        string entryChoice;
        DataManager dataManager = new DataManager();
        
        do
        {
            DateTime today = DateTime.Now;
            Console.WriteLine("Today is " + DateToString(today));

            List<AppTask> todayTasks = new List<AppTask>();
            
            foreach (AppTask task in dataManager.AppTasks)
            {
                if (task.SchedDate == today.Date)
                {
                    Console.WriteLine(
                        task.TaskAction + " " + task.TaskTarget + " is due today!"
                    );
                }
            }
            
            entryChoice = RequestInput("Type: tasks, targets, actions, supplies, or exit ");
            
            if (entryChoice == "supplies")
            {
                Console.WriteLine(Environment.NewLine + "Not implemented yet" + Environment.NewLine);
            } else if (entryChoice == "tasks")
            {
                string mode = RequestInput("Type: add, list, quit ");
        
                if (mode == "add")
                {
                    string addMore;

                    do
                    {
                        AppTask task = AskForTask();

                        DataWriter dataWriter = new DataWriter("tasks-current.txt");
                        dataWriter.AppendData(task);
                        
                        addMore = RequestInput("Add another? (yes or no) ");

                    } while (addMore != "no");
            
                } else if (mode == "list")
                {
                    foreach (AppTask task in dataManager.AppTasks)
                    {
                        Console.WriteLine(
                            "[" + task.TaskAction.Name + "]" + " " +
                            "[" + task.TaskTarget.Name + "]" + " scheduled for " +
                            "[" + task.SchedDate.ToString("MM/dd/yy") + "]" + ", repeats every " +
                            "[" + task.Frequency + "]" + " days."
                        );
                    }
                }
            } else if (entryChoice == "targets")
            {
                foreach (TaskTarget taskTarget in dataManager.TaskTargets)
                {
                    Console.WriteLine(taskTarget);
                }

                string targetChoice = RequestInput("add or remove or quit? ");
                if (targetChoice == "add")
                {
                    string newTarget = RequestInput("What would you like to add? ");
                    dataManager.AddTarget(new TaskTarget(newTarget));
                } else if (targetChoice == "remove")
                {
                    Console.WriteLine("Not implemented yet");
                    // Proving difficult to get to work...
                }

            } else if (entryChoice == "actions")
            {
                foreach (TaskAction taskAction in dataManager.TaskActions)
                {
                    Console.WriteLine(taskAction);
                }
                
                string actionChoice = RequestInput("add or remove or quit? ");
                if (actionChoice == "add")
                {
                    string newAction = RequestInput("What would you like to add? ");
                    dataManager.AddAction(new TaskAction(newAction));
                } else if (actionChoice == "remove")
                {
                    Console.WriteLine("Not implemented yet");
                    // As above, haven't figured this out yet
                }
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
}