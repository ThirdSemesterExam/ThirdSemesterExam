using Application.DTOs;

namespace Domain.Interfaces;

public interface IPetsService
{
    public List<Pets> GetAllPets();
    public Pets AddPets(PostPetsDTO dto);
    public Pets GetPetsById(int id);
    public void RebuildDB();
    public Pets UpdatePets(int id, Pets pet);
    public Pets DeletePets(int id);
}