namespace Tasks;

public class AppUI
{
    public void Show()
    {
        string entryChoice;

        do
        {
            // at top, reinstantiate everything
            TargetManager targetManager = new TargetManager();
            ActionManager actionManager = new ActionManager();
            TaskManager taskManager = new TaskManager();
            SupplyManager supplyManager = new SupplyManager();

            // check for supplies in reorder status (<= 20% remaining) --> add reorder as task
            List<string> reorderSupplies = supplyManager.CheckSuppliesForReorder();
            taskManager.AddSupplies(reorderSupplies);

            taskManager = new TaskManager(); // force reinstantiate, kinda' kludgy but resolves rare bug
            taskManager.TodayRecap();

            entryChoice = Helpers.MakeChoice(new List<string>
                { "Review tasks", "Review targets", "Review actions", "Review supplies", "Exit" });

            if (entryChoice == "Review tasks")
            {
                Console.Clear();
                string mode = Helpers.MakeChoice(new List<string>
                    { "Add task", "List tasks", "Remove task", "Complete task", "Quit" });

                if (mode == "Add task")
                {
                    AppTask task = TaskManager.AskForTask();
                    taskManager.AddTask(task);
                }
                else if (mode == "List tasks")
                {
                    Console.Clear();
                    taskManager.ListTasks();
                    // pause (hard-coded into ListTasks, see note, don't use Wait)
                }
                else if (mode == "Remove task")
                {
                    taskManager.RemoveTask();
                }
                else if (mode == "Complete task")
                {
                    Console.Clear();
                    taskManager.CompleteTask();
                    Console.Clear();
                }
            }
            else if (entryChoice == "Review targets")
            {
                targetManager.DisplayTargets();

                string targetChoice = Helpers.MakeChoice(new List<string>
                    { "Add target", "Remove target", "Quit" });
                if (targetChoice == "Add target")
                {
                    string newTarget = Helpers.RequestString("What Target would you like to add? ");
                    targetManager.AddTarget(new TaskTarget(newTarget));
                }
                else if (targetChoice == "Remove target")
                {
                    targetManager.RemoveTarget();
                }

                Console.Clear();
            }
            else if (entryChoice == "Review actions")
            {
                actionManager.DisplayActions();

                string actionChoice = Helpers.MakeChoice(new List<string>
                    { "Add action", "Remove action", "Quit" });
                if (actionChoice == "Add action")
                {
                    string newAction = Helpers.RequestString("What Action would you like to add?");
                    actionManager.AddAction(new TaskAction(newAction));
                }
                else if (actionChoice == "Remove action")
                {
                    actionManager.RemoveAction();
                }

                Console.Clear();
            }
            else if (entryChoice == "Review supplies")
            {
                supplyManager.DisplaySupplies();

                string supplyChoice = Helpers.MakeChoice(new List<string>
                    { "Add supply", "Remove supply", "Update amount", "Quit" });
                if (supplyChoice == "Add supply")
                {
                    ActionSupply supply = SupplyManager.AskForSupply();
                    supplyManager.AddSupply(supply);
                }
                else if (supplyChoice == "Remove supply")
                {
                    supplyManager.RemoveSupply();
                }
                else if (supplyChoice == "Update amount")
                {
                    supplyManager.UpdateAmount();
                }

                Console.Clear();
            }
        } while (entryChoice != "Exit");
    }
}