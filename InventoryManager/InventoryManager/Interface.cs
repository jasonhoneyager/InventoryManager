using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    public class Interface
    {
        UserDatabase database = new UserDatabase();
        Library library = new Library();
        public User activeuser = null;
        public bool logged_in;
        public bool system_active;

        public void InitializeSystem()
        {
            int status = database.CheckStatus();
            if (status == 0)
            {
                Console.WriteLine("Initial User must be designated as Administrator.");
                User user = new User();
                user.CreateNewUser(status);
                database.UpdateList(user);              
            }
            else
            {
                Console.WriteLine("System Updated.");
            }
        }

        public void DisplayUserConsole()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - View Items");
            Console.WriteLine("2 - Checkout Item");
            Console.WriteLine("3 - Return Item");
            Console.WriteLine("4 - View Checked Items");
            Console.WriteLine("5 - Admin Console");
            Console.WriteLine("6 - Logout");
            Console.WriteLine("Esc - Shut Down");
            Console.WriteLine();
            ConsoleKey entry = Console.ReadKey(true).Key;
            if (entry == ConsoleKey.NumPad1 || entry == ConsoleKey.D1)
            {
                library.ViewItems();
            }
            else if (entry == ConsoleKey.NumPad2 || entry == ConsoleKey.D2)
            {
                library.CheckoutItem(activeuser);
            }
            else if (entry == ConsoleKey.NumPad3 || entry == ConsoleKey.D3)
            {
                library.CheckItemCount(activeuser);
            }
            else if (entry == ConsoleKey.NumPad4 || entry == ConsoleKey.D4)
            {
                library.ViewChecked(activeuser);
            }
            else if (entry == ConsoleKey.NumPad5 || entry == ConsoleKey.D5)
            {
                if (activeuser.admin == false)
                {
                    Console.WriteLine("Access Denied!");
                }
                else
                {
                    DisplayAdminConsole();
                }
            }
            else if (entry == ConsoleKey.NumPad6 || entry == ConsoleKey.D6)
            {
                Logout();
            }
            else if (entry == ConsoleKey.Escape)
            {
                system_active = false;
                Logout();
            }
        }

        public void DisplayAdminConsole()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Edit Item");
            Console.WriteLine("2 - View Users");
            Console.WriteLine("3 - Create Item");
            Console.WriteLine("4 - Create User");
            Console.WriteLine("5 - User Console");
            Console.WriteLine("6 - Logout");
            Console.WriteLine("Esc - Shut Down");
            Console.WriteLine();
            ConsoleKey entry = Console.ReadKey(true).Key;
            if (entry == ConsoleKey.NumPad1 || entry == ConsoleKey.D1)
            {
                library.EditItem();
            }
            else if (entry == ConsoleKey.NumPad2 || entry == ConsoleKey.D2)
            {
                database.ListUsers();
            }
            else if (entry == ConsoleKey.NumPad3 || entry == ConsoleKey.D3)
            {
                CreateNewItem();
            }
            else if (entry == ConsoleKey.NumPad4 || entry == ConsoleKey.D4)
            {
                CreateNewUser();
            }
            else if (entry == ConsoleKey.NumPad5 || entry == ConsoleKey.D5)
            {
                DisplayUserConsole();
            }
            else if (entry == ConsoleKey.NumPad6 || entry == ConsoleKey.D6)
            {
                Logout();
            }
            else if (entry == ConsoleKey.Escape)
            {
                system_active = false;
                Logout();
            }
        }

        public void CreateNewUser()
        {
            int status = database.CheckStatus();
            User user = new User();
            user.CreateNewUser(status);
            database.UpdateList(user);
        }

        public void CreateNewItem()
        {
            Item item = new Item();
            int count = library.CheckStatus();
            item.CreateNewItem(item, count);
            library.UpdateList(item);
        }

        public void Login()
        {
            while (activeuser == null)
            {
                Console.WriteLine("Please Enter your Login: ");
                string login = Console.ReadLine();
                activeuser = database.ObtainLogin(login);
            }
            Console.WriteLine("Welcome, {0}!", activeuser.name);
        }

        public void Logout()
        {
            activeuser = null;
            logged_in = false;
            library.SaveLibraryData();
            database.SaveUserData();
        }

        public void LoadPreviousData()
        {
            database.LoadUserData();
            library.LoadLibraryData();
        }

        public void RunProgram()
        {
            try
            {
                LoadPreviousData();
            }
            catch (Exception)
            {
                InitializeSystem();
            }
            system_active = true;
            while (system_active == true)
            {
                Login();
                logged_in = true;
                while (logged_in == true)
                    DisplayUserConsole();
            }
        }
    }
}