using System.Dynamic;

namespace CodingTracker.Models;

public class LogItem
{
    public int Id {get; set;}
    public string Name { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public int TotalTime {get; set;}


    public LogItem() { }

    public LogItem(string name, string starttime, string endtime, int totaltime)
    {
        
        Name = name;
        StartTime = starttime;
        EndTime = endtime;
        TotalTime = totaltime;

    }

    
}