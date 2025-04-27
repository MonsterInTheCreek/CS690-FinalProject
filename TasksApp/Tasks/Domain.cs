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
    public TaskTarget(string name) : base(name)
    {
    }
}

public class TaskAction : TaskElement
{
    public TaskAction(string name) : base(name)
    {
    }
}

public class AppTask
{
    public TaskAction TaskAction { get; }
    public TaskTarget TaskTarget { get; }
    public DateTime ScheduleDate { get; }
    public int Frequency { get; }
    public DateTime? PrevDate { get; } // set only at instantiation, but null is valid for first use
    public bool IsSupply { get; }

    public AppTask(TaskAction taskAction, TaskTarget taskTarget, DateTime scheduleDate, int frequency,
        DateTime? prevDate,
        bool isSupply)
    {
        this.TaskAction = taskAction;
        this.TaskTarget = taskTarget;
        this.ScheduleDate = scheduleDate;
        this.Frequency = frequency;
        this.PrevDate = prevDate;
        this.IsSupply = isSupply;
    }
}

public class ActionSupply
{
    public string Name { get; }
    public bool AmountCanChange { get; }
    public int Amount { get; }
    public bool OnReorder { get; }

    public ActionSupply(string name, bool amountCanChange, int amount, bool onReorder)
    {
        this.Name = name;
        this.AmountCanChange = amountCanChange;
        this.Amount = amount;
        this.OnReorder = onReorder;
    }
}