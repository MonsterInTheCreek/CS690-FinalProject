namespace Tasks;

using Spectre.Console;

public class TaskManager
{
    private static string nl = Environment.NewLine; // save space
    private string tasksFile = "tasks-current.txt";
    private DateTime today = DateTime.Now; 
    public List<AppTask> AppTasks { get; set; }

    public TaskManager()
    {
        AppTasks = new List<AppTask>();
        var tasksFileContent = File.ReadAllLines(tasksFile);
        foreach (var line in tasksFileContent)
        {
            string[] split = line.Split(";");

            var action = new TaskAction(split[0]);
            var target = new TaskTarget(split[1]);
            var schedDate = DateTime.Parse(split[2]);
            var frequency = int.Parse(split[3]);

            DateTime? prevDate;
            if (split[4] != "")
            {
                prevDate = DateTime.Parse(split[4]);
            }
            else
            {
                prevDate = null;
            }

            AppTasks.Add(new AppTask(action, target, schedDate, frequency, prevDate));
        }
    }

    public void TodayRecap()
    {
        Console.WriteLine("Today is " + today.ToString("MM/dd/yy"));
            
        foreach (AppTask task in AppTasks)
        {
            if (task.SchedDate <= today.Date)
            {
                Console.WriteLine(
                    task.TaskAction + " " + task.TaskTarget + " is due today!"
                );
            }
        }
    }

    public void SyncTasks()
    {
        File.Delete(tasksFile);
        foreach (AppTask task in AppTasks)
        {
            string helperPrevDate;
            if (task.PrevDate == null)
            {
                helperPrevDate = "";
            }
            else
            {
                helperPrevDate = task.PrevDate.Value.ToString("MM/dd/yy");
            }

            string taskSerialized = task.TaskAction.Name + ";" +
                                    task.TaskTarget.Name + ";" +
                                    task.SchedDate.ToString("MM/dd/yy") + ";" +
                                    task.Frequency + ";" +
                                    helperPrevDate + ";" + nl;
            File.AppendAllText(tasksFile, taskSerialized);
        }
    }

    public void AddTask(AppTask task)
    {
        AppTasks.Add(task);
        SyncTasks();
    }

    public void ListTasks()
    {
        var table = new Table();
        table.AddColumn("[red]Action[/]");
        table.AddColumn("[red]Target[/]");
        table.AddColumn("[red]Scheduled Date[/]");
        table.AddColumn("[red]Frequency (in days)[/]");
        foreach (AppTask task in AppTasks)
        {
            table.AddRow(
                task.TaskAction.Name,
                task.TaskTarget.Name,
                task.SchedDate.ToString("MM/dd/yy"),
                task.Frequency.ToString()
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("...Press any key...");   // renders wrong if use Wait()
        Console.ReadKey();
        Console.Clear();
    }

    public void CompleteTask()
    {
        int tasksCount = AppTasks.Count;
        for (int i = 0; i < tasksCount; i++)
        {
            AppTask task = AppTasks[i]; 
            Console.WriteLine(
                "[" + i + "]  " +
                task.TaskAction.Name + " " +
                task.TaskTarget.Name + " due " +
                task.SchedDate.ToString("MM/dd/yy")
            );
        }
        int iComplete = int.Parse(RequestInput("Which task did you complete?  Choose by number> "));
        AppTask oldTask = AppTasks[iComplete];
        
        AppTask newTask = new AppTask(
            oldTask.TaskAction,
            oldTask.TaskTarget,
            today.AddDays(oldTask.Frequency),  // new schedDate = today + frequency
            oldTask.Frequency,
            today);
        AppTasks[iComplete] = newTask;
        SyncTasks();
        
        Console.WriteLine("");
        Console.WriteLine($"Congrats!  You completed {oldTask.TaskAction.Name} {oldTask.TaskTarget.Name}");
        Console.WriteLine($"This task is now scheduled for {newTask.SchedDate.ToString("MM/dd/yy")}");
    }
    
    public static AppTask AskForTask()
    {
        TaskAction taskAction = new TaskAction(RequestInput("What task action? "));
        TaskTarget taskTarget = new TaskTarget(RequestInput("Where will you perform this? "));
        DateTime schedDate = DateTime.Parse(RequestInput("What day to schedule? (mm/dd/yy) "));
        int frequency = int.Parse(RequestInput("What frequency? (in days) "));
        DateTime? prevDate = null;
        AppTask task = new AppTask (taskAction, taskTarget, schedDate, frequency, prevDate);
        return task;
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
}