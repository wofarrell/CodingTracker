 using Spectre.Console;
 using CodingTracker.Models;
 using Dapper;
 
 namespace CodingTracker.Controllers;

 internal class LogUpdate : IBaseController
 {
  public void LogOperation()
        {
            //show habits
            //pick habit to update by id
            //choose which value to update
            //run statement to update specific value at row id in table

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
                int updateChoice = 0;
                string updateEntry = "";
                bool exitRowChoiceLoop = false;



                //readEntry = input to console from user
                //deleteEntry = input to parse for int
                //delete choice = int output for row id
                //valid entry is whether or not the delete entry was not null

                do
                {
                    Console.WriteLine("Choose the record id of a record below to update, or write 'return'");

                    readEntry = Console.ReadLine();
                    if (readEntry != null)
                    {
                        updateEntry = readEntry;
                        if (updateEntry == "return")
                        {
                            exitRowChoiceLoop = true;
                        }
                        else
                        {
                            validEntry = int.TryParse(updateEntry, out updateChoice);

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
                                WHERE id = @updateChoice
                                ";

                                    command.Parameters.Add("@updateChoice", SqliteType.Integer).Value = updateChoice;
                                    command.ExecuteNonQuery();

                                    try
                                    {
                                        using (var reader = command.ExecuteReader())
                                        {
                                            while (reader.Read())
                                            {
                                                int readerTest = reader.GetInt32(0);

                                                if (readerTest == updateChoice)
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

                        }
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid id or write 'return");
                        exitRowChoiceLoop = false;

                    }

                } while (exitRowChoiceLoop != true);


                string updateColumn = "";


                if (updateEntry != "return")
                {
                    //use deleteEntry as string to get into the while loop, then use deleteChoice
                    //ask what column to update, then set it to a variable
                    string updateMenu = "";

                    do
                    {

                        Console.WriteLine("Select the column by number below that needs to be updated (1-3), or type 'exit'");
                        Console.WriteLine("1. Habit \n2. Quantity \n3. Date");
                        bool validUpdateEntry = false;

                        string? inputChoice = "";
                        do
                        {
                            inputChoice = Console.ReadLine();
                            {
                                if (inputChoice != null && (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4" || inputChoice == "exit"))
                                {
                                    //if (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4")
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
                            //set column variable to 1 which is Habit
                            case "1":
                                updateColumn = "Habit";
                                updateMenu = "exit";
                                break;

                            //set column variable to 2 which is Quantity
                            case "2":
                                updateColumn = "Quantity";
                                updateMenu = "exit";
                                break;

                            //set column variable to 1 which is Date
                            case "3":
                                updateColumn = "Date";
                                updateMenu = "exit";
                                break;

                            //quit app
                            default:
                                updateMenu = "exit";
                                break;
                        }
                    } while (updateMenu != "exit");



                    bool answerLoop1End = false;
                    bool answerLoop2End = false;
                    bool answerLoop3End = false;

                    string? inputHabit = "";
                    string? inputQuantity = "";
                    string? inputDate = "00:00:00.000";

                    string? updateHabit = "";
                    int updateQuantity = 0;
                    DateTime updateDate = new DateTime();
                    string shortUpdateDate = "";


                    if (updateColumn == "Habit")
                    {
                        Console.WriteLine($"Enter the new name to update '{updateColumn}'");
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
                                    updateHabit = inputHabit;
                                    answerLoop1End = true;
                                }
                            }
                        } while (answerLoop1End != true);

                        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
                        {
                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText =
                            @"
                            UPDATE TrackedHabits SET Habit = @updateHabit WHERE id = @updateChoice
                            ";
                            //@"INSERT INTO user (name) VALUES ($name)";
                            //command.Parameters.AddWithValue("$habit", habit);

                            command.Parameters.Add("@updateHabit", SqliteType.Text).Value = updateHabit;
                            command.Parameters.Add("@updateChoice", SqliteType.Integer).Value = updateChoice;

                            command.ExecuteNonQuery();
                            command.Dispose();
                            connection.Close();
                        }
                    }

                    if (updateColumn == "Quantity")
                    {
                        Console.WriteLine($"Enter the new quantity to update '{updateColumn}'");
                        do
                        {
                            bool validUpdateEntry = false;
                            answerLoop2End = false;
                            inputQuantity = Console.ReadLine();
                            {
                                if (inputQuantity != null)
                                {
                                    validUpdateEntry = int.TryParse(inputQuantity, out updateQuantity);

                                    answerLoop2End = true;
                                }
                                else
                                {
                                    Console.WriteLine("\nplease enter a valid quantity");
                                }
                            }
                        } while (answerLoop2End != true);

                        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
                        {
                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText =
                            @"
                            UPDATE TrackedHabits SET Quantity = @updateQuantity WHERE id = @updateChoice
                            ";
                            //@"INSERT INTO user (name) VALUES ($name)";
                            //command.Parameters.AddWithValue("$habit", habit);

                            command.Parameters.Add("@updateQuantity", SqliteType.Integer).Value = updateQuantity;
                            command.Parameters.Add("@updateChoice", SqliteType.Integer).Value = updateChoice;

                            command.ExecuteNonQuery();
                            command.Dispose();
                            connection.Close();
                        }

                    }

                    if (updateColumn == "Date")
                    {
                        Console.WriteLine($"Enter the new date in format 'yyyy-mm-dd hh:mm:ss' to update '{updateColumn}'");
                        do
                        {
                            bool validUpdateEntry = false;
                            answerLoop3End = false;
                            inputDate = Console.ReadLine();
                            {
                                if (inputDate != null)
                                {
                                    validUpdateEntry = DateTime.TryParse(inputDate, out updateDate);
                                    shortUpdateDate = updateDate.ToShortDateString();
                                    answerLoop3End = true;
                                }
                                else
                                {
                                    Console.WriteLine("\nPlease enter a valid date");
                                }
                            }
                        } while (answerLoop3End != true);

                        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
                        {
                            connection.Open();
                            var command = connection.CreateCommand();
                            command.CommandText =
                            @"
                            UPDATE TrackedHabits SET Date = @shortUpdateDate WHERE id = @updateChoice
                            ";
                            //@"INSERT INTO user (name) VALUES ($name)";
                            //command.Parameters.AddWithValue("$habit", habit);

                            command.Parameters.Add("@shortUpdateDate", SqliteType.Text).Value = shortUpdateDate;
                            command.Parameters.Add("@updateChoice", SqliteType.Integer).Value = updateChoice;

                            command.ExecuteNonQuery();
                            command.Dispose();
                            connection.Close();
                        }
                    }
                }
            }
        }
 }