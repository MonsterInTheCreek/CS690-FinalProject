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
        table.AddColumn("[red]Days Since Previous[/]");
        foreach (AppTask task in AppTasks)
        {
            string sinceDays;
            if (task.PrevDate == null)
            {
                sinceDays = "N/A";
            }
            else
            {
                sinceDays = (_today - task.PrevDate.Value).Days.ToString();
            }
            
            table.AddRow(
                task.TaskAction.Name,
                task.TaskTarget.Name,
                task.ScheduleDate.ToString("MM/dd/yy"),
                task.Frequency.ToString(),
                sinceDays
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine("...Press any key...");   // renders wrong if use Wait()
        Console.ReadKey();
        Console.Clear();
    }

    public void CompleteTask()
    {
        int iComplete = ChooseTask();
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

    public void RemoveTask()
    {
        int iRemove = ChooseTask();
        AppTasks.RemoveAt(iRemove);
        SyncTasks();
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

    public int ChooseTask()
    {
        List<string> mergedTasks = new List<string>();
        // enumerate the list and return the index number
        for (int i = 0; i < AppTasks.Count; i++)
        {
            mergedTasks.Add($"{i}) " + 
            $"{AppTasks[i].TaskAction.Name} " + 
            $"{AppTasks[i].TaskTarget.Name} due " +
            $"{AppTasks[i].ScheduleDate.ToString("MM/dd/yy")}"
            );
        }
        string userTaskChoice = Helpers.MakeChoice(mergedTasks);
        string index = userTaskChoice.Split(")")[0];
        return int.Parse(index);
    }
}