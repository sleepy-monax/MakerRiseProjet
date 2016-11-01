using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProg
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 1024; i++)
            {
                Console.WriteLine($"(char){i} is {0}", (char)i);
            }
            Console.ReadLine();
        }
    }
}
