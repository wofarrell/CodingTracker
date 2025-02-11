using System.Dynamic;

namespace CodingTracker.Models;

internal class LogItem
{
    public int Id {get; set;}
    public string? Name { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public int TotalTime {get; set;}

    //parameterless constructor for dapper. When retrieving data, Dapper instantiates objects using reflection.Without a parameterless constructor, it doesn’t know how to create LogItem from query results.
    public LogItem() { }

    public LogItem(string name, string starttime, string endtime, int totaltime)
    {
        
        Name = name;
        StartTime = starttime;
        EndTime = endtime;
        TotalTime = totaltime;

    }

    
}