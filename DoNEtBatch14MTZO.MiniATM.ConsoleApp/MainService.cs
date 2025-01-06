using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DoNEtBatch14MTZO.MiniATM.ConsoleApp
{
    public class MainService
    {
        
        private readonly MiniATMRefitService _minATMRefitService;
        public MainService()
        {
            _minATMRefitService = new MiniATMRefitService();
            
        }


       //public static void CreateInitialAccount()
       // {

       // }
        public async Task RunAsync()
        {
            //CreateAccount();
            Console.WriteLine("Mini ATM Console");
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Withdraw Cash");
                Console.WriteLine("4. Deposit Cash");
                Console.WriteLine("5. Blance ");
                Console.WriteLine("5. Exit ");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        await CreateAccount();
                        break;

                    case "2":
                        await Login();
                        break;

                    case "3":
                        await Withdraw();
                        break;

                    case "4":
                        await Deposit();
                        break;

                    case "5":
                        await Blance();
                        break;

                    case "6":
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
        private async Task CreateAccount()
        {

            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter PIN (4 digits): ");
            int pin = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter initial balance: ");
            decimal balance = decimal.Parse(Console.ReadLine());

            //var response = await _atmApi.CreateAccount(new UserAccount
            //{
            //    Name = name,
            //    CardNumber = cardNumber,
            //    Pin = pin,
            //    Balance = balance
            //});

            UserAcountModel userAcountModel = new UserAcountModel()
            { 
                Name = name,
                Pin = pin,
                Balance = balance,
                CardNumber = cardNumber
                
            };


            var response = await _minATMRefitService.Register(userAcountModel);

            Console.WriteLine(response.IsSuccess ? "Create Acount Successful":$"Create Acount Failed : {response.Message}");
        }

        private async Task Login()
        {
           
            Console.Write("Enter card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter PIN: ");
            int pin = Convert.ToInt32(Console.ReadLine());
            var response = await _minATMRefitService.Login(cardNumber, pin);

            Console.WriteLine(response.IsSuccess ? "Login Successful" : "Login Failed");

            //if (response == null)
            //{
            //   return Console.WriteLine("Login Successful");
            //}
            //else
            //{

            //    Console.WriteLine("Invalid card number or PIN. Please try again.");
            //}
            ////Console.WriteLine(response.IsSuccess ? "Login successful! Welcome": "Login failed !!.");
        }

        private async Task Withdraw()
        {
            Console.Write("Enter card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter amount to withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var response =await _minATMRefitService.Withdraw(cardNumber,amount);

            Console.WriteLine(response.IsSuccess ? $"{amount} has been dispensed.":"Failed to deposit cash."  );
        }
        private async Task Deposit()
        {
            Console.Write("Enter card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter amount to deposit: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            var response = await _minATMRefitService.Deposit(cardNumber, amount);

            Console.WriteLine(response.IsSuccess ? $"{amount} has been dispensed." : "Failed to deposit cash.");
        }

        private async Task Blance()
        {
            Console.Write("Enter card number : ");
            string cardNumber = Console.ReadLine();

            var response = await _minATMRefitService.Balance(cardNumber);
            if(response != null )
            {
                Console.WriteLine($" Your balance is {response}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve balance.");
            }
            //Console.WriteLine(response.IsSuccess ? $"{response} blance number {cardNumber}" : "Error");

        }

    }
}
