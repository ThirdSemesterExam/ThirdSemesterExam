using Application.DTOs;
using Domain;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize]
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

    [AllowAnonymous]
    // rebuilding
    [HttpGet]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _petsService.RebuildDB();
    }

    [Authorize("AdminPolicy")]
    [HttpPost]
    [Route("")]
    public ActionResult<Pets> AddPets(PostPetsDTO dto)
    {
        try
        {
            var result = _petsService.AddPets(dto);
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
    [Route("{id}")]
    public ActionResult<Pets> GetPetsById(int id)
    {
        try
        {
            return _petsService.GetPetsById(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No pet found at ID " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }

    [Authorize("AdminPolicy")]
    [HttpPut]
    [Route("{id}")] 
    public ActionResult<Pets> UpdateP([FromRoute]int id, [FromBody]Pets pet)
    {
        try
        {
            return Ok(_petsService.UpdatePets(id, pet));
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    [Authorize("AdminPolicy")]
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<Pets> DeletePets(int id)
    {
        try
        {
            return Ok(_petsService.DeletePets(id));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No pets found at ID " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}