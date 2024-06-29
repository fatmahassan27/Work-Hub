using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Helper;
using System.Text;
using System.Security.Claims;
using ServiceHub.BL.Interfaces;
using ServiceHub.BL.Services;
using ServiceHub.BL.Mapper;
using ServiceHub.DAL.UnitOfWork;
using ServiceHub.PL.Hubs;
using Microsoft.AspNetCore.Http.Connections;

namespace ServiceHub.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Service");

            builder.Services.AddControllers();

            builder.Services.AddSignalR();

            builder.Services.AddLogging();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            builder.Services.AddScoped<IUnitOfWork, UnitWork>();

            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IDistrictService, DistrictService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IRateService, RateService>();
            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
            builder.Services.AddScoped<IUserConnectionService, UserConnectionService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Worker", policy => policy.RequireClaim(ClaimTypes.Role, "Worker"));
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
            });

            var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
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
                    ValidateIssuer = false,     //
                    ValidateAudience = false,       //
                    ValidateLifetime = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"], 
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                           (path.StartsWithSegments("/notificationsHub") || path.StartsWithSegments("/chatHub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost4200", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .SetIsOriginAllowed((host) => true);// Ensure credentials are allowed if using authentication

                });

            });

             var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    app.UseDeveloperExceptionPage();

                }

                app.UseRouting();

                app.UseCors("AllowLocalhost4200");
                app.UseAuthentication();
                app.UseAuthorization();


            app.MapHub<NotificationsHub>("/notificationsHub", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });
            //app.MapHub<NotificationsHub>("/notificationsHub");
          
            app.MapHub<ChatHub>("/chatHub");
                //app.UseEndpoints(endpoints =>
                //{
                //    endpoints.MapHub<ChatHub>("/chatHub");

                //});

                app.MapControllers();

                app.Run();
            



        }

    }
}
