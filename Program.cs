using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM_system2
{
    class ATM
    {
        static Dictionary<int, double> accountBalances = new Dictionary<int, double>() { { 1234, 1000 } };
        static Dictionary<int, List<string>> accountTransactions = new Dictionary<int, List<string>>();
        static int currentUserAccount = 0;
        static double dailyWithdrawalLimit = 500;
        static double dailyWithdrawalTotal = 0;
        static void CheckBalance()
        {
            Console.WriteLine("Your current balance is: $" + accountBalances[currentUserAccount]);
        }

        static void Deposit()
        {
            Console.Write("Enter the amount you want to deposit: ");
            double depositAmount = Convert.ToDouble(Console.ReadLine());
            accountBalances[currentUserAccount] += depositAmount;
            Console.WriteLine("Deposit successful!");
            accountTransactions[currentUserAccount].Add("Deposited: $" + depositAmount);
        }

        static void Withdraw()
        {
            Console.Write("Enter the amount you want to withdraw: ");
            double withdrawAmount = Convert.ToDouble(Console.ReadLine());

            if (withdrawAmount > accountBalances[currentUserAccount] || withdrawAmount > dailyWithdrawalLimit || withdrawAmount + dailyWithdrawalTotal > dailyWithdrawalLimit)
            {
                Console.WriteLine("Unable to process withdrawal. Please check your balance or daily limit.");
            }
            else
            {
                accountBalances[currentUserAccount] -= withdrawAmount;
                dailyWithdrawalTotal += withdrawAmount;
                Console.WriteLine("Withdrawal successful!");
                accountTransactions[currentUserAccount].Add("Withdrawn: $" + withdrawAmount);
            }
        }

        static void ViewTransactionHistory()
        {
            Console.WriteLine("Transaction history for account " + currentUserAccount + ":");
            foreach (string transaction in accountTransactions[currentUserAccount])
            {
                Console.WriteLine(transaction);
            }
        }

        static void Main()
        {
            Console.WriteLine("Welcome to the ATM System!");
            if (AuthenticateUser())
            {
                while (true)
                {
                    Console.WriteLine("\nSelect an option:");
                    Console.WriteLine("1. Check Balance");
                    Console.WriteLine("2. Deposit");
                    Console.WriteLine("3. Withdraw");
                    Console.WriteLine("4. View Transaction History");
                    Console.WriteLine("5. Exit");

                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            CheckBalance();
                            break;
                        case 2:
                            Deposit();
                            break;
                        case 3:
                            Withdraw();
                            break;
                        case 4:
                            ViewTransactionHistory();
                            break;
                        case 5:
                            Console.WriteLine("Thank you for using the ATM System!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }

        static bool AuthenticateUser()
        {
            Console.Write("Enter your account number: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your PIN: ");
            int pin = Convert.ToInt32(Console.ReadLine());

            if (accountBalances.ContainsKey(accountNumber) && pin == 1234)
            {
                currentUserAccount = accountNumber;
                Console.WriteLine("Authentication successful!");
                if (!accountTransactions.ContainsKey(accountNumber))
                {
                    accountTransactions.Add(accountNumber, new List<string>());
                }
                return true;
            }
            else
            {
                Console.WriteLine("Authentication failed!");
                return false;
            }
        }
    }
}
        