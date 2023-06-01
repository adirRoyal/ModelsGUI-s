public static class AppScreen
{
    internal static void Welcome()
    {
        //Clear the console screen
        Console.Clear();
        //sets the title of the console window
        Console.Title = "My ATM Bank App      By Adir Royal";
        //sets the text color or foreground color white
        Console.BackgroundColor = ConsoleColor.White;

        //set the welcome massage
        Console.WriteLine("\n\n--------------Welcome to My ATM App---------------\n\n");
        //prompt the user to insert arm card
        Console.WriteLine("Please insert your ATM card");
        Console.WriteLine("Note: Actual ATM machine will accept and validate" +
            " a physical ATM card, read the card number and validate it.");

        Utility.PreesEnterToContinue();

    }

   
}