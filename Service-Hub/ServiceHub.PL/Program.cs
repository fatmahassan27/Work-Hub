using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;
using System.Text;
using System.Security.Claims;
using ServiceHub.DAL.Interfaces;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.BL.Services;
using ServiceHub.BL.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.Options;
namespace ServiceHub.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string text = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var ConnectionString = builder.Configuration.GetConnectionString("Service");
          
            builder.Services.AddControllers();
            builder.Services.AddSignalR();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            builder.Services.AddScoped<IUnitOfWork,UnitWork>();
            builder.Services.AddScoped<IJobService, JobService>();
            //
           
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(x => { x.AddPolicy("Worker", po => po.RequireClaim(ClaimTypes.Role, "Worker")); });
            builder.Services.AddAuthorization(x => { x.AddPolicy("User", po => po.RequireClaim(ClaimTypes.Role, "User")); });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                    };
                });
			builder.Services.AddCors(
			   Option => {
				   Option.AddPolicy(text,
					   builder =>
					   {
						   builder.AllowAnyOrigin();
						   builder.AllowAnyMethod();
						   builder.AllowAnyHeader();
					   });

			   });

            builder.Services.AddCors(options =>
            {       options.AddPolicy("AllowLocalhost4200",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            var app = builder.Build();

			var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.MapHub<ChatHub>("/chathub");
           // app.MapHub<NotificationsHub>("/notifications");

            app.UseCors("AllowLocalhost4200");
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseCors(text);

            app.MapControllers();

            app.Run();
        }
    }
}
