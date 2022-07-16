using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Inhabitants : IIdentifiable
    {
        private string id;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public bool Detain(string lastDigits)
        {
            string substring = id.Substring(id.Length - lastDigits.Length);
            return substring == lastDigits ? true : false;
        }
    }
}
