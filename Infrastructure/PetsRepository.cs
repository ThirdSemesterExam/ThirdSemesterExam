using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
namespace Infrastructure;

public class PetsRepository : IPetsRepository
{
    //instance of ProductDbContext called _context
    private readonly DatabaseContext _context;

    // ProductRepository constrcutor with ProductDbContext as argument
    public PetsRepository(DatabaseContext context)
    {
        _context = context;
    }

    
    // can build or rebuilding db
    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

}