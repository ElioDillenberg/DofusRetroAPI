using System.Net;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Localization;
using DofusRetroAPI.Entities.Monsters;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;
using DofusRetroClassLibrary.DTOs.Monsters.Archmonster;
using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;
using DofusRetroClassLibrary.DTOs.Monsters.Monster;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.Items.Monster;

public class MonsterService: IMonsterService
{
    readonly DofusRetroDbContext _dbContext;
    
    public MonsterService(DofusRetroDbContext context)
    {
        _dbContext = context;
    }

    
    public async Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int languageId)
    {
        ServiceResponse<List<GetMonsterDto>> serviceResponse = new ServiceResponse<List<GetMonsterDto>>();
        try
        {
            serviceResponse.Data = await _dbContext.Monsters
                .Select(m => new GetMonsterDto(
                    m.Id,
                    m.GameId,
                    m.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId)!.Name,
                    (int)m.Ecosystem,
                    Localization.EcosystemNames![new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    (int)m.Breed,
                    Localization.EcosystemNames[new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    m.Characteristics
                        .Select(mc => mc.AsGetMonsterCharacteristicDto())
                        .ToList())
                )
                 .ToListAsync();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }

    public Task<ServiceResponse<List<GetArchMonsterDto>>> GetAllArchMonsters(int language)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetMonsterDto>>> GetAllNormalMonsters(int language)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<GetMonsterDto>> AddArchMonster(AddArchMonsterDto addArchMonsterDto)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if GameId is already defined in the Monsters table
            Entities.Monsters.Monster? tryMonster =
                await _dbContext.Monsters
                    .FirstOrDefaultAsync(m => m.GameId == addArchMonsterDto.GameId);
            if (tryMonster != null)
                throw new HttpRequestException(
                    $"Provided GameId {addArchMonsterDto.GameId} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // // Check if the NormalMonster refered exists
            NormalMonster? monster =
                await _dbContext.NormalMonsters
                    .FirstOrDefaultAsync(m => m.GameId == addArchMonsterDto.MonsterGameId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Provided MonsterId {addArchMonsterDto.MonsterGameId} doest not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            ArchMonster archMonster = new ArchMonster()
            {
                GameId = addArchMonsterDto.GameId,
                Monster = monster,
                MonsterId = monster.Id,
                Breed = Breed.Archmonsters,
                Ecosystem = Ecosystem.Archmonsters
            };

            // Save to DB
            _dbContext.ArchMonsters.Add(archMonster);
            await _dbContext.SaveChangesAsync();

            // Return GetMonsterDto
            serviceResponse.Data = new GetMonsterDto(
                Id: archMonster.Id,
                GameId: archMonster.GameId,
                Name: "NoName",
                Ecosystem: (int)archMonster.Ecosystem,
                EcosystemName: Localization.EcosystemNames![
                    new ValueTuple<Ecosystem, Language>(archMonster.Ecosystem, (Language)addArchMonsterDto.LanguageId)],
                Breed: (int)archMonster.Breed,
                BreedName: Localization.BreedNames![
                    new ValueTuple<Breed, Language>(archMonster.Breed, (Language)addArchMonsterDto.LanguageId)],
                new List<GetMonsterCharacteristicDto>()
            );
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }
    
    public async Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(AddMonsterCharacteristicDto addMonsterCharacteristicDto)
    {
        ServiceResponse<GetMonsterCharacteristicDto> serviceResponse = new ServiceResponse<GetMonsterCharacteristicDto>();
        try
        {
            // Check if Monster exists
            Entities.Monsters.Monster? monster = await _dbContext.Monsters
                .FirstOrDefaultAsync(m => m.GameId == addMonsterCharacteristicDto.MonsterGameId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Provided MonsterGameId {addMonsterCharacteristicDto.MonsterGameId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterCharacteristic already exist
            MonsterCharacteristic? monsterCharacteristic = await _dbContext.MonsterCharacteristics
                .FirstOrDefaultAsync(mc => mc.MonsterId == monster.Id && mc.Level == addMonsterCharacteristicDto.Level);
            if (monsterCharacteristic != null)
                throw new HttpRequestException(
                    $"MonsterCharacteristic for MonsterGameId {addMonsterCharacteristicDto.MonsterGameId} and Level {addMonsterCharacteristicDto.Level} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            // Add MonsterCharacteristic to DB
            monsterCharacteristic = addMonsterCharacteristicDto.AsMonsterCharacteristic(monster.Id);
            _dbContext.MonsterCharacteristics.Add(monsterCharacteristic);
            await _dbContext.SaveChangesAsync();
            
            serviceResponse.Data = monsterCharacteristic.AsGetMonsterCharacteristicDto();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetMonsterDto>> AddNormalMonster(AddNormalMonsterDto addNormalMonsterDto)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if GameId is already defined in the Monsters table
            Entities.Monsters.Monster? tryMonster =
                await _dbContext.Monsters.FirstOrDefaultAsync(m => m.GameId == addNormalMonsterDto.GameId);
            if (tryMonster != null)
                throw new HttpRequestException(
                    $"Provided GameId {addNormalMonsterDto.GameId} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if breed exists
            if (!Enum.IsDefined(typeof(Breed), addNormalMonsterDto.Breed))
                throw new HttpRequestException(
                    $"Provided Breed {addNormalMonsterDto.Breed} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ecosystem exists
            if (!Enum.IsDefined(typeof(Ecosystem), addNormalMonsterDto.Ecosystem))
                throw new HttpRequestException(
                    $"Provided Ecosystem {addNormalMonsterDto.Ecosystem} is not defined.", 
                    null,
                    HttpStatusCode.BadRequest);
            
            NormalMonster normalMonster = new NormalMonster()
            {
                GameId = addNormalMonsterDto.GameId,
                Breed = (Breed)addNormalMonsterDto.Breed,
                Ecosystem = (Ecosystem)addNormalMonsterDto.Ecosystem
            };

            _dbContext.NormalMonsters.Add(normalMonster);
            await _dbContext.SaveChangesAsync();
            
            // Return GetMonsterDto
            serviceResponse.Data = new GetMonsterDto(
                Id: normalMonster.Id,
                GameId: normalMonster.GameId,
                Name: "NoName",
                Ecosystem: (int)normalMonster.Ecosystem,
                EcosystemName: Localization.EcosystemNames![
                    new ValueTuple<Ecosystem, Language>(normalMonster.Ecosystem,
                        (Language)addNormalMonsterDto.LanguageId)],
                Breed: (int)normalMonster.Breed,
                BreedName: Localization.BreedNames![
                    new ValueTuple<Breed, Language>(normalMonster.Breed, (Language)addNormalMonsterDto.LanguageId)],
                new List<GetMonsterCharacteristicDto>()
            );
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }
}