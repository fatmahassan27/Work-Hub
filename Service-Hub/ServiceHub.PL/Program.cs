
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ServiceHub.BL.Interface;
using ServiceHub.BL.Repository;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.DataBase;
using ServiceHub.PL.Hubs;
using System;

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
			builder.Services.AddScoped<UnitWork>();				//IUNITOFWORK	DIP

			//builder.Services.AddScoped(typeof(IBaseRepo<>),typeof(BaseRepository <>));

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.MapHub<ChatHub>("/chat");
			app.MapHub<NotificationsHub>("/notification");

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
