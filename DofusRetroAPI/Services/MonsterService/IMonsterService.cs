using DofusRetroClassLibrary.DTOs.Monsters.Archmonster;
using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;
using DofusRetroClassLibrary.DTOs.Monsters.GenericMonster;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterName;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;

namespace DofusRetroAPI.Services.MonsterService;

public interface IMonsterService
{
    // Create
    public Task<ServiceResponse<GetMonsterDto>> AddArchMonster(AddArchMonsterDto addArchMonsterDto, int languageId);
    public Task<ServiceResponse<GetMonsterDto>> AddNormalMonster(AddNormalMonsterDto addNormalMonsterDto, int languageId);
    public Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(AddMonsterCharacteristicDto addMonsterCharacteristicDto);
    public Task<ServiceResponse<GetMonsterNameDto>> AddMonsterNameDto(AddMonsterNameDto addMonsterNameDto);
    
    // Read
    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int languageId);
    public Task<ServiceResponse<List<GetArchMonsterDto>>> GetAllArchMonsters(int languageId);
    public Task<ServiceResponse<List<GetNormalMonsterDto>>> GetAllNormalMonsters(int languageId);
}
