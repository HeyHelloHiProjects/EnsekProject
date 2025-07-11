using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.HasKey(item => item.AccountId);
    }
}
