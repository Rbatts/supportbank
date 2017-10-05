using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thursdayproject
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 17;
            if ((age <= 5) && (age < 7))
            {
                Console.WriteLine("Go to school");
            }
            bool canDrive = age >= 16 ? true : false;
            Console.ReadLine();
        }
    }
}
