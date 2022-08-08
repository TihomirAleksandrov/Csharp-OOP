using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly IList<IHero> models;

        public HeroRepository() : base()
        {
            models = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models
        {
            get => (IReadOnlyCollection<IHero>)models;
        }

        public void Add(IHero model)
        {
            if (models.All(x => x.Name != model.Name))
            {
                models.Add(model);
            }
        }

        public IHero FindByName(string name)
        {
            var hero = models.FirstOrDefault(x => x.Name == name);


            if (hero == null)
            {
                throw new InvalidOperationException($"Hero {name} does not exist.");
            }
            return hero;
        }

        public bool Remove(IHero model)
        {
            var removedHero = models.FirstOrDefault(x => x.Name.Equals(model.Name));

            if (removedHero != null)
            {
                var index = models.IndexOf(model);
                models.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
