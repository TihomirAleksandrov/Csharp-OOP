using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double initialArmor = 300;
        private bool sonarMode;

        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, 300)
        {
            SonarMode = false;
        }

        public bool SonarMode
        {
            get => sonarMode;
            private set => sonarMode = value;
        }


        public void ToggleSonarMode()
        {
            if (sonarMode)
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
                SonarMode = false;
            }
            else
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
                SonarMode = true;
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
            string sonarOnOrOff = sonarMode == true ? "ON" : "OFF";
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Sonar mode: {sonarOnOrOff}");

            return sb.ToString().TrimEnd();
        }
    }
}
