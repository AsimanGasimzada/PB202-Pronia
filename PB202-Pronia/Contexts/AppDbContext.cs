using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Models;
using PB202_Pronia.Services;

namespace PB202_Pronia.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDeleted == false);
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddSeedData();

        Category category = new()
        {
            Id = -1,
            Name = "PB202"
        };
        modelBuilder.Entity<Category>().HasData(category);
    }

    public required DbSet<Product> Products { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    public required DbSet<ProductTag> ProductTags { get; set; }
    public required DbSet<BasketItem> BasketItems { get; set; }
}
