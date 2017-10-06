using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supportbank
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            logger.Log(LogLevel.Info, "Program starting");


            string path = @"C:\Users\Rich\test\supportproject\Transactions2014.csv";
            string textFromFile = System.IO.File.ReadAllText(path);
            Console.WriteLine(textFromFile);

            string path2 = @"C: \Users\Rich\Downloads\dodgy transactions.txt";
            string textFromFile2 = System.IO.File.ReadAllText(path2);
            Console.WriteLine(textFromFile2);

            List<Transaction> transactions = new List<Transaction>();
            List<Person> employees = new List<Person>();
            
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i = i + 1)
            {
                string[] line = lines[i].Split(',');
                string date = line[0];
                string from = line[1];
                string to = line[2];
                string narrative = line[3];
                decimal amount = decimal.Parse(line[4]);

                var transaction = new Transaction { date = date, to = to, amount = amount, narrative = narrative, from = from };

                if (!employees.Any(worker => worker.name == from))
                {
                    Person fromPerson = new Person { name = from, balance = -amount };
                    employees.Add(fromPerson);
                    fromPerson.transactions.Add(transaction);
                }
                else
                {
                    Person fromPerson = employees.First(worker => worker.name == from);
                    fromPerson.balance = fromPerson.balance - amount;
                    fromPerson.transactions.Add(transaction);
                }

                if (!employees.Any(worker => worker.name == to))
                {
                    Person toPerson = new Person { name = to, balance = amount };
                    employees.Add(toPerson);
                    toPerson.transactions.Add(transaction);
                }
                else
                {
                    Person toPerson = employees.First(worker => worker.name == to);
                    toPerson.balance = toPerson.balance + amount;
                    toPerson.transactions.Add(transaction);
                }
            }

            foreach (Person worker in employees)
            {
                Console.WriteLine(worker.name);
                Console.WriteLine(worker.balance);
            }

            Console.WriteLine("Input employee");
            var userInput = Console.ReadLine();
            foreach (Person worker in employees)
            {
                if (userInput.ToLower() == worker.name.ToLower())
                {
                    foreach (Transaction transaction in worker.transactions)
                        Console.WriteLine(transaction.date + " " + transaction.from + " " + transaction.to + " " + transaction.narrative + "   " + transaction.amount);
                }
                
            }
            Console.ReadLine();

        }
    }

    class Person
    {
        public string name;
        public decimal balance;
        public List<Transaction> transactions = new List<Transaction>();
        
    }

    class Transaction
    {
        public string to;
        public string from;
        public string date;
        public string narrative;
        public decimal amount;
    }

    class ValidationResult
    {
        public string Validationresult;
    }
}

