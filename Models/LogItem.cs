namespace CodingTracker.Models;

internal class LogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int TotalTime {get; set;}



    public LogItem(int id, string name, string starttime, string endtime, int totaltime)
    {
        Id = id;
        Name = name;
        StartTime = starttime;
        EndTime = endtime;
        TotalTime = totaltime;

    }

    
}