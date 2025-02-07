using System;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using System.IO;
using Microsoft.VisualBasic;
using CodingTracker.Controllers;

namespace CodingTracker.Controllers;

internal class DatbaseController
{

    internal void initializeDatabase()
    {
        //connection to sqlite database

        string curFile = @"C:\Users\Sandwich\OneDrive\Coding\CodingTracker\CodingTracker.db";


        if (!File.Exists(curFile))
        {

            //connection to sqlite database
            using (var connection = new SQLiteConnection("Data Source=CodingTracker.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                //initialize db with id, Habit, Quantity, & Date
                command.CommandText =
                @"
                    CREATE TABLE CodingLog (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT,
                        starttime TEXT,
                        endtime TEXT,
                        totaltime INTEGER
                    );
                ";
                //Date Format TEXT as ISO8601 strings ("YYYY-MM-DD HH:MM:SS.SSS").
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }

        }

    }
}

        /*
        if (!File.Exists(curFile))
        {
            String str;
            SqlConnection myConn = new SqlConnection("Data Source=CodingTracker.db;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE CodingTracker ON PRIMARY " +
             "(NAME = CodingTracker_Data, " +
             "FILENAME = 'C:\\Users\\Sandwich\\OneDrive\\Coding\\CodingTrackerCodingTracker.mdf', " +
             "SIZE = 5MB, MAXSIZE = 20MB, FILEGROWTH = 10%)" +
             "LOG ON (NAME = CodingTracker_Log, " +
             "FILENAME = 'C:\\CodingTrackerLog.ldf', " +
             "SIZE = 1MB, " +
             "MAXSIZE = 5MB, " +
             "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully", "CodingTracker");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString(), "MyProgram");
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            }
        */