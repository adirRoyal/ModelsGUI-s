using System.Collections.Generic;
namespace ATMApp
{
    public class ATMApp
    {
        private List<UserAccountBank> userAccountList;
        private UserAccountBank selectedAccount;
        public void InitializeData()
        {
            userAccountList = new List<UserAccountBank>
            {
                new UserAccountBank{Id=1,FullName="Adir Royal", AccountNumber=123456,CardNumber=321321,CardPin=123123,AccountBalance=50000.00m,IsLocked=false},
                new UserAccountBank{Id=2,FullName="Efrat Royal", AccountNumber=456789,CardNumber=654654,CardPin=456456,AccountBalance=40000.00m,IsLocked=false},
                new UserAccountBank{Id=3,FullName="oshri Royal", AccountNumber=123555,CardNumber=987987,CardPin=789789,AccountBalance=30000.00m,IsLocked=true},
            };
        }

        static void Main(string[] args)
        {
            AppScreen.Welcome();
            long cardNumber = Validator.Convert<long>("your card number");
            Console.WriteLine($"Your card number is {cardNumber}");

            Utility.PreesEnterToContinue();
        }
    }
}



