using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlTypes;
using CodingTracker.Models;
using CodingTracker.Controllers;
using Dapper;
using System.Data;

namespace CodingTracker.Controllers;

internal class LogUpdate : IBaseController
{
    

    public void LogOperation()
    {

        //call log select to pick row to update
        LogItem updateChoiceId = LogSelect.SelectLogId();
        if (updateChoiceId != null)
        {
        AnsiConsole.WriteLine($"\n[yellow]1. Row Id selected:[/]{updateChoiceId.Id}\n");
        string updateColumn = "";

        if (updateChoiceId.Id > 0)
        {
            //use deleteEntry as string to get into the while loop, then use deleteChoice
            //ask what column to update, then set it to a variable
            string updateMenu = "";

            do
            {
                Console.WriteLine("Select the column by number below that needs to be updated (1-3), or type 'exit'\nEnd time will be updated if 2 or 3 is chosen.");
                Console.WriteLine("1. Name \n2. Start Time \n3. End Time\n");
                bool validUpdateEntry = false;

                string? inputChoice = "";
                do
                {
                    inputChoice = Console.ReadLine();
                    {
                        if (inputChoice != null && (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "exit"))
                        {
                            updateMenu = inputChoice;
                            validUpdateEntry = true;
                        }
                        else
                        {
                            Console.WriteLine("\nplease choose a column");
                        }
                    }
                } while (validUpdateEntry == false);

                switch (updateMenu)
                {
                    //set column variable to 1 which is Name
                    case "1":
                        updateColumn = "Name";
                        updateMenu = "exit";
                        break;

                    //set column variable to 2 which is StartTime
                    case "2":
                        updateColumn = "StartTime";
                        updateMenu = "exit";
                        break;

                    //set column variable to 1 which is EndTime
                    case "3":
                        updateColumn = "EndTime";
                        updateMenu = "exit";
                        break;

                    //quit app
                    default:
                        updateMenu = "exit";
                        break;
                }
            } while (updateMenu != "exit");


            bool answerLoopName = false;
            bool answerLoopstarttime = false;
            bool answerLoopEndTime = false;

            string? inputName = "";
            DateTime updateStartTime = new DateTime();
            DateTime updateEndTime = new DateTime();


            if (updateColumn == "Name")
            {
                Console.WriteLine($"Enter a new Name for column'{updateColumn}'");
                do
                {
                    answerLoopName = false;
                    inputName = Console.ReadLine();
                    if (inputName == "")
                    {
                        Console.WriteLine("\nplease enter a valid answer");
                    }
                    else
                    {
                        if (inputName != null)
                        {
                            string updateName = inputName;
                            try
                            {
                                using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                                {
                                    connection.Open();

                                    //Dapper does NOT support column name parameterization. So we're turning     
                                    //string Name = updateName;
                                    //create variable to use in Dapper commands
                                    var updateLogQuery =
                                    @"
                                        UPDATE CodingLog SET Name = @updateName WHERE id = @updateChoiceId
                                    ";


                                    var parameters = new
                                    {
                                        updateName = updateName,  // The new value to set
                                        updateChoiceId = updateChoiceId // The row ID to update
                                    };


                                    int rowsAffected = connection.Execute(updateLogQuery, parameters);

                                    if (rowsAffected > 0)
                                    {
                                        string selectQuery = @"
                                        SELECT * FROM CodingLog 
                                        WHERE id = @updateChoiceId";

                                        var updatedRow = connection.QuerySingleOrDefault<LogItem>(selectQuery, new { updateChoiceId });

                                        Console.WriteLine($"Updated Row: ID={updatedRow.Id}, Name={updatedRow.Name}, Start Time={updatedRow.StartTime}, End Time={updatedRow.EndTime}, Total Time={updatedRow.TotalTime} \npress any key to continue to the main menu");

                                        Console.ReadKey();
                                        answerLoopName = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nNo record found, please enter a valid ID or write 'return'");
                                        answerLoopName = false;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");

                            }
                        }
                    }
                } while (answerLoopName != true);
            }

            if (updateColumn == "StartTime")
            {

                do
                {
                    answerLoopstarttime = false;
                    var dateDay = "";
                    var dateMonth = "";
                    var dateYear = "";
                    //DateTime.MinValue
                    //AnsiConsole.WriteLine($"Enter a new [green]start time[/] of the log in format 0/00/000 00:00:00: for column '{updateColumn}'");
                    //string? startInput = Console.ReadLine();

                    dateYear = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the [green]year[/]")
                        .PageSize(5)
                        .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                        .AddChoices(new[] {
                            "2025", "2026","2027"
                        }));

                    dateMonth = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the [green]Month[/]")
                        .PageSize(5)
                        .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12"
                        }));

                    if (dateMonth == "2")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28"

                            }));
                    }

                    if (dateMonth == "4" || dateMonth == "6" || dateMonth == "9" || dateMonth == "11")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28","29","30"

                            }));
                    }
                    if (dateMonth != "4" || dateMonth != "6" || dateMonth != "9" || dateMonth != "11" || dateMonth != "2")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28","29","30","31"
                            }));
                    }

                    string dateDMY = $"{dateDay}/{dateMonth}/{dateYear}";

                    var dateHour = "";
                    var dateMinute = "";

                    dateHour = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the time in [green]hours[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24"
                            }));

                    dateMinute = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the time in [green]minutes[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "05", "10", "15",
                            "20", "25", "30",
                            "35", "40", "45",
                            "50","55", "60"
                            }));


                    string startInput = $"{dateDMY} {dateHour}:{dateMinute}";

                    if (startInput != null)
                    {
                        bool validEntry = DateTime.TryParse(startInput, out updateStartTime);
                        answerLoopstarttime = true;
                        // testing DateTime dateEntry = DateTime.Now.Date;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid date");
                    }




                    bool parseTime = DateTime.TryParse(updateChoiceId.EndTime, out updateEndTime);
                    //need to query
                    TimeSpan timeDifference = updateEndTime - updateStartTime;
                    int updateTotalTime = (int)Math.Round(timeDifference.TotalMinutes);

                    int rowChoice = updateChoiceId.Id;
                    try
                    {
                        using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                        {
                            connection.Open();

                            //Dapper does NOT support column name parameterization. So we're turning     
                            //string Name = updateName;
                            //create variable to use in Dapper commands


                            var updateLogQuery =
                            @"
                                        UPDATE CodingLog SET starttime = @updateStartTime WHERE id = @rowChoice;
                                        UPDATE CodingLog SET totaltime = @updateTotalTime WHERE id = @rowChoice;
                                    ";


                            var parameters = new
                            {
                                updateStartTime = updateStartTime,  // The new value to set
                                updateTotalTime = updateTotalTime, //total time update
                                rowChoice = rowChoice // The row ID to update

                            };

                            int rowsAffected = connection.Execute(updateLogQuery, parameters);

                            if (rowsAffected > 0)
                            {
                                var table = new Table();
                                table.Border(TableBorder.Rounded);

                                table.AddColumn("[white]Id[/]");
                                table.AddColumn("[white]Name[/]");
                                table.AddColumn("[white]StartTime[/]");
                                table.AddColumn("[white]EndTime[/]");
                                table.AddColumn("[white]TotalTime[/]");

                                string selectQuery = "SELECT * FROM CodingLog WHERE id = @rowChoice";
                                var updatedRow = connection.Query<LogItem>(selectQuery, new { rowChoice });

                                foreach (var log in updatedRow)
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
                                AnsiConsole.MarkupLine("Press Any Key to Continue.");
                                Console.ReadKey();
                                answerLoopstarttime = true;
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


                } while (answerLoopstarttime != true);
            }


            if (updateColumn == "EndTime")
            {
                do
                {
                    var dateDay = "";
                    var dateMonth = "";
                    var dateYear = "";
                    //DateTime.MinValue

                    dateYear = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the [green]year[/]")
                        .PageSize(5)
                        .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                        .AddChoices(new[] {
                            "2025", "2026","2027"
                        }));

                    dateMonth = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the [green]Month[/]")
                        .PageSize(5)
                        .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12"
                        }));

                    if (dateMonth == "2")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28"

                            }));
                    }

                    if (dateMonth == "4" || dateMonth == "6" || dateMonth == "9" || dateMonth == "11")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28","29","30"

                            }));
                    }
                    if (dateMonth == "1" || dateMonth == "3" || dateMonth == "5" || dateMonth == "7" || dateMonth == "8" || dateMonth == "10" || dateMonth == "12")
                    {
                        dateDay = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the [green]day[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24",
                            "25","26","27",
                            "28","29","30","31"
                            }));
                    }

                    string dateDMY = $"{dateDay}/{dateMonth}/{dateYear}";

                    var dateHour = "";
                    var dateMinute = "";

                    dateHour = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the time in [green]hours[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24"
                            }));

                    dateMinute = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select the time in [green]minutes[/]")
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                            .AddChoices(new[] {
                            "05", "10", "15",
                            "20", "25", "30",
                            "35", "40", "45",
                            "50","55", "60"
                            }));


                    string endInput = $"{dateDMY} {dateHour}:{dateMinute}";

                    if (endInput != null)
                    {
                        bool validEntry = DateTime.TryParse(endInput, out updateEndTime);

                        // testing DateTime dateEntry = DateTime.Now.Date;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid date");
                    }

                    bool parseTime = DateTime.TryParse(updateChoiceId.EndTime, out updateEndTime);
                    //need to query
                    TimeSpan timeDifference = updateEndTime - updateEndTime;
                    int updateTotalTime = (int)Math.Round(timeDifference.TotalMinutes);

                    int rowChoice = updateChoiceId.Id;
                    try
                    {
                        using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
                        {
                            connection.Open();

                            //Dapper does NOT support column name parameterization. So we're turning     
                            //string Name = updateName;
                            //create variable to use in Dapper commands


                            var updateLogQuery =
                            @"
                                        UPDATE CodingLog SET starttime = @updateEndTime WHERE id = @rowChoice;
                                        UPDATE CodingLog SET totaltime = @updateTotalTime WHERE id = @rowChoice;
                                    ";


                            var parameters = new
                            {
                                updateEndTime = updateEndTime,  // The new value to set
                                updateTotalTime = updateTotalTime, //total time update
                                rowChoice = rowChoice // The row ID to update

                            };

                            int rowsAffected = connection.Execute(updateLogQuery, parameters);

                          
                            if (rowsAffected > 0)
                            {
                                var table = new Table();
                                table.Border(TableBorder.Rounded);

                                table.AddColumn("[white]Id[/]");
                                table.AddColumn("[white]Name[/]");
                                table.AddColumn("[white]StartTime[/]");
                                table.AddColumn("[white]EndTime[/]");
                                table.AddColumn("[white]TotalTime[/]");

                                string selectQuery = "SELECT * FROM CodingLog WHERE id = @rowChoice";
                                var updatedRow = connection.Query<LogItem>(selectQuery, new { rowChoice });

                                foreach (var log in updatedRow)
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
                                AnsiConsole.MarkupLine("Press Any Key to Continue.");
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


                } while (answerLoopEndTime != true);
            }

        }
        }

        AnsiConsole.WriteLine("Returning to main menu");
        Thread.Sleep(1000);
    }
}





