using System;
using System.Linq;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            
            string input = Console.ReadLine();
            while (input != "Beast!")
            {
                try
                {
                    string[] animalInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                    string name = animalInfo[0];
                    int age = int.Parse(animalInfo[1]);

                    if (input == "Cat")
                    {
                        string gender = animalInfo[2];
                        animals.Add(new Cat(name, age, gender));
                    }
                    else if (input == "Dog")
                    {
                        string gender = animalInfo[2];
                        animals.Add(new Dog(name, age, gender));
                    }
                    else if (input == "Frog")
                    {
                        string gender = animalInfo[2];
                        animals.Add(new Frog(name, age, gender));
                    }
                    else if (input == "Kitten")
                    {
                        animals.Add(new Kitten(name, age));
                    }
                    else if (input == "Tomcat")
                    {
                        animals.Add(new Tomcat(name, age));
                    }
                    else
                    {
                        throw new Exception("Invalid input!");
                    }
                }
                catch (Exception)
                {

                    throw new Exception("Invalid input!");
                }
                
                input = Console.ReadLine();
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}
