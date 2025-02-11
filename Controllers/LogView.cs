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

            var table = new Table();
            table.Border(TableBorder.Rounded);

            table.AddColumn("[white]Id[/]");
            table.AddColumn("[white]Name[/]");
            table.AddColumn("[white]StartTime[/]");
            table.AddColumn("[white]EndTime[/]");
            table.AddColumn("[white]TotalTime[/]");

            string selectQuery = "SELECT * FROM CodingLog;";

            var logs = connection.Query<LogItem>(selectQuery);

      
            foreach (var log in logs)
            {
                table.AddRow(
                $"[yellow]{log.Id}[/]",
                $"[cyan]{log.Name}[/]",
                $"[cyan]{log.StartTime}[/]",
                $"[green]{log.EndTime}[/]",
                $"[blue]{log.TotalTime}[/]"
                );

            }

            AnsiConsole.Write(table);

            connection.Close();


        }

    }
}