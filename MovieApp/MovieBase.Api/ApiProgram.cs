
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieBase.Api.Services;
using MovieBase.Data;
using System;

namespace MovieBase.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddNewtonsoftJson()
            .AddXmlSerializerFormatters();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<MovieContext>(o => o
            .UseSqlite("Data Source=./movies.db")
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine));

        builder.Services.AddDbContext<UsersContext>(o=>o.UseInMemoryDatabase("users.db"));

        builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<UsersContext>();
        builder.Services.AddAuthorization();

        builder.Services.AddAutoMapper(options => options.AddProfile<MapperProfile>());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        builder.Services.AddHostedService<AddMovieService>();

        builder.Services.AddSignalR(o => o.KeepAliveInterval = TimeSpan.FromSeconds(15));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRequestLogging();
            //app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");
        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<MessageHub>("/messages");

        app.MapIdentityApi<IdentityUser>();

        app.Run();
    }

    public class UsersContext : IdentityDbContext<IdentityUser>
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
    }
}
