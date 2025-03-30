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
                // need validations
                Console.Write("What task action? ");
                string taskAction = Console.ReadLine();

                Console.Write("What did you do? ");
                string taskTarget = Console.ReadLine();

                Console.Write("What day to schedule? (mm/dd/yy) ");
                string taskSchedDate = Console.ReadLine();

                Console.Write("Have you completed this? (yes or no) ");
                // improve as boolean values
                string taskComplete = Console.ReadLine();

                string taskCompDate;

                if (taskComplete == "yes")
                {
                    Console.Write("When did you complete this task? (mm/dd/yy)");
                    taskCompDate = Console.ReadLine();
                } else {
                    taskCompDate = null;
                }

                File.AppendAllText("tasks-log.txt",
                    taskAction + ";" +
                    taskTarget + ";" +
                    taskSchedDate + ";" +
                    taskCompDate + ";" +
                    Environment.NewLine
                );

                Console.WriteLine("Add another? (yes or no) ");
                addMore = Console.ReadLine();

            } while (addMore == "yes");
        } else {
            string[] taskLog = File.ReadAllLines("tasks-log.txt");
            foreach (string task in taskLog)
            {
                Console.WriteLine(task);
            }
        }
    }
}