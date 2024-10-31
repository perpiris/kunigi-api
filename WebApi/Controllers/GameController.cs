using Application.Contracts.Game;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateParentGame(CreateParentGameRequest request)
    {
        var result = await _gameService.CreateParentGame(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetParentGameById(Guid id)
    {
        var result = await _gameService.GetParentGameById(id);
        if (!result.IsSuccess)
        {
            return NotFound(result.Errors);
        }
        
        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPagedParentGames(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _gameService.GetPagedParentGames(pageNumber, pageSize);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateParentGame(UpdateParentGameRequest request)
    {
        var result = await _gameService.UpdateParentGame(request);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
}