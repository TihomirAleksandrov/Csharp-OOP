using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            List<Product> products = new List<Product>();

            string[] peopleInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (string people in peopleInput)
            {
                string[] personInfo = people.Split("=", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string name = personInfo[0];
                double money = double.Parse(personInfo[1]);

                try
                {
                    persons.Add(new Person(name, money));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return;
                }
            }

            string[] productInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray();

            foreach (string product in productInput)
            {
                string[] productInfo = product.Split("=", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string name = productInfo[0];
                double price = double.Parse(productInfo[1]);

                try
                {
                    products.Add(new Product(name, price));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return;
                }
            }

            string command = Console.ReadLine();

            while (command != "END")
            {
                string[] splitCommand = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string name = splitCommand[0];
                string productName = splitCommand[1];

                try
                {
                    Person person = persons.FirstOrDefault(x => x.Name == name);
                    Product product = products.FirstOrDefault(x => x.Name == productName);

                    person.BuyProduct(product);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                
                command = Console.ReadLine();
            }

            foreach (Person person in persons)
            {
                Console.WriteLine(person);
            }
        }
    }
}
