using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const double initialArmor = 200;
        private bool submergeMode;

        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 200)
        {
            SubmergeMode = false;
        }

        public bool SubmergeMode
        {
            get => submergeMode;
            private set => submergeMode = value;
        }

        public void ToggleSubmergeMode()
        {
            if (submergeMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
                submergeMode = false;
            }
            else
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
                submergeMode = true;
            }
        }

        public override void RepairVessel()
        {
            if (this.ArmorThickness < initialArmor)
            {
                this.ArmorThickness = initialArmor;
            }
        }

        public override string ToString()
        {
            string submergeOnOrOff = SubmergeMode == true ? "ON" : "OFF";
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Submerge mode: {submergeOnOrOff}");

            return sb.ToString().TrimEnd();
        }
    }
}
