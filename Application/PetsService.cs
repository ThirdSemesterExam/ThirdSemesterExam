using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;

namespace Application;

public class PetsService : IPetsService
{
    private readonly IPetsRepository _petsRepository;
   
    public void RebuildDB()
    {
        _petsRepository.RebuildDB();
    }
}

