namespace Tasks;

public class ActionManager
{
     private readonly string _nl = Environment.NewLine;  // save space
     private List<TaskAction> TaskActions { get; set; }
     
     private readonly string _actionsFile = "actions.txt";
     
     public ActionManager()
     {
         BuildFileIfNull(_actionsFile,
             "clean" + _nl + "dust" + _nl + "wipe" + _nl + "sweep" + _nl + "wash" + _nl
             );

         TaskActions = new List<TaskAction>();
         var actionsFileData = File.ReadAllLines(_actionsFile);

         foreach (string actionName in actionsFileData)
         {
             TaskActions.Add(new TaskAction(actionName));
         }
     }

     private void BuildFileIfNull(string newFile, string dummyData)
     {
         if (!File.Exists(newFile))
         {
             File.Create(newFile).Close();
             File.AppendAllText(newFile, dummyData);
         }
     }
     
     private void SyncActions()
     {
         File.Delete(_actionsFile);
         foreach (var taskAction in TaskActions)
         {
             File.AppendAllText(_actionsFile, taskAction.Name + _nl);
         }
     }

     public void AddAction(TaskAction taskAction)
     {
         TaskActions.Add(taskAction);
         SyncActions();
     }

     public void RemoveAction()
     {
         List<String> actionNames = TaskActions.Select(yada => yada.Name).ToList();
         string userChoiceAction = Helpers.MakeChoice(actionNames);
         int iAction = actionNames.IndexOf(userChoiceAction);
         TaskActions.RemoveAt(iAction);
         SyncActions();
     }

     public void DisplayActions()
     {
         Console.Clear();
         Console.WriteLine("Actions:" + _nl + "--------");
         foreach (TaskAction taskAction in TaskActions)
         {
             Console.WriteLine(taskAction);
         }
     }
}