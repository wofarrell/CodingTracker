using Spectre.Console;

using System.Data.SQLite;
using CodingTracker.Models;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogView : BaseController, IBaseController

{
    public void LogOperation()
    {
        string connectionString = "Data Source=CodingTracker.db";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM CodingLog;";

            var logs = connection.Query<LogItem>(selectQuery);

            Console.WriteLine("ID | Name  | StartTime           | EndTime             | TotalTime");
            Console.WriteLine("------------------------------------------------------------------------");

            foreach (var log in logs)
            {
                Console.WriteLine($"{log.Id}  | {log.Name} | {log.StartTime} | {log.EndTime} | {log.TotalTime} minutes");
            }

            connection.Close();
            Console.ReadKey();

        }

    }
}