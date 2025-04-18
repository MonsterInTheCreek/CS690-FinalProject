namespace Tasks;

public class TargetManager
{
     private static string nl = Environment.NewLine;  // save space
     public List<TaskTarget> TaskTargets { get; set; }
     
     string targetsFile = "targets.txt";
     
     public TargetManager()
     {
         BuildFileIfNull(targetsFile,
             "bathroom" + nl + "shelves" + nl + "counter" + nl + "floor" + nl + "dishes" + nl
             );
         
         TaskTargets = new List<TaskTarget>();
         var targetsFileData = File.ReadAllLines(targetsFile);

         foreach (string targetName in targetsFileData)
         {
             TaskTargets.Add(new TaskTarget(targetName));
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
     
     public void SyncTargets()
     {
         File.Delete(targetsFile);
         foreach (var taskTarget in TaskTargets)
         {
             File.AppendAllText(targetsFile, taskTarget.Name + nl);
         }
     }

     public void AddTarget(TaskTarget taskTarget)
     {
         TaskTargets.Add(taskTarget);
         SyncTargets();
     }

     public void RemoveTarget(TaskTarget taskTarget)
     {
         TaskTargets.Remove(taskTarget);
         SyncTargets();
     }

     public void DisplayTargets()
     {
         Console.Clear();
         Console.WriteLine("Targets:" + nl + "--------");
         foreach (TaskTarget taskTarget in TaskTargets)
         {
             Console.WriteLine(taskTarget);
         }
     }
}