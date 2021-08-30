using Bankomat.Aggregating;
using System;

namespace Bankomat.Ui
{
    class Program
    {
        private static IAdministration _admin;
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

            var userMode = GetUserMode();

            if (userMode == 1)
            {
                var choise = GetAdminOption();

                switch (choise)
                {
                    case 1: CreateUser(); break;

                    default:
                        break;
                }
            }

            Console.WriteLine("EXIT");

            Console.ReadLine();

            Console.WriteLine("Hello World!");
        }

        private static void PrintBanner()
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

        private static byte GetUserMode()
        {
            PrintBanner();

            Console.WriteLine(@"|          --- [[ M A I N   M E N U ]] ---                                     |");
            Console.WriteLine(@"| LOGIN AS:                                                                    |");
            Console.WriteLine(@"|  1) Administrator                                                            |");
            Console.WriteLine(@"|  2) User                                                                     |");
            Console.WriteLine(@"+------------------------------------------------------------------------------+");

            var read = Console.ReadKey();

            if (read.Key == ConsoleKey.D1 || read.Key == ConsoleKey.NumPad1) return 1;
            if (read.Key == ConsoleKey.D2 || read.Key == ConsoleKey.NumPad2) return 2;

            return GetUserMode();
        }

        private static byte GetAdminOption()
        {
            PrintBanner();

            Console.WriteLine(@"|          --- [[ W E L C O M E   B O S S ]] ---                               |");
            Console.WriteLine(@"| WHAT DO YOU WANT TO DO?:                                                     |");
            Console.WriteLine(@"|  1) Create user                                                              |");
            Console.WriteLine(@"|  2) Create account for user                                                  |");
            Console.WriteLine(@"|  3) Delete existing account                                                  |");
            Console.WriteLine(@"|  4) Search for accounts                                                      |");
            Console.WriteLine(@"|  5) View reports                                                             |");
            Console.WriteLine(@"+------------------------------------------------------------------------------+");

            var read = Console.ReadKey();

            if (read.Key == ConsoleKey.D1 || read.Key == ConsoleKey.NumPad1) return 1;
            if (read.Key == ConsoleKey.D2 || read.Key == ConsoleKey.NumPad2) return 2;
            if (read.Key == ConsoleKey.D3 || read.Key == ConsoleKey.NumPad3) return 3;
            if (read.Key == ConsoleKey.D4 || read.Key == ConsoleKey.NumPad4) return 4;
            if (read.Key == ConsoleKey.D5 || read.Key == ConsoleKey.NumPad5) return 5;

            return GetAdminOption();
        }

        private static void CreateUser()
        {
            PrintBanner();

            Console.WriteLine(@"|          --- [[ C R E A T E   U S E R ]] ---                                 |");
            Console.Write(@"|> Username : ");
            var username = Console.ReadLine();
            Console.Write(@"|> PIN      : ");
            var pin = Console.ReadLine();

            try
            {
                _admin.CreateUser(username, pin);
                Console.WriteLine(@"+------------------------------------------------------------------------------+");
                Console.WriteLine(@"|  User created !                                                              |");
                Console.WriteLine(@"+------------------------------------------------------------------------------+");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"+------------------------------------------------------------------------------+");
                Console.WriteLine($"|  ERROR: {ex.Message}                                                              |");
                Console.WriteLine($"+------------------------------------------------------------------------------+");
                
            }


        }
    }
}
