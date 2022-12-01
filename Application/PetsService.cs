using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Domain.Interfaces;

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

        /*
        if (dto == null)
            throw new ArgumentException("Pets is missing");

        if (dto.Id != null && _petsRepository.GetPetsById((int)dto.Id) != null)
            throw new ArgumentException("Pets already exist");

        if (dto.Id == null || dto.Id < 1)
        {
            throw new ArgumentException("Invalid id");
        }
        if (dto.Name == null || dto.Name.Equals(""))
        {
            throw new ArgumentException("Invalid name");
        }
        if (dto.Address == null || dto.Address.Equals(""))
        {
            throw new ArgumentException("Invalid address");
        }
        if (dto.Zipcode == null || dto.Zipcode < 1 || dto.Zipcode > 9999)
        {
            throw new ArgumentException("Invalid zipcode");
        }
        if (dto.City == null || dto.City.Equals(""))
        {
            throw new ArgumentException("Invalid city");
        }
        if (dto.Email == null || dto.Email.Equals(""))
        {
            throw new ArgumentException("Invalid email");
        }

        return _petsRepository.AddPets(_mapper.Map<Pets>(dto));
        */
           var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());

        return _petsRepository.AddPets(_mapper.Map<Pets>(dto));
    }

    public Pets GetPetsById(int id)
    {
        return _petsRepository.GetPetsById(id);

    }

    public void RebuildDB()
    {
        _petsRepository.RebuildDB(); ;
    }
    

    public Pets UpdatePets(int id, Pets pet)
    {
        if (id != pet.Id)
            throw new ValidationException("ID in body and route are different");
        var validation = _petsValidator.Validate(pet);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        return _petsRepository.UpdatePets(pet);
    }
   

    public Pets DeletePets(int id)
    {
        if (_petsRepository.GetPetsById(id) == null)
            throw new ArgumentException("Pets does not exist");

        return _petsRepository.DeletePets(id);
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
    
}
