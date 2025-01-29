using Spectre.Console;
using CodingTracker.Models;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogInsert : IBaseController
{
   public void LogOperation()
    {
        bool answerLoop1End = false;
        bool answerLoop2End = false;
        bool answerLoop3End = false;

        string? inputHabit = "";
        string? inputQuantity = "";
        string? inputDate = "00:00:00.000";

        string? habit = "";
        int quantity = 0;
        DateTime date = new DateTime();
        string shortDate = "";

        Console.WriteLine("Enter the name of the Habit you want to track");


        do
        {
            answerLoop1End = false;
            inputHabit = Console.ReadLine();
            if (inputHabit == "")
            {
                Console.WriteLine("\nplease enter a valid answer");
            }
            else
            {
                if (inputHabit != null)
                {
                    habit = inputHabit;
                    answerLoop1End = true;
                }
            }
        } while (answerLoop1End != true);

        Console.WriteLine("Enter the quantity of the Habit you want to track");

        do
        {
            bool validEntry = false;
            answerLoop2End = false;
            inputQuantity = Console.ReadLine();
            {
                if (inputQuantity != null)
                {
                    validEntry = int.TryParse(inputQuantity, out quantity);

                    answerLoop2End = true;
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid quantity");
                }
            }
        } while (answerLoop2End != true);

        Console.WriteLine("Enter the date in format 'dd/mm/yyyy' of the Habit you want to track");

        do
        {
            bool validEntry = false;
            answerLoop3End = false;
            inputDate = Console.ReadLine();

            {
                if (inputDate != null)
                {
                    validEntry = DateTime.TryParse(inputDate, out date);
                    shortDate = date.ToShortDateString();
                    answerLoop3End = true;

                    // testing DateTime dateEntry = DateTime.Now.Date;


                }
                else
                {
                    Console.WriteLine("\nplease enter a valid date");
                }
            }
        } while (answerLoop3End != true);

        //get required information 
        //run statement to add a row into the table based on prompts 
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
            @"

                    INSERT INTO TrackedHabits (Habit, Quantity, Date)
                    VALUES (@Habit,@Quantity,@shortDate);
                ";

            //@"INSERT INTO user (name) VALUES ($name)";
            //command.Parameters.AddWithValue("$habit", habit);
            command.Parameters.Add("@Habit", SqliteType.Text).Value = habit;
            command.Parameters.Add("@Quantity", SqliteType.Integer).Value = quantity;
            command.Parameters.Add("@shortDate", SqliteType.Text).Value = shortDate;

            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();

            /*
            using (SqliteDataReader reader = command.ExecuteReader())
                {while (reader.Read())
                    {
                        // Process each row
                    }}
                    */
        }

    }
}