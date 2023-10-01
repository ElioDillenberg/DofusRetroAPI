using DofusRetroAPI.Services;
using DofusRetroAPI.Services.MonsterService;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterNameDto;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MonsterController : ControllerBase
{
    private readonly IMonsterService _service;

    public MonsterController(IMonsterService service)
    {
        _service = service;
    }
    
    //
    // Get
    //
    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<ServiceResponse<List<GetMonsterDto>>>> GetAllMonsters(int language = 1)
    {
        ServiceResponse<List<GetMonsterDto>> response = await _service.GetAllMonsters(language);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpGet]
    [Route("ById")]
    public async Task<ActionResult<ServiceResponse<GetMonsterDto>>> GetMonster(int monsterId, int language = 1)
    {
        ServiceResponse<GetMonsterDto> response = await _service.GetMonsterById(monsterId, language);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    // Post
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetMonsterDto>>> AddMonster(AddMonsterDto addMonsterDto)
    {
        ServiceResponse<GetMonsterDto> response = await _service.AddMonster(addMonsterDto);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("Characteristic")]
    public async Task<ActionResult<ServiceResponse<GetMonsterCharacteristicDto>>> AddMonsterCharacteristic(
        AddMonsterCharacteristicDto addMonsterCharacteristicDto)
    {
        ServiceResponse<GetMonsterCharacteristicDto> response = await _service.AddMonsterCharacteristic(addMonsterCharacteristicDto);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("Name")]
    public async Task<ActionResult<ServiceResponse<GetMonsterNameDto>>> AddMonsterName(AddMonsterNameDto addMonsterNameDto)
    {
        ServiceResponse<GetMonsterNameDto> response = await _service.AddMonsterNameDto(addMonsterNameDto);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    //
    // Put
    //
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetMonsterDto>>> UpdateMonster(UpdateMonsterDto updateMonsterDto)
    {
        ServiceResponse<GetMonsterDto> response = await _service.UpdateMonster(updateMonsterDto);
        return StatusCode((int)response.StatusCode!, response);
    }
}