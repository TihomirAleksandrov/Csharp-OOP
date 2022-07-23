using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Animal
    {
        private string name;
        private string favouriteFood;

        public Animal(string name, string favouriteFood)
        {
            Name = name;
            FavoriteFood = favouriteFood;
        }

        public string Name
        {
            get => name;
            private set => name = value;
        }

        public string FavoriteFood
        {
            get => favouriteFood;
            private set => favouriteFood = value;
        }

        public virtual string ExplainSelf()
        {
            return $"I am {Name} and my favourite food is {FavoriteFood}";
        }
    }
}
