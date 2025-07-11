using Application;
using Infrastructure;
using Infrastructure.Data;

namespace TaskForEnsek
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                await app.InitialiseDatabaseAsync();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Configure the HTTP request pipeline.

            app.MapControllers();

            app.Run();
        }
    }
}
