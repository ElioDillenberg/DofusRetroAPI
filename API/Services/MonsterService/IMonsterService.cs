using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterNameDto;

namespace DofusRetroAPI.Services.MonsterService;

public interface IMonsterService
{
    // Create
    public Task<ServiceResponse<GetMonsterDto>> AddMonster(AddMonsterDto addMonsterDto);
    public Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(AddMonsterCharacteristicDto addMonsterCharacteristicDto);
    public Task<ServiceResponse<GetMonsterNameDto>> AddMonsterNameDto(AddMonsterNameDto addMonsterNameDto);
    
    // Read
    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int languageId);
    public Task<ServiceResponse<GetMonsterDto>> GetMonsterById(int monsterId, int languageId);
    
    // Update
    public Task<ServiceResponse<GetMonsterDto>> UpdateMonster(UpdateMonsterDto updateMonsterDto);
}
