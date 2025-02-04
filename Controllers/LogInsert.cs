using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using CodingTracker.Models;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogInsert : IBaseController
{
   public void LogOperation()
    {

        string connectionString = "Data Source=CodingTracker.db";
        using (var connection = new SQLiteConnection(connectionString))
        {
            var logItem = new LogItem
        (id: 1,
            name: "First",
            starttime: "10/10/2024",
            endtime: "10/11/2024",
            totaltime: 100 );


        string insertQuery = "INSERT INTO CodingLog (id,name,starttime,endtime,totaltime) VALUES (@id, @name, @starttime, @endtime, @totaltime);";

        connection.Query(insertQuery, logItem);


        }
        
                   
    }
}