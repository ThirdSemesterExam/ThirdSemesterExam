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
        IPetsRepository repository,
        IValidator<PostPetsDTO> postValidator,
        IValidator<Pets> productValidator,
        IMapper mapper)
    {
        _mapper = mapper;
        _postValidator = postValidator;
        _petsValidator = productValidator;
        _petsRepository = repository ?? throw new ArgumentException("Missing PetsRepository");
    }
    public List<Pets> GetAllPets()
    {
        return _petsRepository.GetAllPets();
    }

    public Pets CreateNewPets(PostPetsDTO dto)
    {
        throw new NotImplementedException();
    }

    public Pets GetPetsById(int id)
    {
        throw new NotImplementedException();

    }

    public void RebuildDB()
    {
        _petsRepository.RebuildDB();;
    }

    public Pets UpdatePets(int id, Pets product)
    {
        throw new NotImplementedException();
    }

    public Pets DeletePets(int id)
    {
        throw new NotImplementedException();
    }

    public void AddStudent(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        ThrowIfInvalidStudent(p);

        if (_petsRepository.GetById(p.Id) != null)
            throw new ArgumentException("Pets already exist");

        _petsRepository.Add(p);
    }

    public void UpdateStudent(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        ThrowIfInvalidStudent(p);

        if (_petsRepository.GetById(p.Id) == null)
            throw new ArgumentException("Pets id does not exist");
        _petsRepository.Update(p);
    }

    public IEnumerable<Pets> GetAll()
    {
        throw new NotImplementedException();
    }

    public Pets? GetPetsById(int id)
    {
        throw new NotImplementedException();
    }

    private void ThrowIfInvalidStudent(Pets p)
    {
        if (p.Id < 1) throw new ArgumentException("Invalid id");
        if (string.IsNullOrEmpty(p.Name)) throw new ArgumentException("Invalid name");
        if (string.IsNullOrEmpty(p.Address)) throw new ArgumentException("Invalid address");
        if (p.Zipcode < 1 || p.Zipcode > 9999) throw new ArgumentException("Invalid zipcode");
        if (string.IsNullOrEmpty(p.City)) throw new ArgumentException("Invalid city");
        if (p.Email != null && p.Email.Length == 0) throw new ArgumentException("Invalid email");
    }

    public void RemoveStudent(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Student is missing");

        if (_petsRepository.GetById(p.Id) == null)
            throw new ArgumentException("Student does not exist");

        _petsRepository.Delete(p);
    }

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
    }
}

