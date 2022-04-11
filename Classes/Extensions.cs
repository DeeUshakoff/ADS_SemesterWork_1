namespace SemesterWork.Extensions;

internal static class Extensions
{
    public static void Print(this object obj, ConsoleColor color = ConsoleColor.White)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(obj.ToString());
        Console.ForegroundColor = prevColor;
    }
}

