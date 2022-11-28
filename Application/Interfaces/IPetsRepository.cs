using Domain;

namespace Application.Interfaces;

public interface IPetsRepository
{
    public List<Pets> GetAllPets();
    public Pets CreateNewPets(Pets pets);
    public Pets GetPetsById(int id);
    public void RebuildDB();
    public Pets UpdatePets(Pets pets);
    public Pets DeletePets(Pets pets);
}