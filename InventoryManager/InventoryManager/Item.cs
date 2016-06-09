using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    [Serializable]
    public class Item
    {
        public string name;
        public string itemnumber;
        public int quantity;

        bool itemvalid;


        public void create_new_item(Item item, int count)
        {
            itemvalid = false;
            while (itemvalid == false)
            {
                name = set_item_name();
                quantity = set_item_quantity();
                itemnumber = set_item_number(count);
                validate_new_item(name, quantity, itemnumber, item, count);
            }
        }

        public string set_item_name()
        {
            Console.WriteLine("Please Enter Item Name: ");
            string item = Console.ReadLine();
            return item;
        }

        public int set_item_quantity()
        {
            Console.WriteLine("Please Enter Item Quantity: ");
            int item = Convert.ToInt32(Console.ReadLine());
            return item;
        }

        public string set_item_number(int count)
        {
            string number = Convert.ToString(count);
            string number2 = number.PadLeft(4, '0');
            return number2;
        }

        public void validate_new_item(string name, int quantity, string itemnumber, Item item, int count)
        {
            Console.WriteLine("Item Name: {0}.", name);
            Console.WriteLine("Item Number: {0}.", itemnumber);
            Console.WriteLine("Quantity: {0}.", quantity);
            Console.WriteLine("Is this OK?  Y/N: ");
            ConsoleKey entry = Console.ReadKey(true).Key;
            if (entry == ConsoleKey.N)  //readkey function forces an input each time it is used.
            {
                create_new_item(item, count);  //"entry" variable method allows for usage as designed in code.
            }
            else if (entry == ConsoleKey.Y)
            {
                itemvalid = true;
            }
            else
            {
                Console.WriteLine("Invalid Entry");
            }
        }
    }
}
