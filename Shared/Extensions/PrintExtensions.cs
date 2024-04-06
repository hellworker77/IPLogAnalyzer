using CommandLine;
using FluentValidation.Results;

namespace Shared.Extensions;

public static class PrintExtensions
{
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor InfoColor = ConsoleColor.DarkCyan;
    public static void Print(this List<ValidationFailure> errors)
    {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ErrorColor;
        errors.ForEach(error => Console.WriteLine(error.ErrorMessage));
        Console.ForegroundColor = foregroundColor;
    }
    public static void PrintError(string message)
    {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine(message);
        Console.ForegroundColor = foregroundColor;
    }
    public static void PrintInfo(string message)
    {
        var foregroundColor = Console.ForegroundColor;
        Console.ForegroundColor = InfoColor;
        Console.WriteLine(message);
        Console.ForegroundColor = foregroundColor;
    }
}