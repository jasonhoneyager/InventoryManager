using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager
{
    public class Program
    {
        static void Main(string[] args)
        {            
            Interface ui = new Interface();
            ui.RunProgram();
        }
    }
}
