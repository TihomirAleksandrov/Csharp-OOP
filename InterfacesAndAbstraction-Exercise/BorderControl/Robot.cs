using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Robot : Inhabitants,IIdentifiable
    {
        private string model;

        public Robot(string model, string id)
        {
            Model = model;
            Id = id;
        }

        public string Model
        {
            get => model;
            set => model = value;
        }

    }
}
