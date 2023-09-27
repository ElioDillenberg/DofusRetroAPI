using System.Net;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Monsters;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;
using DofusRetroClassLibrary.DTOs.Monsters.Archmonster;
using DofusRetroClassLibrary.DTOs.Monsters.Characteristics;
using DofusRetroClassLibrary.DTOs.Monsters.GenericMonster;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterName;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;
using DofusRetroClassLibrary.Enums.Localization;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.MonsterService;

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
                    Localization.Localization.EcosystemNames![new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    (int)m.Breed,
                    Localization.Localization.EcosystemNames[new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
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

    public async Task<ServiceResponse<GetMonsterDto>> AddArchMonster(AddArchMonsterDto addArchMonsterDto, int languageId)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if language exists
            if (!Enum.IsDefined(typeof(Language), languageId))
                throw new HttpRequestException(
                    $"Provided Language {languageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if GameId is already defined in the Monsters table
            Monster? tryMonster =
                await _dbContext.Monsters
                    .FirstOrDefaultAsync(m => m.GameId == addArchMonsterDto.GameId);
            if (tryMonster != null)
                throw new HttpRequestException(
                    $"A Monster with provided GameId {addArchMonsterDto.GameId} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if the NormalMonster refered exists
            NormalMonster? monster =
                await _dbContext.NormalMonsters
                    .Include(m => m.ArchMonster)
                    .FirstOrDefaultAsync(m => m.GameId == addArchMonsterDto.MonsterGameId);
            if (monster == null)
                throw new HttpRequestException(
                    $"NormalMonster with provided GameId {addArchMonsterDto.MonsterGameId} doest not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if the NormalMonster already has an Archmonster
            if (monster.ArchMonster != null)
                throw new HttpRequestException(
                    $"Monster with GameId {monster.GameId} already has an Archmonster.",
                    null,
                    HttpStatusCode.Conflict);
            
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
                EcosystemName: Localization.Localization.EcosystemNames![
                    new ValueTuple<Ecosystem, Language>(archMonster.Ecosystem, (Language)languageId)],
                Breed: (int)archMonster.Breed,
                BreedName: Localization.Localization.BreedNames![
                    new ValueTuple<Breed, Language>(archMonster.Breed, (Language)languageId)],
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
    
    public async Task<ServiceResponse<GetMonsterCharacteristicDto>> AddMonsterCharacteristic(
        AddMonsterCharacteristicDto addMonsterCharacteristicDto)
    {
        ServiceResponse<GetMonsterCharacteristicDto> serviceResponse = new ServiceResponse<GetMonsterCharacteristicDto>();
        try
        {
            // Check if Monster exists
            Monster? monster = await _dbContext.Monsters
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
    
    public async Task<ServiceResponse<GetMonsterNameDto>> AddMonsterNameDto(AddMonsterNameDto addMonsterNameDto)
    {
        ServiceResponse<GetMonsterNameDto> serviceResponse = new ServiceResponse<GetMonsterNameDto>();
        try
        {
            // Check if language exists
            if (!Enum.IsDefined(typeof(Language), addMonsterNameDto.LanguageId))
                throw new HttpRequestException(
                    $"Provided Language {addMonsterNameDto.LanguageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if Monster exists
            Monster? monster = await _dbContext.Monsters
                .FirstOrDefaultAsync(m => m.GameId == addMonsterNameDto.MonsterGameId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Provided MonsterGameId {addMonsterNameDto.MonsterGameId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterName for Language+Monster already exists
            MonsterName? monsterName = await _dbContext.MonsterNames
                .FirstOrDefaultAsync(mn =>
                    mn.MonsterId == monster.Id &&
                    mn.Language == (Language)addMonsterNameDto.LanguageId);
            if (monsterName != null)
                throw new HttpRequestException(
                    $"MonsterName for Monster with GameId {addMonsterNameDto.MonsterGameId} and Language {addMonsterNameDto.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            // Add MonsterName to database
            monsterName = new MonsterName()
            {
                Monster = monster,
                MonsterId = monster.Id,
                Language = (Language)addMonsterNameDto.LanguageId,
                Name = addMonsterNameDto.Name
            };
            _dbContext.MonsterNames.Add(monsterName);
            await _dbContext.SaveChangesAsync();

            // Response
            serviceResponse.Data = monsterName.AsGetMonsterNameDto();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetMonsterDto>> AddNormalMonster(
        AddNormalMonsterDto addNormalMonsterDto,
        int languageId)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if GameId is already defined in the Monsters table
            Monster? tryMonster =
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
                EcosystemName: Localization.Localization.EcosystemNames![new ValueTuple<Ecosystem, Language>(normalMonster.Ecosystem, (Language)languageId)],
                Breed: (int)normalMonster.Breed,
                BreedName: Localization.Localization.BreedNames![new ValueTuple<Breed, Language>(normalMonster.Breed, (Language)languageId)],
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