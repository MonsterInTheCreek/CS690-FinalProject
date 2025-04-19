namespace Tasks;

public class AppUI
{
    private readonly string _nl = Environment.NewLine;  // save space
    public void Show()
    {
        string entryChoice;
        
        do
        {
            // at top, reinstantiate everything
            TargetManager targetManager = new TargetManager();
            ActionManager actionManager = new ActionManager();
            TaskManager taskManager = new TaskManager();
            
            taskManager.TodayRecap();
            
            entryChoice = Helpers.MakeChoice(new List<string>
                { "Review tasks","Review targets","Review actions","Review supplies","Exit" });
            
            if (entryChoice == "Review tasks")
            {
                Console.Clear();
                string mode = Helpers.MakeChoice(new List<string> 
                    { "Add task", "List tasks", "Remove task", "Complete task", "Quit" });
        
                if (mode == "Add task")
                {
                    AppTask task = TaskManager.AskForTask();
                    taskManager.AddTask(task);

                } else if (mode == "List tasks")
                {
                    Console.Clear();
                    taskManager.ListTasks();
                    // pause (hard-coded into ListTasks, see note, don't use Wait)

                } else if (mode == "Remove task")
                {

                    Console.WriteLine("Not ready yet");

                } else if (mode == "Complete task")
                {
                    Console.Clear();
                    taskManager.CompleteTask();
                    Helpers.Wait();
                }
                
            } else if (entryChoice == "Review targets")
            {
                targetManager.DisplayTargets();
                
                string targetChoice = Helpers.MakeChoice(new List<string> 
                    { "Add target", "Remove target", "Quit" });
                if (targetChoice == "Add target")
                {
                    string newTarget = Helpers.RequestInput("What would you like to add? ");
                    targetManager.AddTarget(new TaskTarget(newTarget));
                    
                } else if (targetChoice == "Remove target")
                {
                    targetManager.RemoveTarget();
                }
                Console.Clear();
                
            } else if (entryChoice == "Review actions")
            {
                actionManager.DisplayActions();
                
                string actionChoice = Helpers.MakeChoice(new List<string> 
                    { "Add action", "Remove action", "Quit" });
                if (actionChoice == "Add action")
                {
                    string newAction = Helpers.RequestInput("What would you like to add? ");
                    actionManager.AddAction(new TaskAction(newAction));
                    
                } else if (actionChoice == "Remove action")
                {
                    actionManager.RemoveAction();
                }
                Console.Clear();
                
            }  else if (entryChoice == "Review supplies")
            {
                Console.WriteLine(_nl + "Not implemented yet");
                Helpers.Wait();
            }
        } while (entryChoice != "Exit");
    }
}