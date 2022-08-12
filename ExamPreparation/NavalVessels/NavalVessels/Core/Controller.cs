using NavalVessels.Core.Contracts;
using NavalVessels.Utilities.Messages;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private VesselRepository vessels;
        private List<ICaptain> captains;

        public Controller()
        {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var captain = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);
            var vessel = vessels.FindByName(selectedVesselName);

            if (captain == null)
            {
                return String.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }
            else if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }
            else if (vessel.Captain != null)
            {
                return String.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            captains.Add(captain);
            captain.AddVessel(vessel);
            vessel.Captain = captain;
            return String.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var attackingVessel = vessels.FindByName(attackingVesselName);
            var defendingVessel = vessels.FindByName(defendingVesselName);

            if (attackingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
            else if (defendingVessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }
            else if (attackingVessel.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            else if (defendingVessel.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }
            else
            {
                attackingVessel.Attack(defendingVessel);
                attackingVessel.Captain.IncreaseCombatExperience();
                defendingVessel.Captain.IncreaseCombatExperience();

                return String.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
            }
        }

        public string CaptainReport(string captainFullName)
        {
            var captain = captains.FirstOrDefault(x => x.FullName == captainFullName);

            return captain.Report();
        }

        public string HireCaptain(string fullName)
        {
            var captain = captains.FirstOrDefault(x => x.FullName == fullName);

            if (captain == null)
            {
                captain = new Captain(fullName);
                captains.Add(captain);
                return String.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
            }
            else
            {
                return String.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            var vessel = vessels.FindByName(name);

            if (vessel == null)
            {
                if (vesselType == "Submarine")
                {
                    vessel = new Submarine(name, mainWeaponCaliber, speed);
                }
                else if (vesselType == "Battleship")
                {
                    vessel = new Battleship(name, mainWeaponCaliber, speed);
                }
                else
                {
                    return String.Format(OutputMessages.InvalidVesselType);
                }

                vessels.Add(vessel);
                return String.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
            }
            else
            {
                return String.Format(OutputMessages.VesselIsAlreadyManufactured, vessel.GetType().Name, name);
            }
        }

        public string ServiceVessel(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }
            else
            {
                vessel.RepairVessel();
                return String.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
            }
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }
            else if (vessel is Battleship)
            {
                Battleship battleship = (Battleship)vessel;
                battleship.ToggleSonarMode();
                return String.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            else
            {
                Submarine submarine = (Submarine)vessel;
                submarine.ToggleSubmergeMode();
                return String.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
        }

        public string VesselReport(string vesselName)
        {
            var vessel = vessels.FindByName(vesselName);

            return vessel.ToString();
        }
    }
}
