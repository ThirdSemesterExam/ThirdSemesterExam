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


    public List<Pets> GetAllPets()
    {
        return _context.PetsTable.ToList();
    }

    public Pets AddPets(Pets pets)
    {
        _context.PetsTable.Add(pets);
        _context.SaveChanges();
        return pets;
    }
    /*
    public Pets GetPetsById(int id)
    {
        return _context.PetsTable.Find(id) ?? throw new KeyNotFoundException();

    }
    */
    public Pets? GetPetsById(int id)
    {
        return _context.PetsTable.Find(id);
    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public Pets UpdatePets(Pets pets)
    {
        _context.PetsTable.Update(pets);
        _context.SaveChanges();
        return pets;
    }

    public Pets DeletePets(int id)
    {
        var petsToDelete = _context.PetsTable.Find(id) ?? throw new KeyNotFoundException();
        _context.PetsTable.Remove(petsToDelete);
        _context.SaveChanges();
        return petsToDelete;
    }
}