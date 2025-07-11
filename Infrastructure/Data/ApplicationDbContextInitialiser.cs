using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        _context.Database.EnsureDeleted();
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.UserAccounts.Any())
        {
            UserAccount[] testUserAccounts = [
            new UserAccount
            {
                AccountId = 1234,
                FirstName = "Freya",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1239,
                FirstName = "Noddy",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1240,
                FirstName = "Archie",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1241,
                FirstName = "Lara",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1242,
                FirstName = "Tim",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1243,
                FirstName = "Graham",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1244,
                FirstName = "Tony",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1245,
                FirstName = "Neville",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1246,
                FirstName = "Jo",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1247,
                FirstName = "Jim",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 1248,
                FirstName = "Pam",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2233,
                FirstName = "Barry",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2344,
                FirstName = "Tommy",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2345,
                FirstName = "Jerry",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2346,
                FirstName = "Ollie",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2347,
                FirstName = "Tara",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2348,
                FirstName = "Tammy",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2349,
                FirstName = "Simon",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2350,
                FirstName = "Colin",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2351,
                FirstName = "Gladys",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2352,
                FirstName = "Greg",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2353,
                FirstName = "Tony",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2355,
                FirstName = "Arthur",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 2356,
                FirstName = "Craig",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 4534,
                FirstName = "JOSH",
                LastName = "TEST"
            },
            new UserAccount
            {
                AccountId = 6776,
                FirstName = "Laura",
                LastName = "Test"
            },
            new UserAccount
            {
                AccountId = 8766,
                FirstName = "Sally",
                LastName = "Test"
            }];

            _context.UserAccounts.AddRange(testUserAccounts);

            await _context.SaveChangesAsync();
        }
    }
}
