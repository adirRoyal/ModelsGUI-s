using BankATM;


List<CardHolder> cardHolders = new List<CardHolder>();
cardHolders.Add(new CardHolder("112233445566", 1234, "adir", "royal", 450.46));
cardHolders.Add(new CardHolder("665544332211", 4321, "efrat", "royal", 200.32));
cardHolders.Add(new CardHolder("741258963", 4563, "oshri", "royal", 600.24));
cardHolders.Add(new CardHolder("11223344", 1234, "maayan", "royal", 850.46));

// prompt user 
Console.WriteLine("Welcome to SimpleATM");
Console.WriteLine("Please insert your debit card: ");
String debitCardNum = "";
CardHolder currentUser;

while (true)
{
    try
    {
        debitCardNum = Console.ReadLine();
        //check against our db
        currentUser = cardHolders.FirstOrDefault(a => a.getNum() == debitCardNum);
        if (currentUser != null) { break; }
        else { Console.WriteLine("Card not recognized. Please try again"); }

    }
    catch { Console.WriteLine("Card not recognized. Please try again"); }
}

Console.WriteLine("Please enter your pin: ");
int userPin = 0;

while (true)
{
    try
    {
        userPin = int.Parse(Console.ReadLine());
        //check against our db
        if (currentUser.getPin() == userPin) { break; }
        else { Console.WriteLine("Incorrect pin. Please try again"); }

    }
    catch { Console.WriteLine("Incorrect pin. Please try again"); }
}

Console.WriteLine("Welcome " + currentUser.getFirstName() + " " + currentUser.getLastName() + " :) ");
int option = 0;
do
{
    
    OptionsATM.printOptions();
    
    try
    {
        option = int.Parse(Console.ReadLine());
    }
    catch { }
    if (option == 1) { OptionsATM.deposit(currentUser); }
    else if (option == 2) { OptionsATM.withdraw(currentUser); }
    else if (option == 3) { OptionsATM.balance(currentUser); }
    else if (option == 4) { break; }
    else { option = 0; }

}
while (option != 4);
Console.WriteLine("Thank you! Have a nice day :) ");