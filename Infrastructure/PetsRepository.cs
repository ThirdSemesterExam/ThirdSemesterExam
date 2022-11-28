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
        throw new NotImplementedException();
    }

    public Pets CreateNewPets(Pets pets)
    {
        _context.PetsTable.Add(pets);
        _context.SaveChanges();
        return pets;
    }

    public Pets GetPetsById(int id)
    {
        return _context.PetsTable.Find(id) ?? throw new KeyNotFoundException();

    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public Pets UpdatePets(Pets pets)
    {
        throw new NotImplementedException();
    }

    public Pets DeletePets(Pets pets)
    {
        throw new NotImplementedException();
    }
}