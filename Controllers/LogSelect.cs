using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlTypes;
using CodingTracker.Models;
using CodingTracker.Controllers;
using Dapper;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace CodingTracker.Controllers;

internal class LogSelect
{
    public static LogItem SelectLogId()
    {
        string? readEntry = "";
        bool validEntry = false;
        int updateChoice = 0;
        LogItem? choice = null;

        bool exitRowChoiceLoop = false;
        do
        {
            Console.WriteLine("Choose the record id of a record above to update, or write 'return'");

            readEntry = Console.ReadLine();
            if (readEntry != null)
            {
                if (readEntry == "return")
                {
                    exitRowChoiceLoop = true;
                }

                else
                {
                    validEntry = int.TryParse(readEntry, out updateChoice);
                    try
                    {
                        //using a select statement to find the record, catch the exception and loop back if it doesn't exist
                        using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                        {
                            connection.Open();

                            //create variable to use in Dapper commands
                            var selectLogQuery =
                            @"SELECT * FROM CodingLog WHERE id = @updateChoice";

                            // Dapper's QueryFirstOrDefault<T> Method fetches the first matching row and maps it to a C# object (CodingLog).
                            //Dapper then maps it to an anonymous object, pass parameters using that object
                            choice = connection.QueryFirstOrDefault<LogItem>(selectLogQuery, new { updateChoice });

                            if (choice != null)
                            {
                            
                                var table = new Table();
                                table.Border(TableBorder.Rounded);

                                table.AddColumn("[white]Id[/]");
                                table.AddColumn("[white]Name[/]");
                                table.AddColumn("[white]StartTime[/]");
                                table.AddColumn("[white]EndTime[/]");
                                table.AddColumn("[white]TotalTime[/]");

                                string selectQuery = "SELECT * FROM CodingLog WHERE id = @updateChoice;";

                                var logs = connection.Query<LogItem>(selectQuery,  new { updateChoice });


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
                                exitRowChoiceLoop = true;

                            }
                            else
                            {
                                Console.WriteLine("No row found after update.");
                            }

                             connection.Close();
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            else
            {
                Console.WriteLine("\nplease enter a valid id or write 'return");


            }

        } while (!exitRowChoiceLoop);

        return choice;
        //need to update it to bring whole log item out so we can update end time
    }

}



/*string? readEntry = "";
        bool validEntry = false;
        int updateChoice = 0;
        string selectionEntry = "";
        bool exitRowChoiceLoop = false;


        //is showing logs from userinterface method
        

        //pick coding log to update by id
        do
        {
            Console.WriteLine("Choose the record id of a record above to update, or write 'return'");

            readEntry = Console.ReadLine();
            if (readEntry != null)
            {
                selectionEntry = readEntry;
                if (selectionEntry == "return")
                {
                    exitRowChoiceLoop = true;
                }
                else
                {
                    validEntry = int.TryParse(selectionEntry, out updateChoice);

                    if (updateChoice < 1)
                    {
                        Console.WriteLine("\nPlease enter a valid id, records start at 1. \nExit Update Menu by typing 'return'");
                        
                    }

                    else
                    {
                        try
                        {
                            //using a select statement to find the record, catch the exception and loop back if it doesn't exist
                            using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                            {
                                connection.Open();

                                //create variable to use in Dapper commands
                                var selectLogQuery =
                                @"
                                SELECT *
                                FROM CodingLog 
                                WHERE id = @updateChoice
                                ";

                                // Dapper's QueryFirstOrDefault<T> Method fetches the first matching row and maps it to a C# object (CodingLog).
                                //Dapper then maps it to an anonymous object, pass parameters using that object
                                var choice = connection.QueryFirstOrDefault<LogItem>(selectLogQuery, new { updateChoice });

                                if (choice != null)
                                {
                                    Console.WriteLine($"ID: {choice.Id}, Name: {choice.Name}, Start Time: {choice.StartTime}, End Time: {choice.EndTime}, Total Time: {choice.TotalTime}");
                                    exitRowChoiceLoop = true;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("\nplease enter a valid id or write 'return");
                

            }

        } while (!exitRowChoiceLoop);

        return updateChoice;
        */