using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankATM
{
    static class OptionsATM 
    {
        public static void printOptions()
        {
            Console.WriteLine("Please choose from one of the following options...");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        public static void deposit(CardHolder currentUser) 
        {
            try
            {
                Console.WriteLine("How much $$ would you like to deposit: ");
                double deposit = double.Parse(Console.ReadLine());
                if (deposit == null || deposit < 0) { Console.WriteLine("Your input Not Good Try again."); }
                else
                {
                    currentUser.setBalance(currentUser.getBalance() + deposit);
                    Console.WriteLine("Thank you for your $$. Your new balance is : " + currentUser.getBalance());
                }
            }
            catch { Console.WriteLine("Your input Not Good Try again."); }
        }
        public static void withdraw(CardHolder currentUser) 
        {
            try
            {
                Console.WriteLine("How much $$ would you like to withdraw: ");
                double withdrawal = double.Parse(Console.ReadLine());
                if (withdrawal == null || withdrawal < 0) { Console.WriteLine("Your input Not Good Try again."); }
                //check if the user has enough money
                if (currentUser.getBalance() < withdrawal)
                    Console.WriteLine("Insufficient balance :( ");
                else
                {
                    currentUser.setBalance(currentUser.getBalance() - withdrawal);
                    Console.WriteLine("You're good to go! Thank you :) ");
                }
            }
            catch { Console.WriteLine("Your input Not Good Try again."); }
         }
        public static void balance(CardHolder currentUser)
        {
            Console.WriteLine("Current balance: " + currentUser.getBalance());
        }
    }
}