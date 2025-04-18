namespace Tasks;

//using System.IO;

public class TargetActionManager
{
    private static string nl = Environment.NewLine;  // save space
    public List<TaskTarget> TaskTargets { get; set; }
    public List<TaskAction> TaskActions { get; set; }
    
    string targetsFile = "targets.txt";
    string actionsFile = "actions.txt";
    
    public TargetActionManager()
    {
        BuildFileIfNull(targetsFile,
            "bathroom" + nl + "shelves" + nl + "counter" + nl + "floor" + nl + "dishes" + nl
            );
        
        BuildFileIfNull(actionsFile,
            "clean" + nl + "dust" + nl + "wipe" + nl + "sweep" + nl + "wash" + nl
            );
        
        TaskTargets = new List<TaskTarget>();
        var targetsFileData = File.ReadAllLines(targetsFile);

        foreach (string targetName in targetsFileData)
        {
            TaskTargets.Add(new TaskTarget(targetName));
        }

        TaskActions = new List<TaskAction>();
        var actionsFileData = File.ReadAllLines(actionsFile);

        foreach (string actionName in actionsFileData)
        {
            TaskActions.Add(new TaskAction(actionName));
        }
    }

    public void BuildFileIfNull(string newFile, string dummyData)
    {
        if (!File.Exists(newFile))
        {
            File.Create(newFile).Close();
            File.AppendAllText(newFile, dummyData);
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
