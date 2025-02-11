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

        string? startDateInput = "";
        string? endDateInput = "";

        
        var startDateDay = "";
        var startDateMonth = "";
        var startDateYear = "";

        var endDateDay = "";
        var endDateMonth = "";
        var endDateYear = "";
        //DateTime.MinValue

        bool endDateLoop = false;

        

        var endDateHour = "";
        var endDateMinute = "";

        //time difference requires datetime, so parse startInput and endInput to get difference
        DateTime parsedStartTime = new();
        DateTime parsedEndTime = new();
        bool parseStartTime;
        bool parseEndTime;

        //get the name of the log 
        string name = AnsiConsole.Ask<string>("Enter the [green]Name[/] of the log to add:");

        //get start date using prompts
        AnsiConsole.Markup("Select the date and time of the start of the log");
        Console.WriteLine();
        startDateYear = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select the start [green]year[/]")
            .PageSize(5)
            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
            .AddChoices(new[] {
                            "2025", "2026","2027"
            }));

        startDateMonth = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select the start [green]Month[/]")
            .PageSize(5)
            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12"
            }));

        if (startDateMonth == "2")
        {
            startDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the start [green]day[/]")
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

        if (startDateMonth == "4" || startDateMonth == "6" || startDateMonth == "9" || startDateMonth == "11")
        {
            startDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the start [green]day[/]")
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
        if (startDateMonth == "1" || startDateMonth == "3" || startDateMonth == "5" || startDateMonth == "7" || startDateMonth == "8" || startDateMonth == "10" || startDateMonth == "12")
        {
            startDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the start [green]day[/]")
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

        string startDateDMY = $"{startDateDay}/{startDateMonth}/{startDateYear}";

        var startDateHour = "";
        var startDateMinute = "";

        startDateHour = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the start ime in [green]hours[/]")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                .AddChoices(new[] {
                            "0","1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24"
                }));

        startDateMinute = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the start time in [green]minutes[/]")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                .AddChoices(new[] {
                            "00","05", "10", "15",
                            "20", "25", "30",
                            "35", "40", "45",
                            "50","55", "60"
                }));


        startDateInput = $"{startDateDMY} {startDateHour}:{startDateMinute}";
        parseStartTime = DateTime.TryParse(startDateInput, out parsedStartTime);

        do {

        //Get end date using prompts
        endDateYear = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select the end [orange3]year[/]")
            .PageSize(5)
            .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
            .AddChoices(new[] {
                            "2025", "2026","2027"
            }));

        endDateMonth = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select the end [orange3]Month[/]")
            .PageSize(5)
            .AddChoices(new[] {
                            "1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12"
            }));

        if (endDateMonth == "2")
        {
            endDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the end [orange3]day[/]")
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

        if (endDateMonth == "4" || endDateMonth == "6" || endDateMonth == "9" || endDateMonth == "11")
        {
            endDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the end [orange3]day[/]")
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
        if (endDateMonth == "1" || endDateMonth == "3" || endDateMonth == "5" || endDateMonth == "7" || endDateMonth == "8" || endDateMonth == "10" || endDateMonth == "12")
        {
            endDateDay = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the end [orange3]day[/]")
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



        endDateHour = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the end time in [orange3]hours[/]")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                .AddChoices(new[] {
                            "0","1", "2", "3",
                            "4", "5", "6",
                            "7", "8", "9",
                            "10","11", "12",
                            "13","14","15",
                            "16","17","18",
                            "19","20","21",
                            "22","23","24"
                }));

        endDateMinute = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select the end time in [orange3]minutes[/]")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to show more answers)[/]")
                .AddChoices(new[] {
                            "00","05", "10", "15",
                            "20", "25", "30",
                            "35", "40", "45",
                            "50","55", "60"
                }));

        string endDateDMY = $"{endDateDay}/{endDateMonth}/{endDateYear}";
        endDateInput = $"{endDateDMY} {endDateHour}:{endDateMinute}";
        parseEndTime = DateTime.TryParse(endDateInput, out parsedEndTime);

        if (parsedEndTime > parsedStartTime)
        {   
            endDateLoop = true;
        }

        if (parsedEndTime < parsedStartTime)
        {
            Console.WriteLine();
            AnsiConsole.Markup("[red]The End Date for a log cannot be before the start date, please enter a valid end date[/]");
            Console.WriteLine();
        }

        } while (!endDateLoop);

        
        


        string starttime = parsedStartTime.ToString();
        string endtime = parsedEndTime.ToString();
        
        //need to query
        TimeSpan timeDifference = parsedEndTime - parsedStartTime;
        int totaltime = (int)Math.Round(timeDifference.TotalMinutes);

        var newLogItem = new LogItem(name, starttime, endtime, totaltime);

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






/*
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

        */