using Microsoft.AspNetCore.Mvc;
using Application;
using Domain;
using Domain.Interfaces;

namespace API.Controllers;


[ApiController]
[Route("[controller]")]
public class PetsController : ControllerBase
{
 
    private IPetsService _petsService;

    public PetsController(IPetsService petsService)
    {
        _petsService = petsService;
    }

    // rebuilding
    [HttpGet]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _petsService.RebuildDB();
    }

}