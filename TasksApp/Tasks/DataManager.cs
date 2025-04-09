namespace Tasks;

using System.IO;

public class DataManager
{
    // Targets & Actions only at this point
    // Below has a lot of repeated code - DRY - good opportunity to abstract

    public List<TaskTarget> TaskTargets { get; set; }
    public List<TaskAction> TaskActions { get; set; }
    public List<AppTask> AppTasks { get; set; } 
    
    
    string targetsFile = "targets.txt";
    string actionsFile = "actions.txt";
    
    public DataManager()
    {
        // TaskTargets class instantiation read from targets.txt
        if (!File.Exists(targetsFile))
        {
            // create targets file with dummy data
            File.Create(targetsFile).Close();
            File.AppendAllText(targetsFile,
                "bathroom" + Environment.NewLine +
                "shelves" + Environment.NewLine +
                "counter" + Environment.NewLine +
                "floor" + Environment.NewLine +
                "dishes" + Environment.NewLine
                );
        }
        
        TaskTargets = new List<TaskTarget>();
        var targetsFileData = File.ReadAllLines(targetsFile);

        foreach (string targetName in targetsFileData)
        {
            TaskTargets.Add(new TaskTarget(targetName));
        }
        
        // TaskActions class instantiation read from actions.txt
        if (!File.Exists(actionsFile))
        {
            // create actions file with dummy data
            File.Create(actionsFile).Close();
            File.AppendAllText(actionsFile,
                "clean" + Environment.NewLine +
                "dust" + Environment.NewLine +
                "wipe" + Environment.NewLine +
                "sweep" + Environment.NewLine +
                "wash" + Environment.NewLine
                );
        }

        TaskActions = new List<TaskAction>();
        var actionsFileData = File.ReadAllLines(actionsFile);

        foreach (string actionName in actionsFileData)
        {
            TaskActions.Add(new TaskAction(actionName));
        }
        
        AppTasks = new List<AppTask>();
        var tasksFileContent = File.ReadAllLines("tasks.txt");
        foreach (var line in tasksFileContent)
        {
            var splitline = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
            
            var persistentAction = splitline[0];
            var action = new TaskAction(persistentAction);
            
            var persistentTarget = splitline[1];
            var target = new TaskTarget(persistentTarget);
            
            var persistentSchedDate = splitline[2];
            var schedDate = new SchedDate(DateTime.Parse(persistentSchedDate));
            
            var persistentFrequency = splitline[3];
            var frequency = new Frequency(int.Parse(persistentFrequency));
            
            // CompDate?
            
            var persistentPrevDate = splitline[4];
            var prevDate = new PrevDate(DateTime.Parse(persistentPrevDate));
            
            // Figure out how to access these.  Do I need to instantiate above first?
            // Do I need to / want to add the CompDate?
            // Dates need to be nullable
            
            AppTasks.Add(new AppTask(action, target, schedDate, frequency, prevDate));
            
            
        }
        
    }

    public void SynchTargets()
    {
        File.Delete(targetsFile);
        foreach (var taskTarget in TaskTargets)
        {
            File.AppendAllText(targetsFile, taskTarget.Name + Environment.NewLine);
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
            File.AppendAllText(actionsFile, taskAction.Name + Environment.NewLine);
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
