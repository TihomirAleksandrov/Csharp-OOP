using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private IRepository<IRoom> roomRepository;
        private IRepository<IBooking> bookingRepository;

        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            roomRepository = new RoomRepository();
            bookingRepository = new BookingRepository();
        }
        
        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullName = value;
            }
        }

        public int Category
        {
            get => category;
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
            }
        }

        public double Turnover => SumBookings();

        public IRepository<IRoom> Rooms
        {
            get => roomRepository;
            set => roomRepository = value;
        }
        public IRepository<IBooking> Bookings
        {
            get => bookingRepository;
            set => bookingRepository = value;
        }

        private double SumBookings()
        {
            double sum = 0;

            foreach(IBooking booking in Bookings.All())
            {
                sum += booking.ResidenceDuration * booking.Room.PricePerNight;
            }

            return Math.Round(sum,2);
        }
    }
}
