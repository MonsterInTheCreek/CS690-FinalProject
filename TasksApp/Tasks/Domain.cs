namespace Tasks;

public class TaskTarget
{
    public string Name { get; }

    public TaskTarget(string name)
    {
        this.Name = name;
    }
}

public class TaskAction
{
    // this will eventually be associated with the ActionSupply class, but that is lower dev priority
    public string Name { get; }

    public TaskAction(string name)
    {
        this.Name = name;
    }
}

public class AppTask
{
    public TaskAction TaskAction { get; set; }
    public TaskTarget TaskTarget { get; set; }
    public DateTime SchedDate { get; set; }
    public int Frequency { get; set; }
    public DateTime? CompDate { get; }   // null at instantiation, change value using function during write to Old log 
    public DateTime? PrevDate { get; }   // set only at instantiation, but null is valid for first use

    public AppTask(TaskAction taskAction, TaskTarget taskTarget, DateTime schedDate, int frequency, DateTime? compDate, DateTime? prevDate)
    {
        this.TaskAction = taskAction;
        this.TaskTarget = taskTarget;
        this.SchedDate = schedDate;
        this.Frequency = frequency;
        this.CompDate = compDate;
        this.PrevDate = prevDate;
    }
}