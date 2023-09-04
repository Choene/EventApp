using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Data.Config
{
    public static class DatabaseConfig
    {
        public static string ConnectionString { get; } = "Data Source=eventapp.db;Version=3";
    }
}
