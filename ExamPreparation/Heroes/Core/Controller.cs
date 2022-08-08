
using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    internal class Controller : IController
    {
        private HeroRepository heroes;
        private WeaponRepository weapons;

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            IWeapon weapon = weapons.FindByName(weaponName);

            if (hero.Weapon == null)
            {
                var type = weapon.GetType().Name == "Claymore" ? "claymore" : "mace";
                hero.AddWeapon(weapon);
                weapons.Remove(weapon);
                return $"Hero {heroName} can participate in battle using a {type}.";
            }
            else
            {
                throw new InvalidOperationException($"Hero {heroName} is well-armed.");
            }
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero;
            if (type == "Knight")
            {
                hero = new Knight(name, health, armour);
            }
            else if (type == "Barbarian")
            {
                hero = new Barbarian(name, health, armour);
            }
            else
            {
                throw new InvalidOperationException("Invalid hero type.");
            }

            if (heroes.Models.All(x => x.Name != name))
            {
                heroes.Add(hero);
                
                if (hero is Knight)
                {
                    return $"Successfully added Sir {name} to the collection.";
                }
                else
                {
                    return $"Successfully added Barbarian {name} to the collection.";
                }
            }
            else
            {
                throw new InvalidOperationException($"The hero {name} already exists.");
            }
            
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon;
            if (type == "Claymore")
            {
                weapon = new Claymore(name, durability);
            }
            else if (type == "Mace")
            {
                weapon = new Mace(name, durability);
            }
            else
            {
                throw new InvalidOperationException("Invalid weapon type.");
            }

            if (weapons.Models.All(x => x.Name != name))
            {
                weapons.Add(weapon);
                var weaponType = weapon.GetType().Name == "Claymore" ? "claymore" : "mace";
                return $"A {weaponType} {name} is added to the collection.";
            }
            else
            {
                throw new InvalidOperationException($"The weapon {name} already exists.");
            }
        }

        public string HeroReport()
        {
            StringBuilder info = new StringBuilder();
            
            foreach (var hero in heroes.Models.OrderBy(x => x.GetType().Name).ThenByDescending(x => x.Health).ThenBy(x => x.Name))
            {
                info.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                info.AppendLine($"--Health: {hero.Health}");
                info.AppendLine($"--Armour: {hero.Armour}");
                info.AppendLine(hero.Weapon == null ? "--Weapon: Unarmed" : $"--Weapon: {hero.Weapon.Name}");
            }

            return info.ToString().Trim();
        }

        public string StartBattle()
        {
            IMap map = new Map();

            return map.Fight(heroes.Models as ICollection<IHero>);
        }
    }
}
