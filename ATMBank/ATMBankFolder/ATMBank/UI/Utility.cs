public static class Utility
{
    public static void PrintMessage(string msg, bool success = true)
    {
        if(success)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.White;
        PreesEnterToContinue();

    }
    public static string GetUserInput(string prompt)
    {
        Console.WriteLine($"Enter {prompt}");
        return Console.ReadLine();

    }
     public static void PreesEnterToContinue()
    {
        Console.WriteLine("\n\n Press Enter to continue...\n");
        Console.ReadLine();
    }
}