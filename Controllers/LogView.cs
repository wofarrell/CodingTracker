 using Spectre.Console;

 using CodingTracker.Models;
 using Dapper;

namespace CodingTracker.Controllers;

internal class LogView : IBaseController

{
    public void LogOperation()
        {
            //run statement that prints table of habits

            //connection to sqlite database
            using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                //initialize db with id, Habit, Quantity, & Date
                command.CommandText =
                @"
                        SELECT *
                        FROM TrackedHabits
                ";
                //create reader 
                command.ExecuteNonQuery();

                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n{1}: {0}", reader[0], reader.GetName(0));
                            Console.ResetColor();
                            Console.WriteLine("{3}: {0}\n{4}: {1}\n{5}: {2}\n",
                            reader[1], reader[2], reader[3],
                            reader.GetName(1), reader.GetName(2), reader.GetName(3));

                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Press any key to return to the main menu");
                Console.ReadLine();

                command.Dispose();
                connection.Close();
            }
        }
}