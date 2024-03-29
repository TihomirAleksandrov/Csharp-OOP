﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    internal class Car : Vehicle
    {
        private const double additionalConsumption = 0.9;
        public Car(double fuelQuantity, double consumption, double tankCapacity) : base(fuelQuantity, consumption, tankCapacity)
        {

        }

        public override string Drive(double distance)
        {
           double fuelNeeded = (additionalConsumption + this.FuelConsumption) * distance;
            if (fuelNeeded > this.FuelQuantity)
            {
                return "Car needs refueling";
            }
            else
            {
                this.FuelQuantity -= fuelNeeded;
                return $"Car travelled {distance} km";
            }
        }

        public override void Refuel(double liters)
        {
            double totalFuel = liters + this.FuelQuantity;

            if (liters <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            else if (totalFuel > this.TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
            }
            else
            {
                this.FuelQuantity += liters;
            }
        }
    }
}
