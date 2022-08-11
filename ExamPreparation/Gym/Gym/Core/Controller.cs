using Gym.Core.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Athletes;

namespace Gym.Core
{
    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new List<IGym>();
        }
        
        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete = null;
            var gym = gyms.Find(x => x.Name == gymName);
            
            if (athleteType == "Boxer")
            {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidAthleteType));
            }

            if (gym.GetType().Name == "BoxingGym" && athlete is Boxer)
            {
                gym.AddAthlete(athlete);
            }
            else if (gym.GetType().Name == "WeightliftingGym" && athlete is Weightlifter)
            {
                gym.AddAthlete(athlete);
            }
            else
            {
                return String.Format(OutputMessages.InappropriateGym);
            }

            return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType == "BoxingGloves")
            {
                this.equipment.Add(new BoxingGloves());
            }
            else if (equipmentType == "Kettlebell")
            {
                this.equipment.Add(new Kettlebell());
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidEquipmentType));
            }

            return String.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType == "BoxingGym")
            {
                gyms.Add(new BoxingGym(gymName));
            }
            else if (gymType == "WeightliftingGym")
            {
                gyms.Add(new WeightliftingGym(gymName));
            }
            else
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidGymType));
            }

            return String.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = gyms.Find(x => x.Name == gymName);

            return String.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var equipmentNeeded = this.equipment.FindByType(equipmentType);
            
            if (equipmentNeeded == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            var gym = gyms.Find(x => x.Name == gymName);

            gym.AddEquipment(equipmentNeeded);
            this.equipment.Remove(equipmentNeeded);
            
            return String.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().Trim();
        }

        public string TrainAthletes(string gymName)
        {
            var gym = gyms.Find(x => x.Name == gymName);

            gym.Exercise();

            return String.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }
    }
}
