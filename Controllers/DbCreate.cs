using System;
using System.Data;
using System.Dynamic;
using System.IO;
using Dapper;
using Microsoft.VisualBasic;
using CodingTracker.Controllers;


namespace CodingTracker.Controllers;

internal class DatbaseController
{

    internal void initializeDatabase()
    {
        //connection to sqlite database

        string curFile = @"C:\Users\Sandwich\OneDrive\Coding\HabitTrackerApp";

        //Console.Clear();
        //if (data source HabitTracker.db exists, then don't use intializedb method
        if (!File.Exists(curFile))
        {

            using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                //initialize db with id, Habit, Quantity, & Date
                command.CommandText =
                @"
                    CREATE TABLE TrackedHabits (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        Habit TEXT,
                        Quantity INTEGER,
                        Date TEXT
                    );
                ";
                //Date Format TEXT as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }

        else break;

    }

}
