using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InventoryManager
{
    [Serializable]
    public class UserDatabase
    {
        List<User> users = new List<User>();

        public int check_status()
        {
            int state = users.Count;
            return state;
        }

        public void save_user_data()
        {
            string json = new JavaScriptSerializer().Serialize(users);
            File.WriteAllText(Environment.CurrentDirectory + @"\user.txt", json);
        }

        public void load_user_data()
        {
            string json = File.ReadAllText(Environment.CurrentDirectory + @"\user.txt");
            users = new JavaScriptSerializer().Deserialize<List<User>>(json);
        }

        public void update_list(User user)
        {
            users.Add(user);
            Console.WriteLine("Database Updated.");
            Console.ReadKey();
        }

        public User obtain_login(string login)
        {
            foreach (User user in users)
            {
                if (login == user.login)
                {
                    return user;
                }                
            }
            Console.WriteLine("Login not found.");
            User failedlogin;
            failedlogin = null;
            return failedlogin;            
        }
    }
}