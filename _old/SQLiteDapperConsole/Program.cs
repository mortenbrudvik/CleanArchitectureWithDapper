using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Dapper;
using Microsoft.Data.Sqlite;
using PublicApi;

namespace SQLiteDapperConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Data Source=Planner.sqlite";
            SetupDatabase(connectionString);

            var apiService = new ApiService();
            await apiService.StartAsync(args);

            Console.ReadKey();

            await apiService.StopAsync();
        }

        public static void SetupDatabase(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);

            var table = connection.Query("SELECT name FROM sqlite_master WHERE type='table' AND name = 'TaskItems';");
            var tableName = table.FirstOrDefault()?.name;
            if (!string.IsNullOrEmpty(tableName) && tableName == "TaskItems")
                return;

            connection.Execute("Create Table TaskItems (" +
                               "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                               "Name VARCHAR(200) NOT NULL," +
                               "IsCompleted INTEGER DEFAULT 0);");

            // Seed some data
            connection.Execute("INSERT INTO TaskItems (Name, IsCompleted)" +
                               "VALUES (@Name, @IsCompleted);", new TaskItem()
            {
                Name = "Brush teeth"
            });
            connection.Execute("INSERT INTO TaskItems (Name, IsCompleted)" +
                               "VALUES (@Name, @IsCompleted);", new TaskItem()
            {
                Name = "Do some shopping",
                IsCompleted = true
            });

        }
    }
}
