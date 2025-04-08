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
            entryChoice = RequestInput("Type: tasks, targets, actions, supplies, or exit ");

            if (entryChoice == "supplies")
            {
                Console.WriteLine(Environment.NewLine + "Not implemented yet" + Environment.NewLine);
            } else if (entryChoice == "tasks")
            {
                string mode = RequestInput("Type: add, read, quit ");
        
                if (mode == "add")
                {
                    string addMore;

                    do
                    {
                        AppTask task = AskForTask();

                        DataWriter dataWriter = new DataWriter("tasks-current.txt");
                        dataWriter.AppendData(task);
                        
                        addMore = RequestInput("Add another? (yes or no) ");

                    } while (addMore != "no");
            
                } else if (mode == "read")
                {
                    // this isn't pretty at all yet, improve
                    string[] taskLog = File.ReadAllLines("tasks-current.txt");
                    foreach (string task in taskLog)
                    {
                        Console.WriteLine(task);
                    }
                }
            } else if (entryChoice == "targets")
            {
                foreach (TaskTarget taskTarget in dataManager.TaskTargets)
                {
                    Console.WriteLine(taskTarget);
                }

                string targetChoice = RequestInput("add or remove or quit? ");
                if (targetChoice == "add")
                {
                    string newTarget = RequestInput("What would you like to add? ");
                    dataManager.AddTarget(new TaskTarget(newTarget));
                } else if (targetChoice == "remove")
                {
                    Console.WriteLine("Not implemented yet");
                    // Apparently removing objects is much more difficult, I've tried like 10 ways to do this...
                    // string oldTarget = RequestInput("What would you like to remove? ");
                    // TaskTarget removeTarget = new TaskTarget(oldTarget);
                    // dataManager.RemoveTarget(removeTarget);
                }

            } else if (entryChoice == "actions")
            {
                foreach (TaskAction taskAction in dataManager.TaskActions)
                {
                    Console.WriteLine(taskAction);
                }
                
                string actionChoice = RequestInput("add or remove or quit? ");
                if (actionChoice == "add")
                {
                    string newAction = RequestInput("What would you like to add? ");
                    dataManager.AddAction(new TaskAction(newAction));
                } else if (entryChoice == "remove")
                {
                    Console.WriteLine("Not implemented yet");
                    // As above, haven't figured this out yet
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