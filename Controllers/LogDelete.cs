using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using CodingTracker.Models;
using CodingTracker.Controllers;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogDelete : IBaseController
{


    public void LogOperation()
    {
        //call log select to pick row to update
        LogItem updateChoice = LogSelect.SelectLogId();
        int updateChoiceId = updateChoice.Id;
        bool answerLoopEndTime = false;

        do
        {
            if (updateChoice != null)
            {

                AnsiConsole.Markup("\n1. [yellow]Row Id selected:[/]" + $"{updateChoiceId}\n");
                //string updateColumn = "";

                if (updateChoiceId > 0)
                {

                    int rowChoice = updateChoiceId;
                    try
                    {
                        using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                        {
                            connection.Open();

                            //Dapper does NOT support column name parameterization. So we're turning     
                            //string Name = updateName;
                            //create variable to use in Dapper commands

                            var deleteLogQuery =
                            @"
                                        DELETE from CodingLog WHERE id = @rowChoice;
                                    ";
                            var parameters = new
                            {
                                rowChoice = rowChoice // The row ID to update
                            };

                            int rowsAffected = connection.Execute(deleteLogQuery, parameters);

                            if (rowsAffected > 0)
                            {
                                connection.Close();
                                AnsiConsole.MarkupLine($"Record {rowChoice} deleted, press any key to continue.");
                                Console.ReadKey();
                                answerLoopEndTime = true;
                            }
                            else
                            {
                                Console.WriteLine("\nNo record found, please enter a valid ID or write 'return'");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        } while (!answerLoopEndTime);
    }
}