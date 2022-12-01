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

        
        if (dto == null)
            throw new ArgumentException("Pets is missing");

        if (dto.Id != null && _petsRepository.GetPetsById((int)dto.Id) != null)
            throw new ArgumentException("Pets already exist");
        
        if (dto.Name == null || dto.Name.Equals(""))
        {
            throw new ArgumentException("Please: write the Name of Pet");
        }
        if (dto.Address == null || dto.Address.Equals(""))
        {
            throw new ArgumentException("Please: write the Address");
        }
        if (dto.Zipcode == null || dto.Zipcode < 1 || dto.Zipcode > 9999)
        {
            throw new ArgumentException("Please: write the Zipcode");
        }
        if (dto.City == null || dto.City.Equals(""))
        {
            throw new ArgumentException("Please: write the Name of City");
        }

        if (dto.Email == String.Empty || dto.Equals(""))
        {
            throw new ArgumentException("Please: write your E-mail");
        }
        
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
    
}
