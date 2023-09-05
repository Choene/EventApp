using System.Data.Common;
using System.Threading.Tasks;
using EventApp.Core.Models;
using Dapper;
using System;
using System.Data.Entity;

namespace EventApp.Data.Services
{
    public class RegistrationService
    {
        private readonly DbConnection _connection;

        public RegistrationService(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<RegistrationResult> RegisterUserAsync(Registration registration)
        {
            // Initialize the RegistrationResult object
            var result = new RegistrationResult { IsSuccessful = false };

            // Check for existing registration
            const string checkSql = "SELECT COUNT(*) FROM Registrations WHERE EventId = @EventId AND Email = @Email";
            var count = await _connection.ExecuteScalarAsync<int>(checkSql, new { EventId = registration.EventId, Email = registration.Email });

            if (count > 0)
            {
                // User is already registered for this event.
                result.Message = "You have already registered for this Event";
                return result;
            }

            string uniqueReferenceNumber = Guid.NewGuid().ToString();
            const string sql = "INSERT INTO Registrations (EventId, UserName, Email, UniqueReferenceNumber) VALUES (@EventId, @UserName, @Email, @UniqueReferenceNumber)";
            var parameters = new
            {
                EventId = registration.EventId,
                UserName = registration.UserName,
                Email = registration.Email,
                UniqueReferenceNumber = uniqueReferenceNumber
            };

            await _connection.ExecuteAsync(sql, parameters);

            // Update the RegistrationResult object to indicate success
            result.IsSuccessful = true;
            result.UniqueReferenceNumber = uniqueReferenceNumber;
            result.Message = "Registration successful";

            return result;
        }


        public async Task<List<Registration>> GetAllRegistrationsAsync()
        {
            const string sql = "SELECT * FROM Registrations";
            return (await _connection.QueryAsync<Registration>(sql)).ToList();
        }

    }
}
