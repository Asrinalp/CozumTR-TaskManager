using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }

    }

}
