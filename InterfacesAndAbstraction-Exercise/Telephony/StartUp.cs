using System;
using System.Collections.Generic;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] websites = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            foreach (string phoneNumber in phoneNumbers)
            {
                if (phoneNumber.Length == 7)
                {
                    StationaryPhone phone = new StationaryPhone(phoneNumber);
                    phone.Call();
                }
                else
                {
                    Smartphone phone = new Smartphone(phoneNumber);
                    phone.Call();
                }
            }

            foreach (string website in websites)
            {
                Smartphone phone = new Smartphone();
                phone.Website = website;
                phone.BrowseWeb();
            }
        }
    }
}
