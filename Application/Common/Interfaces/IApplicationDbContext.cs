using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserAccount> UserAccounts { get; }
    DbSet<MeterReadingData> MetersReadingData { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
