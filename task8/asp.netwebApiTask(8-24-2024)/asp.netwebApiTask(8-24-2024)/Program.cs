using asp.netwebApiTask_8_24_2024_.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace asp.netwebApiTask_8_24_2024_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Minute)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure DbContext with SQL Server
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyDBconction")));

            // Add Swagger/OpenAPI support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("mydevelopment", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Enable CORS
            app.UseCors("mydevelopment");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // More detailed errors in development
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
