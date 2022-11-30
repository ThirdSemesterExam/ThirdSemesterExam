using Application.DTOs;
using Application.DTOs.Application.DTOs;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
//push for test
[ApiController]
[Route("[controller]")]
public class PetsController : ControllerBase
{
    private IPetsService _petsService;

    public PetsController(IPetsService petsService)
    {
        _petsService = petsService;
    }

    [HttpGet]
    public ActionResult<List<Pets>> GetAllPets()
    {
        return _petsService.GetAllPets();
    }

    // rebuilding
    [HttpGet]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _petsService.RebuildDB();
    }

    [HttpPost]
    [Route("")]
    public ActionResult<Pets> CreateNewProduct(PostPetsDTO dto)
    {
        try
        {
            var result = _petsService.CreateNewPets(dto);
            return Created("", result);
        }
        catch (ValidationException v)
        {
            return BadRequest(v.Message);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet]
    [Route("{id}")] //localhost:5001/product/42
    public ActionResult<Pets> GetPetsById(int id)
    {
        try
        {
            return _petsService.GetPetsById(id);
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    
    [HttpPut]
    [Route("{id}")] //localhost:5001/product/8732648732
    public ActionResult<Pets> UpdatePets([FromRoute]int id, [FromBody]Pets pets)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<Pets> DeletePets(int id)
    {
        throw new NotImplementedException();
    }
}