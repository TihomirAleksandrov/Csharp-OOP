using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Citizen : Inhabitants,IIdentifiable
    {
        private string name;
        private int age;

        public Citizen(string name, int age, string id)
        {
            Name = name;
            Age = age;
            Id = id;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }
    }
}
