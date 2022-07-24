using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    internal abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;
        private double tankCapacity;

        internal Vehicle(double fuelQuantity, double consumption, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = consumption;
            TankCapacity = tankCapacity;
        }

        public double FuelQuantity
        {
            get => fuelQuantity;
            set => fuelQuantity = value;
        }

        public double FuelConsumption
        {
            get => fuelConsumption;
            set => fuelConsumption = value;
        }

        public double TankCapacity
        {
            get => tankCapacity;
            set
            {
                if (this.fuelQuantity > value)
                {
                    this.fuelQuantity = 0;
                }
                tankCapacity = value;
            }
        }

        public abstract string Drive(double distance);

        public abstract void Refuel(double liters);

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.fuelQuantity:f2}";
        }
    }
}
