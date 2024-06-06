using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.DataBase
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			{

                modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
              
                
                modelBuilder.Entity<Rate>()
                            .HasKey(x => new { x.Id, x.UserId, x.WorkerId });


                // Configure ChatMessage-User relationship
                modelBuilder.Entity<ChatMessage>()
                            .HasOne(cm => cm.User)
                            .WithMany(u => u.ChatMessages)
                            .HasForeignKey(cm => cm.UserId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure ChatMessage-Worker relationship
                modelBuilder.Entity<ChatMessage>()
                            .HasOne(cm => cm.Worker)
                            .WithMany()
                            .HasForeignKey(cm => cm.WorkerId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure Notification-User relationship
                modelBuilder.Entity<Notification>()
                            .HasOne(n => n.User)
                            .WithMany(u => u.Notifications)
                            .HasForeignKey(n => n.UserId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure Order-User relationship
                modelBuilder.Entity<Order>()
                            .HasOne(o => o.User)
                            .WithMany(u => u.UserOrders)
                            .HasForeignKey(o => o.UserId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure Order-Worker relationship
                modelBuilder.Entity<Order>()
                            .HasOne(o => o.Worker)
                            .WithMany(u => u.WorkerOrders)
                            .HasForeignKey(o => o.WorkerId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure ApplicationUser-District relationship
                modelBuilder.Entity<ApplicationUser>()
                            .HasOne(u => u.District)
                            .WithMany()
                            .HasForeignKey(u => u.DistrictId)
                            .OnDelete(DeleteBehavior.Restrict);

                // Configure ApplicationUser-Job relationship
                modelBuilder.Entity<ApplicationUser>()
                           .HasOne(u => u.Job)
                           .WithMany()
                           .HasForeignKey(u => u.JobId)
                           .OnDelete(DeleteBehavior.Restrict);

                // Configure composite key
                modelBuilder.Entity<UserConnection>()
                    .HasKey(uc => new { uc.ConnectionId, uc.UserId });

                // Configure the foreign key relationship
                modelBuilder.Entity<UserConnection>()
                    .HasOne(uc => uc.User)
                    .WithMany()
                    .HasForeignKey(uc => uc.UserId);
               

                base.OnModelCreating(modelBuilder);


            }
        }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<City> Cities { get; set; }
		public DbSet<District> Districts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
		public DbSet<Rate> Ratings { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }

    }
}
