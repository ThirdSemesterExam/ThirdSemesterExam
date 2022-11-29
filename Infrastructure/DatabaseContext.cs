using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pets>()
    .Property(p => p.Id)
    .ValueGeneratedOnAdd();
    }


    public DbSet<Pets> PetsTable { get; set;}
}