using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Inhabitants> inhabitants = new List<Inhabitants>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] info = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

                string name = info[0];

                if (info.Length == 2)
                {
                    string id = info[1];
                    Robot robot = new Robot(name, id);
                    inhabitants.Add(robot);
                }
                else
                {
                    int age = int.Parse(info[1]);
                    string id = info[2];
                    Citizen citizen = new Citizen(name, age, id);
                    inhabitants.Add(citizen);
                }
            }

            string lastDigits = Console.ReadLine();

            foreach (Inhabitants inhabitant in inhabitants)
            {
                if (inhabitant.Detain(lastDigits))
                {
                    Console.WriteLine(inhabitant.Id);
                }
            }
        }
    }
}
