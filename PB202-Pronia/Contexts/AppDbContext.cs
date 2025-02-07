using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PB202_Pronia.Models;

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
    }

    public required DbSet<Product> Products { get; set; }
    public required DbSet<Category> Categories { get; set; }
    public required DbSet<ProductImage> ProductImages { get; set; }
    public required DbSet<Tag> Tags { get; set; }
    public required DbSet<ProductTag> ProductTags { get; set; }
}
