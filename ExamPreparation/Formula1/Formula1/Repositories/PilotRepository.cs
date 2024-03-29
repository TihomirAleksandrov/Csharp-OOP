﻿using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private ICollection<IPilot> models;

        public PilotRepository() : base()
        {
            models = new List<IPilot>();
        }
        
        public IReadOnlyCollection<IPilot> Models
        {
            get => (IReadOnlyCollection<IPilot>)models;
        }

        public void Add(IPilot model)
        {
            models.Add(model);
        }

        public IPilot FindByName(string name)
        {
            return models.FirstOrDefault(x => x.FullName == name);
        }

        public bool Remove(IPilot model)
        {
            return models.Remove(model);
        }
    }
}
