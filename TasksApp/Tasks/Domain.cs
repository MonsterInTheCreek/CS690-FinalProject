namespace Tasks;

public class TaskElement
{
    public string Name { get; }

    protected TaskElement(string name)
    {
        this.Name = name;
    }

    public override string ToString()
    {
        return this.Name;
    }
}

public class TaskTarget : TaskElement
{
    public TaskTarget(string name) : base(name) { }
}

public class TaskAction : TaskElement
{
    public TaskAction(string name) : base(name) { }
}

// ActionSupply class definition, pending, lower priority

public class AppTask
{
    public TaskAction TaskAction { get; set; }
    public TaskTarget TaskTarget { get; set; }
    public DateTime SchedDate { get; set; }
    public int Frequency { get; set; }
    public DateTime? PrevDate { get; }   // set only at instantiation, but null is valid for first use

    public AppTask(TaskAction taskAction, TaskTarget taskTarget, DateTime schedDate, int frequency, DateTime? prevDate)
    {
        this.TaskAction = taskAction;
        this.TaskTarget = taskTarget;
        this.SchedDate = schedDate;
        this.Frequency = frequency;
        this.PrevDate = prevDate;
    }
}