using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configurations;

public class MeterReadingDataConfiguration : IEntityTypeConfiguration<MeterReadingData>
{
    public void Configure(EntityTypeBuilder<MeterReadingData> builder)
    {
        builder.HasKey(data => data.DataId);
        builder.Property(data => data.DataId).ValueGeneratedOnAdd();
    }
}