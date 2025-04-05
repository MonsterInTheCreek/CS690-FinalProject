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
    
}