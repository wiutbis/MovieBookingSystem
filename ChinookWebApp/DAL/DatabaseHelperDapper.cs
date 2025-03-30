using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using ChinookWebApp.Models;
using ChinookWebApp;

namespace MovieBookingApp.DAL
{
    public class DatabaseHelperDapper
    {
        private readonly string? _connectionString;

        public DatabaseHelperDapper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MovieBookingDB");
        }

        // Get all movies
        public List<Movie> GetAllMovies()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Movies";
                return conn.Query<Movie>(query).ToList();
            }
        }

        // Get all theaters
        public List<Theater> GetAllTheaters()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Theaters";
                return conn.Query<Theater>(query).ToList();
            }
        }

        // Get showtimes for a movie
        public List<Showtime> GetShowtimesForMovie(int movieId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT * FROM Showtimes WHERE MovieID = @MovieID";
                return conn.Query<Showtime>(query, new { MovieID = movieId }).ToList();
            }
        }

        // Get customer by email
        public Customer? GetCustomerByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE Email = @Email";
                return conn.QuerySingleOrDefault<Customer>(query, new { Email = email });
            }
        }

        // Book a ticket
        public void BookTicket(Ticket ticket)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Tickets (ShowtimeID, CustomerID, SeatNumber, Price, Status) 
                                 VALUES (@ShowtimeID, @CustomerID, @SeatNumber, @Price, 'Booked')";
                conn.Execute(query, ticket);
            }
        }

        // Cancel a ticket
        public void CancelTicket(int ticketId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Tickets SET Status = 'Canceled' WHERE TicketID = @TicketID";
                conn.Execute(query, new { TicketID = ticketId });
            }
        }

        // Make a payment
        public void MakePayment(Payment payment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Payments (TicketID, Amount, PaymentMethod, Status) 
                                 VALUES (@TicketID, @Amount, @PaymentMethod, 'Successful')";
                conn.Execute(query, payment);
            }
        }
    }
}


