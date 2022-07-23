using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    internal class Car : Vehicle
    {
        private const double additionalConsumption = 0.9;
        public Car(double fuelQuantity, double consumption) : base(fuelQuantity, consumption)
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
            this.FuelQuantity += liters;
        }
    }
}
