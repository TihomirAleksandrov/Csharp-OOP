using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class Smartphone : ICallable, IBrowseable
    {
        private string phoneNumber;
        private string website;
        
        public Smartphone()
        {

        }
        
        public Smartphone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public string PhoneNumber 
        { 
            get => phoneNumber;
            set => phoneNumber = value;
        }

        public string Website 
        {
            get => website;
            set => website = value;
        }

        public void Call()
        {
            if (this.phoneNumber.Any(x => !char.IsDigit(x)))
            {
                Console.WriteLine("Invalid number!");
            }
            else
            {
                Console.WriteLine($"Calling... {this.phoneNumber}");
            }
        }

        public void BrowseWeb()
        {
            if (this.website.Any(x => char.IsDigit(x)))
            {
                Console.WriteLine("Invalid URL!");
            }
            else
            {
                Console.WriteLine($"Browsing: {this.website}!");
            }
        }
    }
}
