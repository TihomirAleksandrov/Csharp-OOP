using System;
using System.Linq;

namespace Vehicles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] truckInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));
            Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]));

            int inputNum = int.Parse(Console.ReadLine());

            for (int i = 0; i < inputNum; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                string command = input[0];
                string type = input[1];
                double quantity = double.Parse(input[2]);

                if (type == "Car")
                {
                    if (command == "Drive")
                    {
                        Console.WriteLine(car.Drive(quantity));
                    }
                    else if (command == "Refuel")
                    {
                        car.Refuel(quantity);
                    }
                }
                else if (type == "Truck")
                {
                    if (command == "Drive")
                    {
                        Console.WriteLine(truck.Drive(quantity));
                    }
                    else if (command == "Refuel")
                    {
                        truck.Refuel(quantity);
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
