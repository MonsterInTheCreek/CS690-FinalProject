namespace Tasks.Tests;

using Tasks;

public class ObjectTests
{
    public static TaskAction action1 = new TaskAction("wash");
    
    public static TaskTarget target1 = new TaskTarget("car");
    
    public static AppTask task1 = new AppTask(
        new TaskAction("wipe"),
        new TaskTarget("counter"),
        DateTime.Parse("04/20/25"),
        7,
        null,
        false
    );

    public static ActionSupply supply1 = new ActionSupply(
        "broom",
        false,
        100,
        false
    );

    [Fact]
    public void CanBuildTaskAction()
    {
        Assert.Equal("wash", action1.Name);
    }

    [Fact]
    public void CanBuildTaskTarget()
    {
        Assert.Equal("car", target1.Name);
    }
    
    [Fact]
    public void CanBuildAppTask()
    {
        Assert.Equal("wipe", task1.TaskAction.Name);
        Assert.Equal("counter", task1.TaskTarget.Name);
        Assert.Equal(new DateTime(2025,04,20).Date, task1.ScheduleDate.Date);
        Assert.Equal(7, task1.Frequency);
        Assert.Null(task1.PrevDate);
        Assert.False(task1.IsSupply);
    }

    [Fact]
    public void CanBuildActionSupply()
    {
        Assert.Equal("broom", supply1.Name);
        Assert.False(supply1.AmountCanChange);
        Assert.Equal(100, supply1.Amount);
        Assert.False(supply1.OnReorder);
    }
}