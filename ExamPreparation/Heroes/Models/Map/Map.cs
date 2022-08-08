using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = new List<IHero>();
            var barbarians = new List<IHero>();

            foreach (var hero in players)
            {
                if (hero is Knight && hero.Weapon != null && hero.IsAlive)
                {
                    knights.Add(hero as Knight);
                }
                else if (hero is Barbarian && hero.Weapon != null && hero.IsAlive)
                {
                    barbarians.Add(hero as Barbarian);
                }
            }


            var startBattle = false;
            while (knights.Any(x => x.IsAlive) && barbarians.Any(x => x.IsAlive))
            {
                startBattle = true;
                foreach (var knight in knights)
                {
                    foreach (var barbarian in barbarians)
                    {
                        if (knight.IsAlive && barbarian.IsAlive)
                        {
                            barbarian.TakeDamage(knight.Weapon.DoDamage());
                        }
                    }
                }

                foreach (var barbarian in barbarians)
                {
                    foreach (var knight in knights)
                    {
                        if (barbarian.IsAlive && knight.IsAlive)
                        {
                            knight.TakeDamage(barbarian.Weapon.DoDamage());
                        }
                    }
                }
            }

            if (startBattle)
            {
                if (knights.Any(x => x.IsAlive))
                {
                    return $"The knights took {knights.Where(x => !x.IsAlive).ToList().Count} casualties but won the battle.";
                }
                return $"The barbarians took {knights.Where(x => !x.IsAlive).ToList().Count} casualties but won the battle.";
            }
            return null;
        }
    }
}
