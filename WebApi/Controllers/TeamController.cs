using Application.Contracts.Team;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("teams")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(CreateTeamRequest request)
    {
        var result = await _teamService.CreateTeam(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTeamById(Guid id)
    {
        var result = await _teamService.GetTeamById(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPagedTeams(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _teamService.GetPagedTeams(pageNumber, pageSize);
        return Ok(result);
    }
    
    [HttpGet("select-list")]
    public async Task<IActionResult> GetTeamSelectList()
    {
        var result = await _teamService.GetTeamSelectList();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTeam(UpdateTeamRequest request, IFormFile profileImage)
    {
        var result = await _teamService.UpdateTeam(request, profileImage.OpenReadStream());
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
}