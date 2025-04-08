namespace Tasks;

using System.IO;

public class DataManager
{
    // Targets & Actions only at this point
    // Below has a lot of repeated code - DRY - good opportunity to abstract

    public List<TaskTarget> TaskTargets { get; set; }
    public List<TaskAction> TaskActions { get; set; }
    string targetsFile = "targets.txt";
    string actionsFile = "actions.txt";
    
    public DataManager()
    {
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
