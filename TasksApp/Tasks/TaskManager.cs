namespace Tasks;

public class TaskManager
{
    private static string nl = Environment.NewLine; // save space
    public List<AppTask> AppTasks { get; set; }

    public TaskManager()
    {
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

    public void TodayRecap()
    {
        string today = DateTime.Now.ToString("MM/dd/yy");
        Console.WriteLine("Today is " + today);
            
        foreach (AppTask task in AppTasks)
        {
            if (task.SchedDate <= DateTime.Parse(today).Date)
            {
                Console.WriteLine(
                    task.TaskAction + " " + task.TaskTarget + " is due today!"
                );
            }
        }
    }
}