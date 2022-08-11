using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
{
    public abstract class Gym : IGym
    {
        private string name;
        private int capacity;
        private List<IEquipment> equipment;
        private List<IAthlete> athletes;

        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            equipment = new List<IEquipment>();
            athletes = new List<IAthlete>();
        }
        
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidGymName));
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public double EquipmentWeight
        {
            get { return this.equipment.Sum(x => x.Weight); }
        }

        public ICollection<IEquipment> Equipment
        {
            get => equipment;
        }

        public ICollection<IAthlete> Athletes
        {
            get => athletes;
        }

        public void AddAthlete(IAthlete athlete)
        {
            if (athletes.Count == Capacity)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NotEnoughSize));
            }
            athletes.Add(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            this.equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach(var athlete in athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            string athletes = this.athletes.Count > 0 ? string.Join(", ", this.athletes.Select(x => x.FullName).ToArray()) : "No athletes";
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Name} is a {this.GetType().Name}:");
            sb.AppendLine($"Athletes: {athletes}");
            sb.AppendLine($"Equipment total count: {this.equipment.Count}");
            sb.AppendLine($"Equipment total weight: {this.EquipmentWeight:f2} grams");

            return sb.ToString().Trim();
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return athletes.Remove(athlete);
        }
    }
}
