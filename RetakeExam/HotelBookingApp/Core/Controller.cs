using BookingApp.Core.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Utilities.Messages;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Models.Bookings;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotelRepository;

        public Controller()
        {
            hotelRepository = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);

            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            hotel = new Hotel(hotelName, category);

            hotelRepository.AddNew(hotel);

            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            List<IHotel> hotels = hotelRepository.All().OrderBy(x => x.FullName).ToList();

            if (!hotels.Any(x => x.Category == category))
            {
                return String.Format(OutputMessages.CategoryInvalid, category);
            }

            List<IRoom> rooms = hotels.SelectMany(x => x.Rooms.All()).Where(x => x.PricePerNight > 0).ToList();
            List<IRoom> sortedRooms = rooms.OrderBy(x => x.BedCapacity).ToList();
            IRoom room = sortedRooms.FirstOrDefault(x => x.BedCapacity > adults + children);

            if (room is null)
            {
                return OutputMessages.RoomNotAppropriate;
            }
            else
            {
                IHotel hotel = null;

                foreach (var currHotel in hotels)
                {
                    if (currHotel.Rooms.All().Contains(room))
                    {
                        hotel = currHotel;
                    }
                }

                int bookingNum = 0;

                if (hotel != null)
                {
                    bookingNum = hotel.Bookings.All().Count() + 1;
                    Booking booking = new Booking(room, duration, adults, children, bookingNum);
                    hotel.Bookings.AddNew(booking);
                }

                return String.Format(OutputMessages.BookingSuccessful, bookingNum, hotel.FullName);
            }
        }

        public string HotelReport(string hotelName)
        {
            StringBuilder sb = new StringBuilder();

            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);
            
            if (hotel is null)
            {
                sb.AppendLine(String.Format(OutputMessages.HotelNameInvalid, hotelName));
            }
            else
            {
                sb.AppendLine($"Hotel name: {hotel.FullName}");
                sb.AppendLine($"--{hotel.Category} star hotel");
                sb.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
                sb.AppendLine($"--Bookings:");
                sb.AppendLine();

                if (hotel.Bookings.All().Count == 0)
                {
                    sb.AppendLine("none");
                }
                else
                {
                    foreach (var booking in hotel.Bookings.All())
                    {
                        sb.AppendLine(booking.BookingSummary());
                        sb.AppendLine();
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);

            if (hotel is null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            else if (hotel.Rooms.All().All(x => x.GetType().Name != roomTypeName))
            {
                return OutputMessages.RoomTypeNotCreated;
            }
            else if (roomTypeName == "Apartment" || roomTypeName == "DoubleBed" || roomTypeName == "Studio")
            {
                IRoom room = hotel.Rooms.All().First(x => x.GetType().Name == roomTypeName);

                if (room.PricePerNight != 0)
                {
                    throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
                }
                else
                {
                    room.SetPrice(price);
                    return String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotelRepository.All().FirstOrDefault(x => x.FullName == hotelName);

            if (hotel is null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            else if (hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                return OutputMessages.RoomTypeAlreadyCreated;
            }
            else if (roomTypeName == "Apartment" || roomTypeName == "DoubleBed" || roomTypeName == "Studio")
            {
                IRoom room;

                if (roomTypeName == "Apartment")
                {
                    room = new Apartment();
                }
                else if (roomTypeName == "DoubleBed")
                {
                    room = new DoubleBed();
                }
                else
                {
                    room = new Studio();
                }

                hotel.Rooms.AddNew(room);
                return String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
        }
    }
}
