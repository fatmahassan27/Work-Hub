using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Enum;
using ServiceHub.DAL.Helper;

namespace ServiceHub.DAL.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Ratings { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<IdentityRole<int>>().HasData(
             new IdentityRole<int>

             {
                 Id = 1,
                 Name = Role.Worker.ToString(),
                 NormalizedName = Role.Worker.ToString().ToUpper()
             },
              new IdentityRole<int>

              {
                  Id = 2,
                  Name = Role.User.ToString(),
                  NormalizedName = Role.User.ToString().ToUpper()
              }
              );
                // Configure ChatMessage entity
                modelBuilder.Entity<ChatMessage>()
                    .HasOne(cm => cm.Sender)
                    .WithMany()
                    .HasForeignKey(cm => cm.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<ChatMessage>()
                    .HasOne(cm => cm.Receiver)
                    .WithMany()
                    .HasForeignKey(cm => cm.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure City-District relationship
                modelBuilder.Entity<City>()
                    .HasMany(c => c.Districtlist)
                    .WithOne(d => d.City)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure Job-ApplicationUser relationship
                modelBuilder.Entity<Job>()
                    .HasMany(j => j.Workers)
                    .WithOne(u => u.Job)
                    .HasForeignKey(u => u.JobId)
                    .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure Notification-ApplicationUser relationship
                modelBuilder.Entity<Notification>()
                    .HasOne(n => n.Owner)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure Order-ApplicationUser relationship
                modelBuilder.Entity<Order>()
                    .HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Order>()
                    .HasOne(o => o.Worker)
                    .WithMany()
                    .HasForeignKey(o => o.WorkerId)
                    .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure composite key for Rate entity
                modelBuilder.Entity<Rate>()
                            .HasKey(r => new { r.FromUserId, r.ToUserId });

                // Configure the relationships and other properties
                modelBuilder.Entity<Rate>()
                            .HasOne(r => r.Rater)
                            .WithMany()
                            .HasForeignKey(r => r.FromUserId)
                            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Rate>()
                            .HasOne(r => r.Rated)
                            .WithMany()
                            .HasForeignKey(r => r.ToUserId)
                            .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure ApplicationUser-District relationship
                modelBuilder.Entity<ApplicationUser>()
                            .HasOne(u => u.District)
                            .WithMany()
                            .HasForeignKey(u => u.DistrictId)
                            .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<ApplicationUser>()
                           .HasOne(u => u.Job)
                           .WithMany(j => j.Workers)
                           .HasForeignKey(u => u.JobId) // Use the JobId property as the foreign key
                           .OnDelete(DeleteBehavior.Restrict);
                //---------------------------------------------------------------------
                // Configure UserConnection relationships
                modelBuilder.Entity<UserConnection>()
                    .HasKey(uc => new { uc.ConnectionId, uc.UserId });

                modelBuilder.Entity<UserConnection>()
                    .HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                //---------------------------------------------------------------------
                base.OnModelCreating(modelBuilder);

            }
        }


    }
}
