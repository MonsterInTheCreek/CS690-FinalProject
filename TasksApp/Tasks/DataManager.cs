namespace Tasks;

using System.IO;

public class DataManager
{
    private static string nl = Environment.NewLine;  // save space
    public List<TaskTarget> TaskTargets { get; set; }
    public List<TaskAction> TaskActions { get; set; }
    public List<AppTask> AppTasks { get; set; } 
    
    string targetsFile = "targets.txt";
    string actionsFile = "actions.txt";
    
    public DataManager()
    {
        if (!File.Exists(targetsFile))
        {
            // create targets file with dummy data
            File.Create(targetsFile).Close();
            File.AppendAllText(targetsFile, 
                "bathroom" + nl + "shelves" + nl + "counter" + nl + "floor" + nl + "dishes" + nl
                );
        }
        
        TaskTargets = new List<TaskTarget>();
        var targetsFileData = File.ReadAllLines(targetsFile);

        foreach (string targetName in targetsFileData)
        {
            TaskTargets.Add(new TaskTarget(targetName));
        }
        
        if (!File.Exists(actionsFile))
        {
            // create actions file with dummy data
            File.Create(actionsFile).Close();
            File.AppendAllText(actionsFile,
                "clean" + nl + "dust" + nl + "wipe" + nl + "sweep" + nl + "wash" + nl
                );
        }

        TaskActions = new List<TaskAction>();
        var actionsFileData = File.ReadAllLines(actionsFile);

        foreach (string actionName in actionsFileData)
        {
            TaskActions.Add(new TaskAction(actionName));
        }
        
        AppTasks = new List<AppTask>();
        var tasksFileContent = File.ReadAllLines("tasks-current.txt");
        DateTime? prevDate;
        foreach (var line in tasksFileContent)
        {
            string[] split = line.Split(";");
            
            var action = new TaskAction(split[0]);
            var target = new TaskTarget(split[1]);
            var schedDate = DateTime.Parse(split[2]);
            var frequency = int.Parse(split[3]);            
            
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

    public void SynchTargets()
    {
        File.Delete(targetsFile);
        foreach (var taskTarget in TaskTargets)
        {
            File.AppendAllText(targetsFile, taskTarget.Name + nl);
        }
    }

    public void AddTarget(TaskTarget taskTarget)
    {
        TaskTargets.Add(taskTarget);
        SynchTargets();
    }

    public void RemoveTarget(TaskTarget taskTarget)
    {
        TaskTargets.Remove(taskTarget);
        SynchTargets();
    }
    
    public void SynchActions()
    {
        File.Delete(actionsFile);
        foreach (var taskAction in TaskActions)
        {
            File.AppendAllText(actionsFile, taskAction.Name + nl);
        }
    }

    public void AddAction(TaskAction taskAction)
    {
        TaskActions.Add(taskAction);
        SynchActions();
    }

    public void RemoveAction(TaskAction taskAction)
    {
        TaskActions.Remove(taskAction);
        SynchActions();
    }
}
