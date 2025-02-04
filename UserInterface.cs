using Spectre.Console;
using static CodingTracker.Enums;
using CodingTracker.Controllers;

namespace CodingTracker;

internal class UserInterface
{

    private readonly LogInsert _logInsert = new();
    private readonly LogDelete _logDelete = new();
    private readonly LogUpdate _logUpdate = new();
    private readonly LogView _logView = new();

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
                case MenuAction.LogInsert:
                    LogInsert();
                    break;
                case MenuAction.LogDelete:
                    LogDelete();
                    break;
                case MenuAction.LogUpdate:
                    LogUpdate();
                    break;
                case MenuAction.LogView:
                    LogView();
                    break;
            }


        }
    }

    private void LogInsert()
    {
        _logInsert.LogOperation();
    }

    private void LogDelete()
    {
        _logDelete.LogOperation();
    }

    private void LogUpdate()
    {
        _logUpdate.LogOperation();
    }

    private void LogView()
    {
        _logView.LogOperation();
    }



}