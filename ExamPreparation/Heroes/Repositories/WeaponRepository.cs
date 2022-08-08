using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private readonly IList<IWeapon> models;

        public WeaponRepository() : base()
        {
            models = new List<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models
        {
            get => (IReadOnlyCollection<IWeapon>)models;
        }

        public void Add(IWeapon model)
        {
            if (models.All(x => x.Name != model.Name))
            {
                models.Add(model);
            }
        }

        public IWeapon FindByName(string name)
        {
            var weapon = models.FirstOrDefault(x => x.Name == name);

            if (weapon == null)
            {
                throw new InvalidOperationException($"Weapon {name} does not exist.");
            }
            return weapon;
        }

        public bool Remove(IWeapon model)
        {
            var removedWeapon = models.FirstOrDefault(x => x.Name.Equals(model.Name));

            if (removedWeapon != null)
            {
                var index = models.IndexOf(model);
                models.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
