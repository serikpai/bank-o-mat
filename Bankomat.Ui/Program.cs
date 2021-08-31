using Bankomat.Aggregating;
using System;
using System.Linq;

namespace Bankomat.Ui
{
    class Program
    {
        private static IAdministration _admin;
        private static Screen _screen = new Screen();
        static void Main(string[] args)
        {

            _admin = Aggregator.NewAdministration();

            /**
             * 1. ADMIN
             *    1. create new user
             *    2. create new account
             *    3. delete existing account
             *    4. search for accounts
             *    5. view reports
             *    
             * 2. CUSTOMER
             *    1. withdraw cash
             *    2. deposit cash
             *    3. transfer cash
             *    4. display balance
             */

            do
            {
                var userMode = GetUserMode();

                if (userMode == 1)
                {
                    HandleAdminOperations();
                }
                else if (userMode == 255)
                {
                    break;
                }


            } while (true);


            Console.WriteLine("Hello World!");
        }

        static void HandleAdminOperations()
        {
            do
            {

                var choise = GetAdminOption();

                switch (choise)
                {
                    case 1: CreateUser(); break;
                    case 2: CreateAccount(); break;
                    case 3: DeleteAccount(); break;
                    case 4: PrintReports(); break;
                    case 255: return;
                    default: break;
                }

                Console.Read();
            } while (true);
        }



        private static byte GetUserMode()
        {
            _screen.PrintBanner();

            _screen.Headline("Main menu");

            _screen.Text("LOGIN AS:");
            _screen.Text(" 1) Administrator");
            _screen.Text(" 2) User");
            _screen.Text(" x) Exit");

            _screen.HorizontalSeparator();

            var read = Console.ReadKey();

            if (read.Key == ConsoleKey.D1 || read.Key == ConsoleKey.NumPad1) return 1;
            if (read.Key == ConsoleKey.D2 || read.Key == ConsoleKey.NumPad2) return 2;
            if (read.Key == ConsoleKey.X) return 255;

            return GetUserMode();
        }

        private static byte GetAdminOption()
        {
            _screen.PrintBanner();

            _screen.Headline("Welcome Boss");
            
            _screen.Text("WHAT DO YOU WANT TO DO?:");
            _screen.Text(" 1) Create user");
            _screen.Text(" 2) Create account for user");
            _screen.Text(" 3) Delete existing account");
            _screen.Text(" 4) Show reports");
            _screen.Text(" x) Exit");
            _screen.HorizontalSeparator();

            var read = Console.ReadKey();

            if (read.Key == ConsoleKey.D1 || read.Key == ConsoleKey.NumPad1) return 1;
            if (read.Key == ConsoleKey.D2 || read.Key == ConsoleKey.NumPad2) return 2;
            if (read.Key == ConsoleKey.D3 || read.Key == ConsoleKey.NumPad3) return 3;
            if (read.Key == ConsoleKey.D4 || read.Key == ConsoleKey.NumPad4) return 4;
            if (read.Key == ConsoleKey.X) return 255;

            return GetAdminOption();
        }

        private static void CreateUser()
        {
            _screen.PrintBanner();

            _screen.Headline("Create user");
            
            var username = _screen.ReadUserInput("Username");
            var pin = _screen.ReadUserInput("PIN");

            try
            {
                _admin.CreateUser(username, pin);
                _screen.LogInfo("User created!");
            }
            catch (Exception ex)
            {
                _screen.LogError(ex.Message);
            }
        }


        private static void CreateAccount()
        {
            _screen.PrintBanner();
            _screen.Headline("Create account");
            
            var username = _screen.ReadUserInput("Username");
            var accountName = _screen.ReadUserInput("Name");

            try
            {
                _admin.CreateAccount(username, accountName);
                _screen.LogInfo("Account created!");
            }
            catch (Exception ex)
            {
                _screen.LogError(ex.Message);
            }
        }

        private static void DeleteAccount()
        {
            _screen.PrintBanner();
        }

        private static void PrintReports()
        {
            _screen.PrintBanner();

            _screen.Headline("Account repots");
            _screen.EmpryLine();

            var users = _admin.GetAllUsers();

            Console.WriteLine($"| {"Description".PadRight(48)} | {"Balance".PadLeft(17)} | {"ID".PadLeft(5)} |");
            foreach (var user in users)
            {
                _screen.HorizontalSeparator();
                Console.WriteLine($"| ~> {user.Username.PadRight(63)} (ID: {user.Id.ToString().PadLeft(3)}) |");
                _screen.HorizontalSeparator();

                var accounts = _admin.GetAccountsForUser(user.Username);

                if (!accounts.Any())
                {
                    _screen.Text("currently no accounts");
                }

                foreach (var account in accounts)
                {
                    Console.WriteLine($"| {account.Description.PadRight(48)} | {account.GetBalance().ToString("c").PadLeft(17)} | {account.Id.ToString().PadLeft(5)} |");
                }
            }

            _screen.HorizontalSeparator();
        }





    }

    class Screen
    {
        private const int ScreenWidth = 80;

        public void PrintBanner()
        {
            Console.Clear();

            Console.WriteLine(@"+------------------------------------------------------------------------------+");
            Console.WriteLine(@"|                                          )        *                          |");
            Console.WriteLine(@"|           (                      )     ( /(      (  `               )        |");
            Console.WriteLine(@"|         ( )\      )           ( /(     )\())     )\))(       )   ( /(        |");
            Console.WriteLine(@"|         )((_)  ( /(    (      )\())   ((_)\     ((_)()\   ( /(   )\())       |");
            Console.WriteLine(@"|        ((_)_   )(_))   )\ )  ((_)\      ((_)    (_()((_)  )(_)) (_))/        |");
            Console.WriteLine(@"|         | _ ) ((_)_   _(_/(  | |(_)    / _ \    |  \/  | ((_)_  | |_         |");
            Console.WriteLine(@"|         | _ \ / _` | | ' \)) | / /    | (_) |   | |\/| | / _` | |  _|        |");
            Console.WriteLine(@"|         |___/ \__,_| |_||_|  |_\_\     \___/    |_|  |_| \__,_|  \__|        |");
            Console.WriteLine(@"|                                                                              |");
            Console.WriteLine(@"+------------------------------------------------------------------------------+");
        }

        public void HorizontalSeparator()
        {
            Console.WriteLine($"+{"".PadRight(ScreenWidth - 2, '-')}+");
        }
        public void EmpryLine()
        {
            Console.WriteLine($"+{"".PadRight(ScreenWidth - 2)}+");
        }
        public void Headline(string title)
        {
            var foo = (ScreenWidth - title.Length - 13) / 2;

            var leftSpace = "".PadLeft(foo);
            var rightSpace = "".PadRight(foo);

            Console.WriteLine($"|{leftSpace} --[[ {title.ToUpper()} ]]-- {rightSpace}|");
        }

        public void Text(string text)
        {
            Console.WriteLine($"| {text.PadRight(ScreenWidth - 4)} |");
        }

        public void LogInfo(string message)
        {
            HorizontalSeparator();
            Text(message);
            HorizontalSeparator();
        }


        public void LogError(string message)
        {
            HorizontalSeparator();
            Text("ERROR: " + message);
            HorizontalSeparator();
        }


        public string ReadUserInput(string what)
        {
            Console.Write($"|> {what.PadRight(10)} : ");
            return Console.ReadLine();
        }
    }
}
