using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterDto;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.MonsterService;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/monster")]
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
    [Route("all")]
    public async Task<ActionResult<ServiceResponse<List<GetMonsterDto>>>> GetAllMonsters(int language = 1)
    {
        ServiceResponse<List<GetMonsterDto>> response = await _service.GetAllMonsters(language);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpGet]
    [Route("byId")]
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
    [Route("characteristic")]
    public async Task<ActionResult<ServiceResponse<GetMonsterCharacteristicDto>>> AddMonsterCharacteristic(
        AddMonsterCharacteristicDto addMonsterCharacteristicDto)
    {
        ServiceResponse<GetMonsterCharacteristicDto> response = await _service.AddMonsterCharacteristic(addMonsterCharacteristicDto);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("name")]
    public async Task<ActionResult<ServiceResponse<GetLocalizedStringDto>>> AddMonsterName(AddLocalizedStringDto addLocalizedStringDto)
    {
        ServiceResponse<GetLocalizedStringDto> response = await _service.AddMonsterNameDto(addLocalizedStringDto);
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