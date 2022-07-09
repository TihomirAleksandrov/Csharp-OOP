using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        private string name;
        private int age;
        private string gender;
        
        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Invalid input!");
                }
                this.name = value;
            } 
        }
        public int Age 
        {
            get
            {
                return age;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Invalid input!");
                }
                this.age = value;
            }
        }
        public string Gender 
        {
            get
            {
                return gender;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Invalid input!");
                }
                this.gender = value;
            }
        }


        public abstract string ProduceSound();


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().Name);
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            sb.Append(this.ProduceSound());
            return sb.ToString();
        }
    }
}
