using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingTracker;

internal class UserInterface
{
    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Coding Tracker v1.0");


            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .Title("Make a selection below using the menu.")
                .AddChoices(Enum.GetValues<MenuAction>()));


            switch (actionChoice)
            {
                case MenuAction.ViewLogs:
                    ViewLogs();
                    break;
                case MenuAction.AddLog:
                    AddLog();
                    break;
                case MenuAction.DeleteLog:
                    DeleteLog();
                    break;
                case MenuAction.UpdateLog:
                    UpdateLog();
                    break;
            }


        }
    }

    private void ViewLogs()
    {

    }


    private void AddLog()
    {

    }

    private void DeleteLog()
    {

    }

    private void UpdateLog()
    {

    }



}