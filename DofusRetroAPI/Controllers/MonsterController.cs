using DofusRetroAPI.Services;
using DofusRetroAPI.Services.Items.Monster;
using DofusRetroClassLibrary.DTOs.Monsters.Archmonster;
using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;
using DofusRetroClassLibrary.DTOs.Monsters.Monster;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;
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
    
    [HttpGet]
    [Route("AllMonsters")]
    public async Task<ActionResult<ServiceResponse<List<GetMonsterDto>>>> GetAllMonsters(int language = 1)
    {
        ServiceResponse<List<GetMonsterDto>> response = await _service.GetAllMonsters(language);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("ArchMonster")]
    public async Task<ActionResult<ServiceResponse<GetMonsterDto>>> AddArchMonster(AddArchMonsterDto addArchMonsterDto)
    {
        ServiceResponse<GetMonsterDto> response = await _service.AddArchMonster(addArchMonsterDto);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpPost]
    [Route("NormalMonster")]
    public async Task<ActionResult<ServiceResponse<GetMonsterDto>>> AddNormalMonster(AddNormalMonsterDto addNormalMonsterDto)
    {
        ServiceResponse<GetMonsterDto> response = await _service.AddNormalMonster(addNormalMonsterDto);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("MonsterCharacteristic")]
    public async Task<ActionResult<ServiceResponse<GetMonsterCharacteristicDto>>> AddMonsterCharacteristic(
        AddMonsterCharacteristicDto addMonsterCharacteristicDto)
    {
        ServiceResponse<GetMonsterCharacteristicDto> response = await _service.AddMonsterCharacteristic(addMonsterCharacteristicDto);
        return StatusCode((int)response.StatusCode!, response);
    }
}