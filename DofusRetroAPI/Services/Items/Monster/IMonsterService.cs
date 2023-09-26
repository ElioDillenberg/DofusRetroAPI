using DofusRetroClassLibrary.DTOs.Monsters.Archmonster;
using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;
using DofusRetroClassLibrary.DTOs.Monsters.Monster;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;

namespace DofusRetroAPI.Services.Items.Monster;

public interface IMonsterService
{
    // Create
    public Task<ServiceResponse<GetMonsterDto>> AddArchMonster(AddArchMonsterDto addArchMonsterDto);
    public Task<ServiceResponse<GetMonsterDto>> AddNormalMonster(AddNormalMonsterDto addNormalMonsterDto);
    public Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(
        AddMonsterCharacteristicDto addMonsterCharacteristicDto);
    
    // Read
    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int language);
    public Task<ServiceResponse<List<GetArchMonsterDto>>> GetAllArchMonsters(int language);
    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllNormalMonsters(int language);

}
