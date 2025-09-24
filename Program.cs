// Sample .NET Console Application with NLog
using System;
using NLog;

class Program
{
    static void Main()
    {
        // Initialize NLog from configuration (nlog.config will be copied to output)
        NLog.LogManager.LoadConfiguration("nlog.config");
        var logger = NLog.LogManager.GetCurrentClassLogger();
        try
        {
            logger.Info("Application starting");

            Console.WriteLine("Hello, .NET with NLog!");
            logger.Info("Hello printed to console");
        }
        catch (Exception ex)
        {
            // NLog: catch setup errors
            logger.Error(ex, "Unhandled exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
        }
    }
}
