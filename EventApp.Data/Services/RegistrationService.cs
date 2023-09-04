using System.Data.Common;
using System.Threading.Tasks;
using EventApp.Core.Models;
using Dapper;
using System;

namespace EventApp.Data.Services
{
    public class RegistrationService
    {
        private readonly DbConnection _connection;

        public RegistrationService(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<string> RegisterUserAsync(Registration registration)
        {
            // Generate a unique reference number. In a real-world application,
            // you'd want a more robust way to generate a unique number.
            string uniqueReferenceNumber = Guid.NewGuid().ToString();

            // Insert the registration into the database.
            const string sql = "INSERT INTO Registrations (EventId, UserName, Email, UniqueReferenceNumber) VALUES (@EventId, @UserName, @Email, @UniqueReferenceNumber)";

            var parameters = new
            {
                EventId = registration.EventId,
                UserName = registration.UserName,
                Email = registration.Email,
                UniqueReferenceNumber = uniqueReferenceNumber
            };

            await _connection.ExecuteAsync(sql, parameters);

            // Return the unique reference number
            return uniqueReferenceNumber;
        }
    }
}
