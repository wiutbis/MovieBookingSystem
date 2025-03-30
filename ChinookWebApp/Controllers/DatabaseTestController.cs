using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ChinookWebApp.Controllers
{
    public class DatabaseTestController : Controller
    {
        private readonly IConfiguration _configuration;

        public DatabaseTestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult TestConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return Content("✅ Database Connection Successful!");
                }
            }
            catch (Exception ex)
            {
                return Content($"❌ Database Connection Failed: {ex.Message}");
            }
        }
    }
}
