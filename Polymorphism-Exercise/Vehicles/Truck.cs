using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    internal class Truck : Vehicle
    {
        private const double additionalConsumption = 1.6;
        
        public Truck(double fuelQuantity, double consumption) : base(fuelQuantity, consumption)
        {

        }

        public override string Drive(double distance)
        {
            double fuelNeeded = (additionalConsumption + this.FuelConsumption) * distance;
            if (fuelNeeded > this.FuelQuantity)
            {
                return "Truck needs refueling";
            }
            else
            {
                this.FuelQuantity -= fuelNeeded;
                return $"Truck travelled {distance} km";
            }
        }

        public override void Refuel(double liters)
        {
            this.FuelQuantity += (liters * 0.95);
        }
    }
}
