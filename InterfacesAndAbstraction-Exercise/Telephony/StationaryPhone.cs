using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public class StationaryPhone : ICallable
    {
        private string phoneNumber;
        
        public StationaryPhone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        
        public string PhoneNumber 
        {
            get => phoneNumber;
            set => phoneNumber = value;
        }

        public void Call()
        {
            if (this.phoneNumber.Any(x => !char.IsDigit(x)))
            {
                Console.WriteLine("Invalid number!");
            }
            else
            {
                Console.WriteLine($"Dialing... {this.phoneNumber}");
            }
        }
    }
}
