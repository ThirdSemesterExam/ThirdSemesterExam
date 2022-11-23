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
        _petsRepository = repository;
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
}

