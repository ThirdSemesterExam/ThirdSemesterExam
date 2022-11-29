using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class PetsService : IPetsService
{
    private readonly IPetsRepository _petsRepository;
    private IValidator<PostPetsDTO> _postValidator;
    private IValidator<PutPetsDTO> _putPetsValidator;
    private IMapper _mapper;
    
    public PetsService(IPetsRepository repository, IMapper mapper, IValidator<PostPetsDTO> postDeviceValidator, IValidator<PutPetsDTO> putDeviceValidator)
    {
        _petsRepository = repository;
        _mapper = mapper;
        _postValidator = postDeviceValidator;
        _putPetsValidator = putDeviceValidator;
    }
    
    public List<Pets> GetAllPets()
    {
        return _petsRepository.GetAllPets();
    }

    public Pets CreateNewPets(PostPetsDTO dto)
    {
        ThrowsIfPostPetsIsInvalid(dto);
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());

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

    public Pets UpdatePets(int id, Pets product)
    {
        throw new NotImplementedException();
    }

    public Pets DeletePets(Pets pets)
    {
        throw new NotImplementedException();
    }

    /*
    public void AddPets(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        ThrowIfInvalidPets(p);

        if (_petsRepository.GetPetsById(p.Id) != null)
            throw new ArgumentException("Pets already exist");

        _petsRepository.CreateNewPets(p);
    }
    */

    public void UpdatePets(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Pets is missing");

        //ThrowIfInvalidPets(p);

        if (_petsRepository.GetPetsById(p.Id) == null)
            throw new ArgumentException("Pets id does not exist");
        _petsRepository.UpdatePets(p);
    }

    public IEnumerable<Pets> GetAll()
    {
        throw new NotImplementedException();
    }

    /*
    private void ThrowIfInvalidPets(Pets p)
    {
        if (p.Id < 1) throw new ArgumentException("Invalid id");
        if (string.IsNullOrEmpty(p.Name)) throw new ArgumentException("Invalid name");
        if (string.IsNullOrEmpty(p.Address)) throw new ArgumentException("Invalid address");
        if (p.Zipcode < 1 || p.Zipcode > 9999) throw new ArgumentException("Invalid zipcode");
        if (string.IsNullOrEmpty(p.City)) throw new ArgumentException("Invalid city");
        if (p.Email != null && p.Email.Length == 0) throw new ArgumentException("Invalid email");
    }
    */

    /*
    public void RemovePets(Pets p)
    {
        if (p == null)
            throw new ArgumentException("Student is missing");

        if (_petsRepository.GetPetsById(p.Id) == null)
            throw new ArgumentException("Student does not exist");

        _petsRepository.DeletePets(p);
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
    
    
    private void ThrowsIfPostPetsIsInvalid(PostPetsDTO pet)
    {
        if (string.IsNullOrEmpty(pet.Name)) throw new ArgumentException("Dog name cannot be empty or null");
        if (string.IsNullOrEmpty(pet.DogBreeds)) throw new ArgumentException("pet DogBreeds cannot be empty or null");
    }
    
}

