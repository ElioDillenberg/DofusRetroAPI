using DofusRetroAPI.Entities.Items;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Database;

public class DofusRetroDbContext : DbContext
{
    public DofusRetroDbContext (DbContextOptions<DofusRetroDbContext> options) : base(options) {}

    // Db Sets
    public DbSet<Consumable> Consumables => Set<Consumable>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
}