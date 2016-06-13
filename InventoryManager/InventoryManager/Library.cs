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

        public void EditItem()
        {
            Console.WriteLine("Which item # do you wish to edit?\n");
            foreach (Item item in items)
            {
                Console.WriteLine("Item #: {0}, Item: {1}, Quantity: {2}", item.itemnumber, item.name, item.quantity);
            }
            Console.WriteLine();
            string edit = Console.ReadLine();
            foreach (Item item in items)
            {
                if (item.itemnumber == edit)
                {
                    EditName(item);
                    EditQuantity(item);
                    break;
                }
            }
            Console.WriteLine("Invalid Input:\n");
        }

        public void EditName(Item item)
        {
            Console.WriteLine("Please Enter New Item Name:\n");
            item.name = Console.ReadLine();
        }

        public void EditQuantity(Item item)
        {
            Console.WriteLine("Please Enter New Item Quantity:\n");
            try
            {
                item.quantity = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid Entry.  Input must be a number.\n");
            }
        }

        public int CheckStatus()
        {
            int count = items.Count;
            return count;
        }
        
        public void UpdateList(Item item)
        {
            items.Add(item);
            Console.WriteLine("Library Updated.\n");
            Console.ReadKey();
        }

        public void SaveLibraryData()
        {
            string json = new JavaScriptSerializer().Serialize(items);
            File.WriteAllText(Environment.CurrentDirectory + @"\item.txt", json);
        }

        public void LoadLibraryData()
        {
            string json = File.ReadAllText(Environment.CurrentDirectory + @"\item.txt");
            items = new JavaScriptSerializer().Deserialize<List<Item>>(json);
        }

        public void ViewItems()
        {
            foreach (Item item in items)
            {
                Console.WriteLine("Item #: {0}, Item: {1}, Quantity: {2}", item.itemnumber, item.name, item.quantity);
            }
        }

        public void ViewChecked(User activeuser)
        {
            Console.WriteLine("Name: {0}, Login: {1}", activeuser.name, activeuser.login);
            int len = activeuser.held.Count;
            while (len > 0)
            {
                Console.WriteLine("{0}", activeuser.held[len - 1].name);
                len--;
            }
            Console.WriteLine();
        }

        public void CheckoutItem(User activeuser)
        {
            if (activeuser.held.Count == 3)
            {
                Console.WriteLine("Please Return something before checking out more items.\n");
            }
            else
            {
                Console.WriteLine("Which item # do you wish to checkout?\n");
                foreach (Item item in items)
                {
                    Console.WriteLine("Item #: {0}, Item: {1}, Quantity: {2}", item.itemnumber, item.name, item.quantity);
                }
                Console.WriteLine();
                string edit = Console.ReadLine();
                bool validitem = VerifyItem(edit);
                bool validquantity = VerifyQuantity(edit);
                CompleteCheckout(activeuser, edit, validitem, validquantity);
            }
        }

        public bool VerifyItem(string edit)
        {
            bool validitem = false;
            foreach (Item item in items)
            {
                if (edit == item.itemnumber)
                {
                    validitem = true;
                    break;
                }
            }
            return validitem;
        }

        public bool VerifyQuantity(string edit)
        {
            bool validquantity = true;
            foreach (Item item in items)
            {
                if (edit == item.itemnumber && item.quantity == 0)
                {
                    validquantity = false;
                    break;
                }
            }
            return validquantity;
        }

        public void CompleteCheckout(User activeuser, string edit, bool validitem, bool validquantity)
        {
            foreach (Item item in items)
            {
                if (validquantity == false || validitem == false)
                {
                    Console.WriteLine("Invalid Input\n");
                    break;
                }
                else if (item.itemnumber == edit && item.quantity > 0)
                {
                    item.quantity--;
                    activeuser.held.Add(item);
                }
            }
        }

        public void CheckItemCount(User activeuser)
        {
            if (activeuser.held.Count == 0)
            {
                Console.WriteLine("You don't have any items checked out.\n");
            }
            else
            {
                ReturnItem(activeuser);
            }
        }

        public void ReturnItem(User activeuser)
        {
            Console.WriteLine("Which item # do you wish to return?\n");
            foreach (Item item in activeuser.held)
            {
                Console.WriteLine("Item #: {0}, Item: {1}", item.itemnumber, item.name);
            }
            Console.WriteLine();
            string edit = Console.ReadLine();
            bool validinput = false;
            foreach (Item item in activeuser.held)
            {
                if (item.itemnumber == edit)
                {
                    activeuser.held.Remove(item);
                    CompleteReturn(activeuser, edit);
                    validinput = true;
                    break;
                }
            }
            if (validinput == false)
            {
                Console.WriteLine("Invalid Input:\n");
            }
        }

        public void CompleteReturn(User activeuser, string edit)
        {
            foreach (Item item in items)
                if (item.itemnumber == edit)
                {
                    item.quantity++;
                }
        }
    }
}