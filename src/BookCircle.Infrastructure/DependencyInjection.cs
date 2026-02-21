using BookCircle.Application.Services.Implementations;
using BookCircle.Application.Services.Interfaces;
using BookCircle.Domain.Interfaces;
using BookCircle.Infrastructure.Data;
using BookCircle.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookCircle.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IClubRepository, ClubRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();

        // Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IMeetingService, MeetingService>();

        return services;
    }
}
