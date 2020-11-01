using System;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace SQLiteDapperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=Planner.sqlite";
            SetupDatabase(connectionString);

            var task = new TaskItem()
            {
                Name = "Complete the code for this project",
                IsCompleted = true
            };

            var taskRepository = new TaskRepository(connectionString);

            taskRepository.Create(task);

        }

        public static void SetupDatabase(string connectionString)
        {
            using var connection = new SqliteConnection(connectionString);

            var table = connection.Query("SELECT name FROM sqlite_master WHERE type='table' AND name = 'TaskItems';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "TaskItems")
                return;

            connection.Execute("Create Table TaskItems (" +
                               "Name VARCHAR(200) NOT NULL," +
                               "IsCompleted INTEGER DEFAULT 0);");
        }

    }
}
