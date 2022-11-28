using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class PetsService : IPetsService
{
    private readonly IPetsRepository _petsRepository;

    private readonly IValidator<PostPetsDTO> _postValidator;
    private readonly IValidator<Pets> _petsValidator;
    private readonly IMapper _mapper;

    public PetsService(
        IPetsRepository repository)
    {
        _petsRepository = repository ?? throw new ArgumentException("Missing PetsRepository");
    }
    public PetsService(
        IPetsRepository repository,
        IValidator<PostPetsDTO> postValidator,
        IValidator<Pets> petsValidator,
        IMapper mapper)
    {
        _mapper = mapper;
        _postValidator = postValidator;
        _petsValidator = petsValidator;
        _petsRepository = repository ?? throw new ArgumentException("Missing PetsRepository");
    }
    public List<Pets> GetAllPets()
    {
        return _petsRepository.GetAllPets();
    }

    public Pets AddPets(PostPetsDTO dto)
    {

        if (dto == null)
            throw new ArgumentException("Pets is missing");

        if (dto.Id != null && _petsRepository.GetPetsById((int)dto.Id) != null)
            throw new ArgumentException("Pets already exist");

        return _petsRepository.AddPets(_mapper.Map<Pets>(dto));
    }

    public Pets GetPetsById(int id)
    {
        throw new NotImplementedException();

    }

    public void RebuildDB()
    {
        _petsRepository.RebuildDB();;
    }

    public Pets UpdatePets(int id, Pets pets)
    {
        throw new NotImplementedException();
    }

    public void DeletePets(Pets pets)
    {
        if (pets == null)
            throw new ArgumentException("Pets is missing");

        if (_petsRepository.GetPetsById(pets.Id) == null)
            throw new ArgumentException("Pets does not exist");

        _petsRepository.DeletePets(pets);
    }

    /*
    public void AddPets(Pets p) //Eksrta addPets
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        ThrowIfInvalidPets(p);

        if (_petsRepository.GetPetsById(p.Id) != null)
            throw new ArgumentException("Pets already exist");

        _petsRepository.AddPets(p);
    }*/
    

    public void UpdatePets(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        ThrowIfInvalidPets(p);

        if (_petsRepository.GetPetsById(p.Id) == null)
            throw new ArgumentException("Pets id does not exist");
        _petsRepository.UpdatePets(p);
    }

    public IEnumerable<Pets> GetAll()
    {
        throw new NotImplementedException();
    }

    private void ThrowIfInvalidPets(Pets p)
    {
        if (p.Id < 1) throw new ArgumentException("Invalid id");
        if (string.IsNullOrEmpty(p.Name)) throw new ArgumentException("Invalid name");
        if (string.IsNullOrEmpty(p.Address)) throw new ArgumentException("Invalid address");
        if (p.Zipcode < 1 || p.Zipcode > 9999) throw new ArgumentException("Invalid zipcode");
        if (string.IsNullOrEmpty(p.City)) throw new ArgumentException("Invalid city");
        if (p.Email != null && p.Email.Length == 0) throw new ArgumentException("Invalid email");
    }

    /*
    void IPetsService.Add(Pets p)
    {
        throw new NotImplementedException();
    }

    void IPetsService.Update(Pets p)
    {
        throw new NotImplementedException();
    }

    void IPetsService.Delete(Pets p)
    {
        throw new NotImplementedException();
    }

    void IPetsService.GetAll()
    {
        throw new NotImplementedException();
    }

    void IPetsService.GetById(int v)
    {
        throw new NotImplementedException();
    }*/
}

