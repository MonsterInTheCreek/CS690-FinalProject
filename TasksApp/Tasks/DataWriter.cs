namespace Tasks;

using System.IO;

public class DataWriter
{
    private string _fileName;

    public DataWriter(string fileName)
    {
        this._fileName = fileName;
        if (!File.Exists(this._fileName))
        {
            File.Create(this._fileName).Close();
        }
    }

    public void AppendData(AppTask task)
    {
        string prevDate;
        if (task.PrevDate.HasValue)
        {
            prevDate = task.PrevDate.Value.ToString("MM/dd/yy");
        } else {
            prevDate = "";
        }
        
        File.AppendAllText(this._fileName,
            task.TaskAction + ";" +
            task.TaskTarget + ";" +
            task.SchedDate.ToString("MM/dd/yy") + ";" +
            task.Frequency + ";" +
            prevDate + ";" +
            Environment.NewLine
            );
    }
}