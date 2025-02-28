// Sample .NET Console Application with Serilog
using System;
using Serilog;

class Program
{
    static void Main()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Hello, .NET with Serilog!");

        Log.CloseAndFlush();
    }
}
