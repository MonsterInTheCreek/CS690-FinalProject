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
                Task task = askForTask();
                File.AppendAllText("tasks-log.txt",
                    task.taskAction + ";" +
                    task.taskTarget + ";" +
                    task.taskSchedDate + ";" +
                    task.taskCompDate + ";" +
                    Environment.NewLine
                );

                Console.WriteLine("Add another? (yes or no) ");
                addMore = Console.ReadLine();

            } while (addMore == "yes");
        } else {
            // modify logic to include supplies
            string[] taskLog = File.ReadAllLines("tasks-log.txt");
            foreach (string task in taskLog)
            // need to make this pretty write out
            {
                Console.WriteLine(task);
            }
        }
    }

    public static Task askForTask()
    {
        string taskAction = RequestInput("What task action? ");
        string taskTarget = RequestInput("To where? ");
        string taskSchedDate = RequestInput("What day to schedule? (mm/dd/yy) ");
        
        string taskCompDate;
        string taskComplete = RequestInput("Have you completed this (yes or no) ");
        if (taskComplete == "yes")
        {
            taskCompDate = RequestInput("When did you complete this task? (mm/dd/yy) ");
        } else {
            taskCompDate = null;
        }

        Task task = new Task (taskAction, taskTarget, taskSchedDate, taskCompDate);
        return task;
    }

    public static string RequestInput(string message)
    // need validations
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public class Task
    {
        // remember that public references in fields is a bad idea, refactor
        public string taskAction;
        public string taskTarget;
        public string taskSchedDate;
        public string taskCompDate;
        public Task (string taskAction, string taskTarget, string taskSchedDate, string taskCompDate)
        {
            this.taskAction = taskAction;
            this.taskTarget = taskTarget;
            this.taskSchedDate = taskSchedDate;
            this.taskCompDate = taskCompDate;
        }
    }

}