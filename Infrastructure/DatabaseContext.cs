using System.Collections.Generic;
using System.Reflection.Emit;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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


    public DbSet<Pets> ProductTable { get; set; }
}