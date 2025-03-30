using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ChinookWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace MovieBookingApp.DAL
{
    public class DatabaseHelper
    {
        private readonly string? _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MovieBookingDB");
        }

        // Get all movies
        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT MovieID, Title, Genre, Duration, Language, ReleaseDate FROM Movies";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            MovieID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Genre = reader.GetString(2),
                            Duration = reader.GetInt32(3),
                            Language = reader.GetString(4),
                            ReleaseDate = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return movies;
        }

        // Get showtimes for a specific movie
        public List<Showtime> GetShowtimesByMovie(int movieId)
        {
            var showtimes = new List<Showtime>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT ShowtimeID, MovieID, TheaterID, ScreenNumber, ShowDate, ShowTime 
                    FROM Showtimes 
                    WHERE MovieID = @MovieID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MovieID", movieId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            showtimes.Add(new Showtime
                            {
                                ShowtimeID = reader.GetInt32(0),
                                MovieID = reader.GetInt32(1),
                                TheaterID = reader.GetInt32(2),
                                ScreenNumber = reader.GetInt32(3),
                                ShowDate = reader.GetDateTime(4),
                                ShowTime = reader.GetTimeSpan(5)
                            });
                        }
                    }
                }
            }
            return showtimes;
        }

        // Book a ticket
        public void BookTicket(int showtimeId, int customerId, string seatNumber, decimal price)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Tickets (ShowtimeID, CustomerID, SeatNumber, Price, Status) 
                    VALUES (@ShowtimeID, @CustomerID, @SeatNumber, @Price, 'Booked')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ShowtimeID", showtimeId);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get customer by email
        public Customer? GetCustomerByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT CustomerID, FirstName, LastName, Email, PhoneNumber FROM Customers WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4)
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Process payment
        public void ProcessPayment(int ticketId, decimal amount, string paymentMethod)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Payments (TicketID, Amount, PaymentMethod, Status) 
                    VALUES (@TicketID, @Amount, @PaymentMethod, 'Successful')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
