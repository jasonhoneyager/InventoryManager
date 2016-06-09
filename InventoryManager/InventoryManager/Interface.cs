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

        public void initialize_system()
        {
            int status = database.check_status();
            if (status == 0)
            {
                Console.WriteLine("Initial User must be designated as Administrator.");
                User user = new User();
                user.create_new_user(status);
                database.update_list(user);              
            }
            else
            {
                Console.WriteLine("System Updated.");
            }
        }

        public void display_user_console()
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
                library.view_items();
            }
            else if (entry == ConsoleKey.NumPad2 || entry == ConsoleKey.D2)
            {
                library.checkout_item(activeuser);
            }
            else if (entry == ConsoleKey.NumPad3 || entry == ConsoleKey.D3)
            {
                library.check_item_count(activeuser);
            }
            else if (entry == ConsoleKey.NumPad4 || entry == ConsoleKey.D4)
            {
                library.view_checked(activeuser);
            }
            else if (entry == ConsoleKey.NumPad5 || entry == ConsoleKey.D5)
            {
                if (activeuser.admin == false)
                {
                    Console.WriteLine("Access Denied!");
                }
                else
                {
                    display_admin_console();
                }
            }
            else if (entry == ConsoleKey.NumPad6 || entry == ConsoleKey.D6)
            {
                logout();
            }
            else if (entry == ConsoleKey.Escape)
            {
                system_active = false;
                logout();
            }
        }

        public void display_admin_console()
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
                library.edit_item();
            }
            else if (entry == ConsoleKey.NumPad2 || entry == ConsoleKey.D2)
            {
                database.list_users();
            }
            else if (entry == ConsoleKey.NumPad3 || entry == ConsoleKey.D3)
            {
                create_new_item();
            }
            else if (entry == ConsoleKey.NumPad4 || entry == ConsoleKey.D4)
            {
                create_new_user();
            }
            else if (entry == ConsoleKey.NumPad5 || entry == ConsoleKey.D5)
            {
                display_user_console();
            }
            else if (entry == ConsoleKey.NumPad6 || entry == ConsoleKey.D6)
            {
                logout();
            }
            else if (entry == ConsoleKey.Escape)
            {
                system_active = false;
                logout();
            }
        }

        public void create_new_user()
        {
            int status = database.check_status();
            User user = new User();
            user.create_new_user(status);
            database.update_list(user);
        }

        public void create_new_item()
        {
            Item item = new Item();
            int count = library.check_status();
            item.create_new_item(item, count);
            library.update_list(item);
        }

        public void login()
        {
            while (activeuser == null)
            {
                Console.WriteLine("Please Enter your Login: ");
                string login = Console.ReadLine();
                activeuser = database.obtain_login(login);
            }
            Console.WriteLine("Welcome, {0}!", activeuser.name);
        }

        public void logout()
        {
            activeuser = null;
            logged_in = false;
            library.save_library_data();
            database.save_user_data();
        }

        public void load_previous_data()
        {
            database.load_user_data();
            library.load_library_data();
        }

        public void run_program()
        {
            try
            {
                load_previous_data();
            }
            catch (Exception)
            {
                initialize_system();
            }
            system_active = true;
            while (system_active == true)
            {
                login();
                logged_in = true;
                while (logged_in == true)
                    display_user_console();
            }
        }
    }
}