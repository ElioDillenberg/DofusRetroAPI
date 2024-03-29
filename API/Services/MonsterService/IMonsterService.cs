using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterDto;
using ClassLibrary.DTOs.ServiceResponse;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;

namespace DofusRetroAPI.Services.MonsterService;

public interface IMonsterService
{
    // Create
    public Task<ServiceResponse<GetMonsterDto>> AddMonster(AddMonsterDto addMonsterDto);
    public Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(AddMonsterCharacteristicDto addMonsterCharacteristicDto);
    public Task<ServiceResponse<GetLocalizedStringDto>> AddMonsterNameDto(AddLocalizedStringDto addLocalizedStringDto);
    
    // Read
    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int languageId);
    public Task<ServiceResponse<GetMonsterDto>> GetMonsterById(int monsterId, int languageId);
    
    // Update
    public Task<ServiceResponse<GetMonsterDto>> UpdateMonster(UpdateMonsterDto updateMonsterDto);
}
