// See https://aka.ms/new-console-template for more information


//requirements
/*
1. enter and log coding time in db
2. date and occurence
3. create sqlite db at start if needed
4. create table in db
5. The users should be able to insert, delete, update and view their logged time.

To show the data on the console, you should use the "Spectre.Console" library.

You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)

You should tell the user the specific format you want the date and time to be logged and not allow any other format.

You'll need to create a configuration file that you'll contain your database path and connection strings.

You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration

The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.

The user should be able to input the start and end times manually.
*/



using CodingTracker.Controllers;
using CodingTracker;

namespace Main;

class Program
{
    //int rowTotal = 0;
    static void Main(string[] args)
    {
        //create obj of create database class then call it to see if db is created
        DatbaseController dbCreate = new();
        dbCreate.initializeDatabase();
        
        UserInterface userInterface = new();
        userInterface.MainMenu();
    }
}


