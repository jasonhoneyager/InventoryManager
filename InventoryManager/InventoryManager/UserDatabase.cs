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

        public int CheckStatus()
        {
            int state = users.Count;
            return state;
        }

        public void SaveUserData()
        {
            string json = new JavaScriptSerializer().Serialize(users);
            File.WriteAllText(Environment.CurrentDirectory + @"\user.txt", json);
        }

        public void LoadUserData()
        {
            string json = File.ReadAllText(Environment.CurrentDirectory + @"\user.txt");
            users = new JavaScriptSerializer().Deserialize<List<User>>(json);
        }

        public void UpdateList(User user)
        {
            users.Add(user);
            Console.WriteLine("Database Updated.\n");
            Console.ReadKey();
        }

        public User ObtainLogin(string login)
        {
            foreach (User user in users)
            {
                if (login == user.login)
                {
                    return user;
                }                
            }
            Console.WriteLine("Login not found.\n");
            User failedlogin;
            failedlogin = null;
            return failedlogin;            
        }

        public void ListUsers()
        {
            foreach (User user in users)
            {
                Console.WriteLine("Name: {0}, Login: {1}, Admin: {2}\n", user.name, user.login, user.admin);
            }
        }
    }
}