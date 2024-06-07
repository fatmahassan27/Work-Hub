
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceHub.BL.Interface;
using ServiceHub.BL.Repository;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.Interface;

//using ServiceHub.PL.Hubs;
using System;
using System.Security.Claims;

namespace ServiceHun.PL
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var ConnectionString = builder.Configuration.GetConnectionString("Service");

			builder.Services.AddControllers();
			builder.Services.AddSignalR();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                           .AddEntityFrameworkStores<ApplicationDbContext>()
                           .AddDefaultTokenProviders();

			builder.Services.AddAuthorization(x => { x.AddPolicy("Worker", po => po.RequireClaim(ClaimTypes.Role, "Worker")); }) ;

            // Register your services
            //builder.Services.AddScoped<IUnitOfWork,UnitWork>();
            // Register other services as needed
            // builder.Services.AddScoped<IRepository, Repository>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			//app.MapHub<ChatHub>("/chat");
			//app.MapHub<NotificationsHub>("/notifications");


			app.UseAuthentication();
			app.UseAuthorization();



			app.MapControllers();


			app.Run();
		}
	}
}
