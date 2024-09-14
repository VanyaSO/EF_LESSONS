namespace lesson_final.Data;

public class MessageHelper
{
    public static void Print(string text, ConsoleColor textColor = ConsoleColor.White)
    {
        Console.ForegroundColor = textColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void PrintError(string text)
    {
        Print(text, ConsoleColor.Red);
    }

    public static void PrintSuccess(string text)
    {
        Print(text, ConsoleColor.Green);
    }
}