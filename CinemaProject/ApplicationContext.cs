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
            optionsBuilder.UseMySql("server=sql11.freemysqlhosting.net;userid=sql11487449;pwd=GJwdiY6bPe;port=3306;database=sql11487449;Allow User Variables=true",
                ServerVersion.AutoDetect("server=sql11.freemysqlhosting.net;userid=sql11487449;pwd=GJwdiY6bPe;port=3306;database=sql11487449;Allow User Variables=true"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
