namespace Tasks;

using Spectre.Console;

public class TaskManager
{
    private readonly string _nl = Environment.NewLine; // save space
    private readonly string _tasksFile = "tasks-current.txt";
    private readonly DateTime _today = DateTime.Now; 
    private List<AppTask> AppTasks { get; set; }

    public TaskManager()
    {
        AppTasks = new List<AppTask>();
        var tasksFileContent = File.ReadAllLines(_tasksFile);
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
        Console.WriteLine("Today is " + _today.ToString("MM/dd/yy"));
            
        foreach (AppTask task in AppTasks)
        {
            if (task.ScheduleDate <= _today.Date)
            {
                Console.WriteLine(
                    task.TaskAction + " " + task.TaskTarget + " is due today!"
                );
            }
        }
    }

    private void SyncTasks()
    {
        File.Delete(_tasksFile);
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
                                    task.ScheduleDate.ToString("MM/dd/yy") + ";" +
                                    task.Frequency + ";" +
                                    helperPrevDate + ";" + _nl;
            File.AppendAllText(_tasksFile, taskSerialized);
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
                task.ScheduleDate.ToString("MM/dd/yy"),
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
                task.ScheduleDate.ToString("MM/dd/yy")
            );
        }
        int iComplete = int.Parse(Helpers.RequestInput("Which task did you complete?  Choose by number> "));
        AppTask oldTask = AppTasks[iComplete];
        
        AppTask newTask = new AppTask(
            oldTask.TaskAction,                             // maintain current
            oldTask.TaskTarget,                             // maintain current
            _today.AddDays(oldTask.Frequency),     // new scheduleDate = today + frequency
            oldTask.Frequency,                              // maintain current
            _today);                                        // new prevDate = today
        AppTasks[iComplete] = newTask;
        SyncTasks();
        
        Console.WriteLine("");
        Console.WriteLine($"Congrats!  You completed {oldTask.TaskAction.Name} {oldTask.TaskTarget.Name}");
        Console.WriteLine($"This task is now scheduled for {newTask.ScheduleDate.ToString("MM/dd/yy")}");
    }
    
    public static AppTask AskForTask()
    {
        ActionManager actions = new ActionManager();
        TargetManager targets = new TargetManager();
        List<string> actionNames = actions.TaskActions.Select(yada => yada.Name).ToList();
        List<string> targetNames = targets.TaskTargets.Select(yada => yada.Name).ToList();
        
        Console.Clear();
        Console.WriteLine("What task action?");
        TaskAction taskAction = new TaskAction(Helpers.MakeChoice(actionNames));
        Console.Clear();
        Console.WriteLine("Where will you perform this?");
        TaskTarget taskTarget = new TaskTarget(Helpers.MakeChoice(targetNames));

        DateTime scheduleDate = DateTime.Parse(Helpers.RequestInput("What day to schedule? (mm/dd/yy) "));
        int frequency = int.Parse(Helpers.RequestInput("What frequency? (in days) "));
        DateTime? prevDate = null;
        AppTask task = new AppTask (taskAction, taskTarget, scheduleDate, frequency, prevDate);
        return task;
    }
}