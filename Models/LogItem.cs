namespace CodingTracker.Models;

internal abstract class LogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }


    protected LogItem(int id, string name, string starttime, string endtime)
    {
        Id = id;
        Name = name;
        StartTime = starttime;
        EndTime = endtime;

    }

    public abstract void DisplayDetails();
}