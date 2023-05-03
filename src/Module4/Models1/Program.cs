using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models1
{
    class Program
    {
        public static void Main()
        {
            var dc = new SymBankDataContext();
            var list = dc.Accounts.ToList();
            Console.WriteLine(list.Count);
        }
    }
}
