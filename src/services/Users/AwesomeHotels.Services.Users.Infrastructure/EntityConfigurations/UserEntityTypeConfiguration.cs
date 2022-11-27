using AwesomeHotels.Services.Users.Domain.Entities;
using AwesomeHotels.Services.Users.Domain.ValueObjects;
using AwesomeHotels.Services.Users.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeHotels.Services.Users.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    private const string Schema = "dbo";
    private const string TableName = "Users";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableName, Schema);

        builder.HasKey(t => t.Id);

        builder.Property(x => x.Id).HasConversion(x => x.Value, x => UserId.Create(x));

        builder.OwnsOne(x => x.EmailAddress, b =>
        {
            b.Property(y => y.Value).HasColumnName("EmailAddress");
        });

        builder.OwnsOne(x => x.DateOfBirth, b =>
        {
            b.Property(y => y.Value)
                .HasColumnName("DateOfBirth")
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });

        builder.OwnsOne(x => x.Password, b =>
        {
            b.Property(y => y.Hash).HasColumnName("PasswordHash");
        });

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}