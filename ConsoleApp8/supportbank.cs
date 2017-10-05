using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supportbank
{
    class Program
    {
        public static void Main(string[] args)
        {
            string path = @"C:\Users\Rich\test\supportproject\Transactions2014.csv";
            string textFromFile = System.IO.File.ReadAllText(path);
            Console.WriteLine(textFromFile);

            Console.Write("Who do you want to search for?");
            Console.ReadLine();

            List<Person> employees = new List<Person>();
            
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i = i + 1)
            {
                string[] line = lines[i].Split(',');
                string date = line[0];
                string from = line[1];
                string to = line[2];
                decimal amount = decimal.Parse(line[4]);

                if (!employees.Any(worker => worker.name == from))
                {
                    Person fromPerson = new Person { name = from, balance = -amount };
                    employees.Add(fromPerson);
                }
                else
                {
                    Person fromPerson = employees.First(worker => worker.name == from);
                    fromPerson.balance = fromPerson.balance - amount;
                }

                if (!employees.Any(worker => worker.name == to))
                {
                    Person toPerson = new Person { name = to, balance = amount };
                    employees.Add(toPerson);
                }
                else
                {
                    Person toPerson = employees.First(worker => worker.name == to);
                    toPerson.balance = toPerson.balance + amount;

                }
            }

            foreach (Person worker in employees)
            {
                Console.WriteLine(worker.name);
                Console.WriteLine(worker.balance);
            }

            Console.WriteLine("Amount of employees: {0}", employees.Count);
            Console.ReadLine();


        }
    }

    class Person
    {
        public string name;
        public decimal balance;

    }
}

