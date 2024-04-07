using CommandLine;
using FluentValidation.Results;

namespace Shared.Extensions;

public static class PrintExtensions
{
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor InfoColor = ConsoleColor.DarkCyan;
    private const ConsoleColor WarningColor = ConsoleColor.DarkYellow;
    private const ConsoleColor SuccessColor = ConsoleColor.Green;
    public static void Print(this List<ValidationFailure> errors)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ErrorColor;
        errors.ForEach(error => Console.WriteLine(error.ErrorMessage));
        Console.ForegroundColor = prevColor;
    }
    
    public static void PrintError(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }
    
    public static void PrintInfo(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = InfoColor;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }

    public static void PrintWarning(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = WarningColor;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }
    
    public static void PrintSuccess(string message)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = SuccessColor;
        Console.WriteLine(message);
        Console.ForegroundColor = prevColor;
    }
}