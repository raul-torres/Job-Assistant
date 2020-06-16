using Microsoft.EntityFrameworkCore;
 
namespace JobAssistant.Models
{
    public class Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Job> Jobs {get;set;}
        public DbSet<Goal> Goals {get;set;}

    }
}