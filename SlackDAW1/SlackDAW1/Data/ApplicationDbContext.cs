using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SlackDAW1.Models;

namespace SlackDAW1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<Channel>()
		 //       .HasOne(c => c.Category)
		 //       .WithMany(cat => cat.Channels)
		 //       .HasForeignKey(c => c.CategoryID)
		 //       .OnDelete(DeleteBehavior.Cascade);
		}
    }
}