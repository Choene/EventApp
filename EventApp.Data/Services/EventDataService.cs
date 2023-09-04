using Dapper;
using EventApp.Core.Models;
using EventApp.Data.Config;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Data.Services
{
    public class EventDataService
    {
        public async Task<int> AddEventAsync(Event newEvent)
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                const string query = "INSERT INTO Events (Name, TotalSeats) VALUES (@Name, @TotalSeats)";
                return await connection.ExecuteAsync(query, connection);
            }
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                const string query = "SELECT * FROM Events";
                return await connection.QueryAsync<Event>(query);
            }
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                const string query = "SELECT * FROM Events WHERE Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<Event>(query, new { Id = id });
            }
        }

        public async Task<bool> UpdateEventAsync(Event updatedEvent)
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                const string query = "UPDATE Events SET Name = @Name, TotalSeats = @TotalSeats WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(query, updatedEvent);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            using (var connection = new SQLiteConnection(DatabaseConfig.ConnectionString))
            {
                const string query = "DELETE FROM Events WHERE Id = @Id";
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }
    }
}
