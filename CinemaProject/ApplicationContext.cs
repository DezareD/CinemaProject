using CinemaProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaProject
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var str = GetConnectionString();
            optionsBuilder.UseMySql(str, ServerVersion.AutoDetect(str));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static string GetConnectionString()
        {
            var root = Directory.GetParent(Environment.CurrentDirectory).FullName;
            Console.WriteLine("trusted connection .env root: " + root);
            var dotenv = Path.Combine(root, ".env");
            EnvFileLoader.Load(dotenv);

            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            var userid = Environment.GetEnvironmentVariable("MYSQL_USER");
            var usersDataBase = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

            Console.WriteLine($"connection string: server={host};userid={userid};pwd={password};port={port};database={usersDataBase};Allow User Variables=true");

            return $"server={host};userid={userid};pwd={password};port={port};database={usersDataBase};Allow User Variables=true";
        }
    }

    public static class EnvFileLoader
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
