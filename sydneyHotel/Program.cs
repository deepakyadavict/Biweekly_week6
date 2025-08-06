using System;
using System.Collections.Generic;
using System.Linq;

namespace SydneyHotel
{
    class Program
    {
        // ReservationDetail class to represent customer reservation information
        class ReservationDetail
        {
            public string customerName { get; set; }
            public int nights { get; set; }
            public string roomService { get; set; }
            public double totalPrice { get; set; }

            // Method to calculate total price based on nights and room service
            public void CalculateTotalPrice()
            {
                double price = 0;

                if (nights > 0 && nights <= 3)
                    price = 100 * nights;
                else if (nights > 3 && nights <= 10)
                    price = 80.5 * nights;
                else if (nights > 10 && nights <= 20)
                    price = 75.3 * nights;

                if (roomService.ToLower() == "yes")
                    price += price * 0.1;

                totalPrice = price;
            }
        }

        // Method to display summary of highest and lowest spenders
        static void DisplaySummary(List<ReservationDetail> reservations)
        {
            var max = reservations.OrderByDescending(r => r.totalPrice).First();
            var min = reservations.OrderBy(r => r.totalPrice).First();

            Console.WriteLine("\n\t\t\tSummary of Reservation");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Name\t\tNights\t\tRoom Service\t\tCharge");
            Console.WriteLine($"{min.customerName}\t\t{min.nights}\t\t{min.roomService}\t\t${min.totalPrice:F2}");
            Console.WriteLine($"{max.customerName}\t\t{max.nights}\t\t{max.roomService}\t\t${max.totalPrice:F2}");
            Console.WriteLine("\n------------------------------------------------------------");
            Console.WriteLine($"The customer spending the most is: {max.customerName} (${max.totalPrice:F2})");
            Console.WriteLine($"The customer spending the least is: {min.customerName} (${min.totalPrice:F2})");
        }

        static void Main(string[] args)
        {
            Console.WriteLine(".................Welcome to Sydney Hotel....................");

            Console.Write("\nEnter number of customers: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.Write("Invalid input. Please enter a valid number of customers: ");
            }

            Console.WriteLine("\n------------------------------------------------------------\n");

            List<ReservationDetail> reservations = new List<ReservationDetail>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Entering details for customer {i + 1} ---");
                var reservation = new ReservationDetail();

                Console.Write("Enter customer name: ");
                reservation.customerName = Console.ReadLine();

                // Validate number of nights
                int nights;
                while (true)
                {
                    Console.Write("Enter number of nights (1 to 20): ");
                    if (int.TryParse(Console.ReadLine(), out nights) && nights >= 1 && nights <= 20)
                        break;
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 20.");
                }
                reservation.nights = nights;

                // Validate room service input
                string service;
                while (true)
                {
                    Console.Write("Room service? (yes/no): ");
                    service = Console.ReadLine().Trim().ToLower();
                    if (service == "yes" || service == "no")
                        break;
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
                reservation.roomService = service;

                // Calculate total price
                reservation.CalculateTotalPrice();

                Console.WriteLine($"The total price for {reservation.customerName} is: ${reservation.totalPrice:F2}");
                Console.WriteLine("------------------------------------------------------------");

                reservations.Add(reservation);
            }

            // Display final summary
            DisplaySummary(reservations);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
