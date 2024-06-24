using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Enums;

namespace ServiceHub.DAL.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Ratings { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seeding data

            #region Roles
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
            #endregion
            #region Cities
            modelBuilder.Entity<City>().HasData(
                new { id = 1, Name = "Alexandria" }, new { id = 2, Name = "Cairo" }, new { id = 3, Name = "Dakahlia" },
                new { id = 4, Name = "Giza" }, new { id = 5, Name = "Red Sea" }, new { id = 6, Name = "Fayoum" },
                new { id = 7, Name = "Ismailia" }, new { id = 8, Name = "Minya" }, new { id = 9, Name = "New Valley" },
                new { id = 10, Name = "Suez" }, new { id = 11, Name = "Aswan" }, new { id = 12, Name = "Assiut" },
                new { id = 13, Name = "Beni Suef" }, new { id = 14, Name = "Port Said" }, new { id =15, Name = "Luxor" },
                new { id = 16, Name = "Matrouh" }, new { id = 17, Name = "Qena" }, new { id = 18, Name = "North Sinai" }
                );
            #endregion
            #region Districts
            modelBuilder.Entity<District>().HasData(
                new { Id = 1, Name = "Montaza District", CityId = 1 }, new { Id = 2, Name = "East District", CityId = 1 }, new { Id = 3, Name = "Central District", CityId = 1 },
                new { Id = 4, Name = "West District", CityId = 1 }, new { Id = 5, Name = "El Amreya District", CityId = 1 }, new { Id = 6, Name = "Borg El Arab District", CityId = 1 },
                new { Id = 7, Name = "Zamelak", CityId = 2 }, new { Id = 8, Name = "Zayed", CityId = 2 }, new { Id = 9, Name = "Maady", CityId = 2 },
                new { Id = 10, Name = "Nasr City", CityId = 2 }, new { Id = 11, Name = "New Cairo", CityId = 2 }, new { Id = 12, Name = "Helwan", CityId = 2 },
                new { Id = 13, Name = "Mansoura", CityId = 3 }, new { Id = 14, Name = "Talkha", CityId = 3 }, new { Id = 15, Name = "Mit Ghamr", CityId = 3 },
                new { Id = 16, Name = "Sinbillawin", CityId = 3 }, new { Id = 17, Name = "El-Mahalla El-Kubra", CityId = 3 }, new { Id = 18, Name = "Sherbin", CityId = 3 },
                new { Id = 19, Name = "Al-Haram", CityId = 4 }, new { Id = 20, Name = "Al-Ayat", CityId = 4 }, new { Id = 21, Name = "Imbaba", CityId = 4 },
                new { Id = 22, Name = "Al-Omraneyah", CityId = 4 }, new { Id = 23, Name = "Al-Warraq", CityId = 4 }, new { Id = 24, Name = "Bulaq El Dakrour", CityId = 4 },
                new { Id = 25, Name = "Hurghada", CityId = 5}, new { Id = 26, Name = "Ras Ghareb", CityId = 5 }, new { Id = 27, Name = "Safaga", CityId = 5 },
                new { Id = 28, Name = "El Qoseir", CityId = 5 }, new { Id = 29, Name = "Marsa Alam", CityId = 5 }, new { Id = 30, Name = "Shalateen", CityId = 5 },
              
                new { Id = 31, Name = "Tamiya", CityId = 6 }, new { Id = 32, Name = "Senuris", CityId = 6 }, new { Id = 33, Name = "Etsa", CityId = 6 },
                new { Id = 34, Name = "Fayed", CityId = 7 }, new { Id = 35, Name = "Tel El Kebir", CityId = 7 }, new { Id = 36, Name = "Abu Suwayr", CityId = 7 },
                new { Id = 37, Name = "El Idwa", CityId = 8 }, new { Id = 38, Name = "Matai", CityId = 8 }, new { Id = 39, Name = "Samalut", CityId = 8 },
                new { Id = 40, Name = "Kharga", CityId = 9 }, new { Id = 41, Name = "Dakhla", CityId = 9 }, new { Id = 42, Name = "Farafra", CityId = 9 },
                new { Id = 43, Name = "Ataka", CityId = 10 }, new { Id = 44, Name = "Al Arbaeen", CityId = 10 }, new { Id = 45, Name = "Al Ganayen", CityId = 10 },
                new { Id = 46, Name = "Daraw", CityId = 11 }, new { Id = 47, Name = "Kom Ombo", CityId = 11 }, new { Id = 48, Name = "Edfu", CityId = 11 },
                new { Id = 49, Name = "Dayrout", CityId = 12 }, new { Id = 50, Name = "Manfalut", CityId = 12 }, new { Id = 51, Name = "El Ghanayem", CityId = 12 },
                new { Id = 52, Name = "El Wasta", CityId = 13 }, new { Id = 53, Name = "Nasser", CityId = 13 }, new { Id = 54, Name = "Biba", CityId = 13 },
                new { Id = 55, Name = "Al Zohour", CityId = 14 }, new { Id = 56, Name = "Al Arab", CityId = 14 }, new { Id = 57, Name = "Al Shark", CityId = 14 },
                new { Id = 58, Name = "Armant", CityId = 15 }, new { Id = 59, Name = "Esna", CityId = 15 }, new { Id = 60, Name = "Tiba", CityId = 15 },
                new { Id = 61, Name = "Marsa Matrouh", CityId = 16 }, new { Id = 62, Name = "El Alamein", CityId = 16 }, new { Id = 63, Name = "Siwa", CityId = 16 },
                new { Id = 64, Name = "Farshut", CityId = 17 }, new { Id = 65, Name = "Dishna", CityId = 17 }, new { Id = 66, Name = "Nag Hammadi", CityId = 17 },
                new { Id = 67, Name = "Arish", CityId = 18 }, new { Id = 68, Name = "Rafah", CityId = 18 }, new { Id = 69, Name = "Nakhl", CityId = 18 }
                );
            #endregion
            #region Jobs
            modelBuilder.Entity<Job>().HasData(
                new { Id = 1, Name = "Carpenter", Price = 400 },
                new { Id = 2, Name = "Electrician", Price = 400 },
                new { Id = 3, Name = "Plumber", Price = 400 },
                new { Id = 4, Name = "Blacksmith", Price = 400 },
                new { Id = 5, Name = "HVAC Technician", Price = 400 },
                new { Id = 6, Name = "Construction Worker", Price = 400 },
                new { Id = 7, Name = "Automotive Mechanic", Price = 400 },
                new { Id = 8, Name = "Maintenance Technician", Price = 500 },
                new { Id = 9, Name = "Tile Installer", Price = 300 },
                new { Id = 10, Name = "Welder", Price = 400 },
                new { Id = 11, Name = "Mason", Price = 500 },
                new { Id = 12, Name = "Tailor", Price = 350 },
                new { Id = 13, Name = "Baker", Price = 450 },
                new { Id = 14, Name = "Butcher", Price = 400 },
                new { Id = 15, Name = "Barber", Price = 150 },
                new { Id = 16, Name = "Furniture Upholsterer", Price = 500 },
                new { Id = 17, Name = "Jewelry Maker", Price = 500 },
                new { Id = 18, Name = "Glass Blower", Price = 500 },
                new { Id = 19, Name = "Ceramic Artist", Price = 500 },
                new { Id = 20, Name = "Leatherworker", Price = 500 },
                new { Id = 21, Name = "Florist", Price = 250 },
                new { Id = 22, Name = "Sign Painter", Price = 300 },
                new { Id = 23, Name = "Tailor/Seamstress", Price = 500 },
                new { Id = 24, Name = "Goldsmith", Price = 500 },
                new { Id = 25, Name = "Potter", Price = 300 },
                new { Id = 26, Name = "Cobbler", Price = 200 },
                new { Id = 27, Name = "Roofer", Price = 350 },
                new { Id = 28, Name = "Window Installer", Price = 400 },
                new { Id = 29, Name = "Life Coach", Price = 500 },
                new { Id = 30, Name = "School Teacher", Price = 400 },
                new { Id = 31, Name = "University Lecturer", Price = 700 },
                new { Id = 32,Name = "Language Instructor", Price = 400 },
                new { Id = 33, Name = "Music Teacher", Price = 250 },
                new { Id = 34, Name = "Art Instructor", Price = 400 },
                new { Id = 35, Name = "Fitness Trainer", Price = 400 },
                new { Id = 36, Name = "Dance Instructor", Price = 400 },
                new { Id = 37, Name = "Driving Instructor", Price = 400 },
                new { Id = 38, Name = "Programming Instructor", Price = 500 },
                new { Id = 39, Name = "Technical Trainer", Price = 500 },
                new { Id = 40, Name = "Corporate Trainer", Price = 700 },
                new { Id = 41, Name = "Martial Arts Instructor", Price = 400 },
                new { Id = 42, Name = "Yoga Instructor", Price = 400 },
                new { Id = 43, Name = "Swimming Coach", Price = 500 },
                new { Id = 44, Name = "Graphic Design Instructor", Price = 500 },
                new { Id = 45, Name = "Culinary Instructor", Price = 500 },
                new { Id = 46, Name = "Photography Instructor", Price = 400 },
                new { Id = 47, Name = "First Aid Instructor", Price = 500 },
                new { Id = 48, Name = "Accounting Instructor", Price = 500 }
            );
            #endregion

            #endregion

            #region Configuring entities
            //---------------------------------------------------------------------
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
            #endregion

            base.OnModelCreating(modelBuilder);            
        }
    }
}
