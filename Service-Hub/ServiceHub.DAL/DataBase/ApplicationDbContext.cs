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
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser >
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			{
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
                modelBuilder.Entity<UserConnection>()
                    .HasKey(uow => new { uow.ConnectionId, uow.UserId });
                
                modelBuilder.Entity<Rate>()
                 .HasKey(x => new { x.Id, x.UserId, x.WorkerId });
                modelBuilder.Entity<Order>()
                            .HasKey(uow => new { uow.WorkerId, uow.UserId, uow.Id });



            }
        }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<City> Cities { get; set; }
		public DbSet<District> Districts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
		public DbSet<Rate> Ratings { get; set; }
		public DbSet<User> Users { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<Worker> Workers { get; set; }

	}
}
