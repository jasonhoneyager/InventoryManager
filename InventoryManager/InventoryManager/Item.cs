﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    public class Item
    {
        public string name;
        public string itemnumber;
        public int quantity;
        bool itemvalid;

        public void CreateNewItem(Item item, int count)
        {
            itemvalid = false;
            while (itemvalid == false)
            {
                name = SetItemName();
                quantity = SetItemQuantity();
                itemnumber = SetItemNumber(count);
                ValidateNewItem(name, quantity, itemnumber, item, count);
            }
        }

        public string SetItemName()
        {
            Console.WriteLine("Please Enter Item Name:\n");
            string item = Console.ReadLine();
            Console.WriteLine();
            return item;
        }

        public int SetItemQuantity()
        {
            int item = -1;
            while (item < 0)
            {
                Console.WriteLine("Please Enter Item Quantity:\n");
                try
                {
                    item = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid Entry.  Input must be a number.\n");
                }
            }
            Console.WriteLine();
            return item;
        }

        public string SetItemNumber(int count)
        {
            string number = Convert.ToString(count);
            string number2 = number.PadLeft(4, '0');
            return number2;
        }

        public void ValidateNewItem(string name, int quantity, string itemnumber, Item item, int count)
        {
            Console.WriteLine("Item Name: {0}.", name);
            Console.WriteLine("Item Number: {0}.", itemnumber);
            Console.WriteLine("Quantity: {0}.", quantity);
            Console.WriteLine("Is this OK?  Y/N:\n");
            ConsoleKey entry = Console.ReadKey(true).Key;
            if (entry == ConsoleKey.N)  //readkey function forces an input each time it is used.
            {
                CreateNewItem(item, count);  //"entry" variable method allows for usage as designed in code.
            }
            else if (entry == ConsoleKey.Y)
            {
                itemvalid = true;
            }
            else
            {
                Console.WriteLine("Invalid Entry\n");
            }
        }
    }
}
