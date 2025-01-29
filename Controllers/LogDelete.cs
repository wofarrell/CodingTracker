 using Spectre.Console;
 using CodingTracker.Models;
 using Dapper;

namespace CodingTracker.Controllers;

internal class LogDelete : IBaseController
{

public void LogOperation()
        {
            //show habits already tracked
            //pick habit to update by id
            //run statement to delete by id, primary key
            //show results



            //run statement that prints table of habits
            //connection to sqlite database

            Console.WriteLine("Here are the records in HabitTracker.db:");

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

                command.Dispose();
                connection.Close();
            }

            //Total number of rows, so that loop is maintained
            string? readEntry = "";
            bool validEntry = false;
            int deleteChoice = 0;
            string deleteEntry = "";
            bool exitRowChoiceLoop = false;

            //readEntry = input to console from user
            //deleteEntry = input to parse for int
            //delete choice = int output for row id
            //valid entry is whether or not the delete entry was not null

            do
            {
                Console.WriteLine("Choose the record id of a record below to delete, or write 'return'");

                readEntry = Console.ReadLine();
                if (readEntry != null)
                {
                    deleteEntry = readEntry;
                    if (deleteEntry == "return")
                    {
                        exitRowChoiceLoop = true;
                    }
                    else
                    {
                        validEntry = int.TryParse(deleteEntry, out deleteChoice);

                        try
                        {
                            //using a select statement to find the record, catch the exception and loop back if it doesn't exist
                            using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
                            {
                                connection.Open();
                                var command = connection.CreateCommand();

                                //initialize db with id, Habit, Quantity, & Date
                                command.CommandText =
                                @"
                                SELECT *
                                FROM TrackedHabits 
                                WHERE id = @deleteChoice
                                ";

                                command.Parameters.Add("@deleteChoice", SqliteType.Integer).Value = deleteChoice;
                                command.ExecuteNonQuery();

                                try
                                {
                                    using (var reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            int readerTest = reader.GetInt32(0);

                                            if (readerTest == deleteChoice)
                                            {
                                                exitRowChoiceLoop = true;
                                            }

                                            else
                                            {
                                                Console.WriteLine("\nplease enter a valid id or write 'return");
                                                exitRowChoiceLoop = false;
                                            }
                                        }
                                        reader.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        /*
                        if (deleteChoice <= rowTotalLoop)
                        {exitRowChoiceLoop = true;}
                        else
                        {Console.WriteLine("\nplease enter a valid id or write 'return");
                            exitRowChoiceLoop = false;}
                        */
                    }
                }
                else
                {
                    Console.WriteLine("\nplease enter a valid id or write 'return");
                    exitRowChoiceLoop = false;

                }

            } while (exitRowChoiceLoop != true);


            if (deleteEntry != "return")

            {
                //use deleteEntry as string to get into the while loop, then use deleteChoice

                using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                    DELETE from TrackedHabits where id = @deleteChoice
                    ";
                    //@"INSERT INTO user (name) VALUES ($name)";
                    //command.Parameters.AddWithValue("$habit", habit);

                    command.Parameters.Add("@deleteChoice", SqliteType.Integer).Value = deleteChoice;

                    command.ExecuteNonQuery();
                    command.Dispose();
                    connection.Close();
                }
            }
        }
}