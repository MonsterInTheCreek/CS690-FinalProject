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

public class AppTask
{
    public TaskAction TaskAction { get; set; }
    public TaskTarget TaskTarget { get; set; }
    public DateTime ScheduleDate { get; set; }
    public int Frequency { get; set; }
    public DateTime? PrevDate { get; }   // set only at instantiation, but null is valid for first use
    public bool IsSupply { get; set; }

    public AppTask(TaskAction taskAction, TaskTarget taskTarget, DateTime schedDate, int frequency, DateTime? prevDate, bool isSupply)
    {
        this.TaskAction = taskAction;
        this.TaskTarget = taskTarget;
        this.ScheduleDate = schedDate;
        this.Frequency = frequency;
        this.PrevDate = prevDate;
        this.IsSupply = isSupply;
    }
}

public class ActionSupply
{
    public string Name { get; set; }
    public bool AmountCanChange { get; set; }
    public int Amount { get; set; }
    public bool OnReorder { get; set; }

    public ActionSupply(string name, bool amountCanChange, int amount, bool onReorder)
    {
        this.Name = name;
        this.AmountCanChange = amountCanChange;
        this.Amount = amount;
        this.OnReorder = onReorder;
    }
}