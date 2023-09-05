using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;
using System.Data.SQLite;

namespace EventApp.Data.Helpers
{
    public static class DatabaseHelper
    {
        public static void InitializeDatabase(string connectionString)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        var command = connection.CreateCommand();

                        command.CommandText = @"
                            CREATE TABLE IF NOT EXISTS Events (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                AvailableSeats NOT NULL DEFAULT 0,
                                TotalSeats INTEGER NOT NULL
                            );

                            CREATE TABLE IF NOT EXISTS Registrations (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                EventId INTEGER NOT NULL,
                                UserName TEXT NOT NULL,
                                Email TEXT NOT NULL,
                                UniqueReferenceNumber TEXT NOT NULL,
                                FOREIGN KEY (EventId) REFERENCES Events(Id)
                            );
                        ";

                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database Initialization Failed", ex);
            }
        }
    }
}
