using Spectre.Console;
using System.Data.SqlClient;
using System.Data.SQLite;
using CodingTracker.Models;
using CodingTracker.Controllers;
using Dapper;

namespace CodingTracker.Controllers;

internal class LogDelete : IBaseController
{

    
    public void LogOperation()
    {
        //show coding logs

        //int selectedId = LogSelect.SelectLogId();
        

        Console.WriteLine("set up");
        Console.ReadKey();

        //pick habit to update by id
        //run statement to delete by id, primary key
        //show results

    }

    




}