namespace Tasks;

public class ActionManager
{
     private static string nl = Environment.NewLine;  // save space
     public List<TaskAction> TaskActions { get; set; }
     
     string actionsFile = "actions.txt";
     
     public ActionManager()
     {
         BuildFileIfNull(actionsFile,
             "clean" + nl + "dust" + nl + "wipe" + nl + "sweep" + nl + "wash" + nl
             );

         TaskActions = new List<TaskAction>();
         var actionsFileData = File.ReadAllLines(actionsFile);

         foreach (string actionName in actionsFileData)
         {
             TaskActions.Add(new TaskAction(actionName));
         }
     }

     public void BuildFileIfNull(string newFile, string dummyData)
     {
         if (!File.Exists(newFile))
         {
             File.Create(newFile).Close();
             File.AppendAllText(newFile, dummyData);
         }
     }
     
     public void SynchActions()
     {
         File.Delete(actionsFile);
         foreach (var taskAction in TaskActions)
         {
             File.AppendAllText(actionsFile, taskAction.Name + nl);
         }
     }

     public void AddAction(TaskAction taskAction)
     {
         TaskActions.Add(taskAction);
         SynchActions();
     }

     public void RemoveAction(TaskAction taskAction)
     {
         TaskActions.Remove(taskAction);
         SynchActions();
     }
}