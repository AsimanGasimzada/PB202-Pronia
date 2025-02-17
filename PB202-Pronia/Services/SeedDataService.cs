using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Models;

namespace PB202_Pronia.Services;

public static class SeedDataService
{
    public static void AddSeedData(this ModelBuilder modelBuilder)
    {

        IdentityRole admin = new() { Id = "ccfdb7ca-6ada-411d-be54-196fa5a390e7", Name = "Admin", NormalizedName = "ADMIN" };
        IdentityRole moderator = new() { Id = "78dbc786-5226-4028-9772-7b17eee997ea", Name = "Moderator", NormalizedName = "MODERATOR" };
        IdentityRole member = new() { Id = "34cf98c4-6752-4334-a41a-dcd6cfa09cd4", Name = "Member", NormalizedName = "MEMBER" };


        modelBuilder.Entity<IdentityRole>().HasData(admin, moderator, member);


        AppUser adminUser = new()
        {
            Id = "7c60df9f-e193-41fb-97ca-a7669fb2faca",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEFsNZBh+epZl9PtcEWTQ/RFyKmqg78J8CtETfv6IbL8U0KzZX/u0I38YSTzTcZRLJA==",
            SecurityStamp = "NCSJXLWMZICDXVVDECA6D6YO74BI64A2",
            ConcurrencyStamp = "37e364e2-f7fb-4593-8d64-1c3f71104fb3"
        };

        modelBuilder.Entity<AppUser>().HasData(adminUser);

        IdentityUserRole<string> identityUserRole = new()
        {
            UserId = adminUser.Id,
            RoleId = admin.Id
        };

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(identityUserRole);

    }
}
