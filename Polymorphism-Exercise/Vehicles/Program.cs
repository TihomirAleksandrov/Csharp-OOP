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
            string[] busInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));
            Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));
            Bus bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

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
                        try
                        {
                            car.Refuel(quantity);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
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
                        try
                        {
                            truck.Refuel(quantity);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
                else if (type == "Bus")
                {
                    if (command == "DriveEmpty")
                    {
                        Console.WriteLine(bus.DriveEmpty(quantity));
                    }
                    else if (command == "Drive")
                    {
                        Console.WriteLine(bus.Drive(quantity));
                    }
                    else if (command == "Refuel")
                    {
                        try
                        {
                            bus.Refuel(quantity);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
            }

            Console.WriteLine(car);
            Console.WriteLine(truck);
            Console.WriteLine(bus);
        }
    }
}
