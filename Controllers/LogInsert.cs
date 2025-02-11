using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using CodingTracker.Models;
using CodingTracker.Controllers;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogInsert : BaseController, IBaseController
{
    public void LogOperation()
    {

        bool answerLoopstarttime = false;
        bool answerLoopendtime = false;
        DateTime starttime = new DateTime();
        DateTime endtime = new DateTime();

        string name = AnsiConsole.Ask<string>("Enter the [green]Name[/] of the log to add:");

        //start time answer loop
        do
        {
            bool validEntry = false;
            answerLoopstarttime = false;
            //DateTime.MinValue
            string startInput = AnsiConsole.Ask<string>("Enter the [green]start time[/] of the log in format 0/00/000 00:00:00:");
            {
                if (startInput != null)
                {
                    validEntry = DateTime.TryParse(startInput, out starttime);
                    answerLoopstarttime = true;
                    // testing DateTime dateEntry = DateTime.Now.Date;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid date");
                }
            }
        } while (answerLoopstarttime != true);

        //end time answwer loop
        do
        {
            bool validEntry = false;
            answerLoopendtime = false;
            //DateTime.MinValue
            string endInput = AnsiConsole.Ask<string>("Enter the [red]end time[/] of the log in format 0/00/000 00:00:00:");
            {
                if (endInput != null)
                {
                    validEntry = DateTime.TryParse(endInput, out endtime);

                    if (endtime > starttime)
                    {
                        answerLoopendtime = true;
                    }
                    else
                    {
                        Console.WriteLine("\n the end time must be after the start time");
                    }
                    // testing DateTime dateEntry = DateTime.Now.Date;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid date");
                }
            }
        } while (answerLoopendtime != true);

        TimeSpan timeDifference = endtime - starttime;
        int totaltime = (int)Math.Round(timeDifference.TotalMinutes);
        //int.TryParse(timeDifference, out totaltime);

        var newLogItem = new LogItem(name, starttime.ToString(), endtime.ToString(), totaltime);

        string connectionString = "Data Source=CodingTracker.db";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO CodingLog (name, starttime, endtime, totaltime) 
                    VALUES (@Name, @StartTime, @EndTime, @TotalTime);";

            connection.Execute(insertQuery, newLogItem);
            connection.Close();

        }


    }
}