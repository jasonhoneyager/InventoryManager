using System;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Serializable]
    class Library
    {
        List<Item> items = new List<Item>();

        public void edit_item()
        {
            Console.WriteLine("Which item # do you wish to edit? ");
            foreach (Item item in items)
            {
                Console.WriteLine("{0}, {1}, {2}", item.itemnumber, item.name, item.quantity);
            }
            Console.WriteLine();
            string edit = Console.ReadLine();
            foreach (Item item in items)
                if (item.itemnumber == edit)
                {
                    edit_name(item);
                    edit_quantity(item);
                    break;
                }                 
        }

        public void edit_name(Item item)
        {
            Console.WriteLine("Please Enter New Item Name: ");
            item.name = Console.ReadLine();
        }

        public void edit_quantity(Item item)
        {
            Console.WriteLine("Please Enter New Item Quantity: ");
            item.quantity = Convert.ToInt32(Console.ReadLine());
        }

        public int check_status()
        {
            int count = items.Count;
            return count;
        }
        
        public void update_list(Item item)
        {
            items.Add(item);
            Console.WriteLine("Library Updated.");
            Console.ReadKey();
        }

        public void save_library_data()
        {
            string json = new JavaScriptSerializer().Serialize(items);
            File.WriteAllText(Environment.CurrentDirectory + @"\item.txt", json);
        }

        public void load_library_data()
        {
            string json = File.ReadAllText(Environment.CurrentDirectory + @"\item.txt");
            items = new JavaScriptSerializer().Deserialize<List<Item>>(json);
        }

        public void view_items()
        {
            foreach (Item item in items)
            {
                Console.WriteLine("{0}, {1}, {2}", item.itemnumber, item.name, item.quantity);
            }
        }

        public void view_checked(User activeuser)
        {
            Console.WriteLine("{0}, {1}", activeuser.name, activeuser.login);
            int len = activeuser.held.Count;
            while (len > 0)
            {
                Console.WriteLine("{0}", activeuser.held[len - 1].name);
                len--;
            }
        }

        public void checkout_item(User activeuser)
        {
            if (activeuser.held.Count == 3)
            {
                Console.WriteLine("Please Return something before checking out more items.");
            }
            else
            {
                Console.WriteLine("Which item # do you wish to checkout? ");
                foreach (Item item in items)
                {
                    Console.WriteLine("{0}, {1}, {2}", item.itemnumber, item.name, item.quantity);
                }
                Console.WriteLine();
                string edit = Console.ReadLine();
                foreach (Item item in items)
                    if (item.itemnumber == edit && item.quantity > 0)
                    {
                        item.quantity--;
                        activeuser.held.Add(item);
                    }
            }
        }

        public void return_item(User activeuser)
        {
            Console.WriteLine("Which item # do you wish to return? ");
            foreach (Item item in activeuser.held)
            {
                Console.WriteLine("{0}, {1}", item.itemnumber, item.name);
            }
            Console.WriteLine();
            string edit = Console.ReadLine();
            foreach (Item item in activeuser.held)
            {
                if (item.itemnumber == edit)
                {
                    activeuser.held.Remove(item);
                    complete_return(activeuser, edit);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input:");
                }
            }                
        }
        public void complete_return(User activeuser, string edit)
        {
            foreach (Item item in items)
                if (item.itemnumber == edit)
                {
                    item.quantity++;
                }
        }
    }
}
