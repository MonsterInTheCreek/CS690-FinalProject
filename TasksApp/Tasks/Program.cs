namespace Tasks;

using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Add tasks or read tasks? (add or read): ");
        string mode = Console.ReadLine();

        if (mode == "add")
        {
            string addMore;

            do
            {
                AppTask task = AskForTask();

                string prevDate;
                if (task.PrevDate.HasValue)
                {
                    prevDate = task.PrevDate.Value.ToString("MM/dd/yy");
                } else {
                    prevDate = "";
                }
                
                File.AppendAllText("tasks-log.txt",
                    task.TaskAction + ";" +
                    task.TaskTarget + ";" +
                    task.SchedDate.ToString("MM/dd/yy") + ";" +
                    task.Frequency + ";" +
                    // CompDate does not get stored
                    prevDate + ";" +
                    Environment.NewLine
                );

                Console.WriteLine("Add another? (yes or no) ");
                addMore = Console.ReadLine();

            } while (addMore == "yes");
        } else {
            // modify logic to include supplies
            string[] taskLog = File.ReadAllLines("tasks-log.txt");
            foreach (string task in taskLog)

            {
                Console.WriteLine(task);
            }
        }
    }

    public static AppTask AskForTask()
    {
        // Desperately need validations here, but lower priority than just getting to work
        TaskAction taskAction = new TaskAction(RequestInput("What task action? "));
        TaskTarget taskTarget = new TaskTarget(RequestInput("Where will you perform this? "));
        DateTime schedDate = DateTime.Parse(RequestInput("What day to schedule? (mm/dd/yy) "));
        int frequency = int.Parse(RequestInput("What frequency? (in days) "));
        DateTime? compDate;
        string taskComplete = RequestInput("Have you completed this (yes or no) ");
        if (taskComplete == "yes")
        {
            compDate = DateTime.Parse(RequestInput("When did you complete this task? (mm/dd/yy) "));
        } else {
            compDate = null;
        }
        DateTime? prevDate = null;
    
        AppTask task = new AppTask (taskAction, taskTarget, schedDate, frequency, compDate, prevDate);
        return task;
    }
    
    public static string RequestInput(string message)
    // need validations
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}