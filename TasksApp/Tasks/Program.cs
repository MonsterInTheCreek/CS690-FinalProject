namespace Tasks;

using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Today is " + DateToString(DateTime.Now));
        string entryChoice;
        DataManager dataManager = new DataManager();
        
        do
        {
            Console.WriteLine("Type: tasks, targets, actions, supplies, or exit ");
            entryChoice = Console.ReadLine();

            if (entryChoice == "supplies")
            {
                Console.WriteLine(Environment.NewLine + "Not implemented yet" + Environment.NewLine);
            } else if (entryChoice == "tasks")
            {
                Console.Write("Type: add, read, quit ");
                string mode = Console.ReadLine();
        
                if (mode == "add")
                {
                    string addMore;

                    do
                    {
                        AppTask task = AskForTask();

                        DataWriter dataWriter = new DataWriter("tasks-current.txt");
                        dataWriter.AppendData(task);

                        Console.WriteLine("Add another? (yes or no) ");
                        addMore = Console.ReadLine();

                    } while (addMore != "no");
            
                } else if (mode == "read")
                {
                    // this isn't pretty at all yet, improve
                    string[] taskLog = File.ReadAllLines("tasks-current.txt");
                    foreach (string task in taskLog)

                    {
                        Console.WriteLine(task);
                    }
                } else
                {
                    // this is crappy logic, improve
                    Console.WriteLine(Environment.NewLine + "Quitting" + Environment.NewLine);
                }
            } else if (entryChoice == "targets")
            {
                foreach (TaskTarget taskTarget in dataManager.TaskTargets)
                {
                    Console.WriteLine(taskTarget);
                }
                    
            } else if (entryChoice == "actions")
            {
                foreach (TaskAction taskAction in dataManager.TaskActions)
                {
                    Console.WriteLine(taskAction);
                }
            }
        } while (entryChoice != "exit");
        
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

    public static string DateToString(DateTime date)
    {
        return date.ToString("dd/MM/yy");
    }
}