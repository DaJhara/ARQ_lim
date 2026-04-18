using Application.Interfaces;
using System;

namespace Infrastructure.Logging;

public class Logger: ILogger
{
    public bool Enabled { get; set; } = true;

    public void Log(string message)
    {
        if (!Enabled) return;
        Console.WriteLine("[LOG] " + DateTime.Now + " - " + message);
    }

    public void Try(Action a)
    {
        try { 
            a(); 
        } catch (Exception ex) { 
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
