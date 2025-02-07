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


        var newLogItem = new LogItem
        (

        name: "First",
        starttime: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        endtime: DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"),
        totaltime: 100);

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