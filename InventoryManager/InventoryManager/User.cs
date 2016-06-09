using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Serializable]
    public class User
    {
        public string name;
        public bool admin;
        public string login;
        public List<Item> held = new List<Item>();

        public bool uservalid;

        public void create_new_user(int status)
        {
            while (uservalid == false)
            {
                name = name_new_user();
                admin = set_admin_privilige(status);
                login = set_login(admin, status);
                validate_new_info(name, admin, login, status);
            }
        }

        public string name_new_user()
        {
            Console.WriteLine("Please Enter Your Name: ");
            string name = Console.ReadLine();
            return name;
        }

        public bool set_admin_privilige(int status)
        {
            if (status == 0)
            {
                bool admin = true;
                return admin;
            }
            else
            {
                Console.WriteLine("Make this user an Admin? Y/N: ");
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Y)
                {
                    admin = true;
                }
                else
                {
                    admin = false;
                }
                return admin;
            }
        }

        public string set_login(bool admin, int status)
        {
            string len = Convert.ToString(status);
            string login = len.PadLeft(4, '0');
            if (admin == true)
            {
                login = ("a" + login);
                return login;
            }
            else
            {
                login = ("u" + login);
                return login;
            }
        }

        public void validate_new_info(string name, bool admin, string login, int status)
        {
            Console.WriteLine("User Name: {0}.", name);
            Console.WriteLine("User Login: {0}.", login);
            Console.WriteLine("Admin Access: {0}.", admin);
            Console.WriteLine("Is this OK?  Y/N: ");
            ConsoleKey entry = Console.ReadKey(true).Key;
            if (entry == ConsoleKey.N)  //readkey function forces an input each time it is used.
            {
                create_new_user(status);  //"entry" variable method allows for usage as designed in code.
            }
            else if (entry == ConsoleKey.Y)
            {
                uservalid = true;
            }
            else
            {
                Console.WriteLine("Invalid Entry");
            }
        }
    }
}
