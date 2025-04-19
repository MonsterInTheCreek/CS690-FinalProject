namespace Tasks;

public class TargetManager
{
     private readonly string _nl = Environment.NewLine;  // save space
     public List<TaskTarget> TaskTargets { get; set; }
     
     private readonly string _targetsFile = "targets.txt";
     
     public TargetManager()
     {
         BuildFileIfNull(_targetsFile,
             "bathroom" + _nl + "shelves" + _nl + "counter" + _nl + "floor" + _nl + "dishes" + _nl
             );
         
         TaskTargets = new List<TaskTarget>();
         var targetsFileData = File.ReadAllLines(_targetsFile);

         foreach (string targetName in targetsFileData)
         {
             TaskTargets.Add(new TaskTarget(targetName));
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
     
     private void SyncTargets()
     {
         File.Delete(_targetsFile);
         foreach (var taskTarget in TaskTargets)
         {
             File.AppendAllText(_targetsFile, taskTarget.Name + _nl);
         }
     }

     public void AddTarget(TaskTarget taskTarget)
     {
         TaskTargets.Add(taskTarget);
         SyncTargets();
     }

     public void RemoveTarget()
     {
         List<String> targetNames = TaskTargets.Select(yada => yada.Name).ToList();
         string userChoiceTarget = Helpers.MakeChoice(targetNames);
         int iTarget = targetNames.IndexOf(userChoiceTarget);
         TaskTargets.RemoveAt(iTarget);
         SyncTargets();
     }

     public void DisplayTargets()
     {
         Console.Clear();
         Console.WriteLine("Targets:" + _nl + "--------");
         foreach (TaskTarget taskTarget in TaskTargets)
         {
             Console.WriteLine(taskTarget);
         }
     }
}