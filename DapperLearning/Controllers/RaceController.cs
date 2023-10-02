using DapperLearning.Contracts;
using DapperLearning.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace DapperLearning.Controllers;

[ApiController]
[Route("api/race")]
public class RaceController: ControllerBase
{
    private readonly IRaceRepository _raceRepo;
    public RaceController(IRaceRepository raceRepo)
    {
        _raceRepo = raceRepo;
    }
    [HttpGet]
    [Route("GetRacesDescription")]
    public async Task<IActionResult> GetRacesDescription()
    {
        try
        {
            var races = await _raceRepo.GetRacesDescription();
            return Ok(races);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("GetRacesFullInfo")]
    public async Task<IActionResult> GetRacesFullInfo()
    {
        try
        {
            var races = await _raceRepo.GetRacesFullInfo();
            if (!races.Any()) return NotFound();
            return Ok(races);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("{name}/GetRaceByName")]
    public async Task<IActionResult> GetRaceByName(string name)
    {
        try
        {
            var race = await _raceRepo.GetRaceByName(name);
            if (race == null) return NotFound();
            return Ok(race);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost]
    [Route("AddNewRace")]
    public async Task<IActionResult> AddNewRace(RaceFullInfoDto newRace)
    {
        try
        {
            await _raceRepo.AddNewRace(newRace);
            return Ok();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}/DeleteRaceById")]
    public async Task<IActionResult> DeleteRaceById(Guid id)
    {
        try
        {
            await _raceRepo.DeleteRaceById(id);
            return Ok();
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}