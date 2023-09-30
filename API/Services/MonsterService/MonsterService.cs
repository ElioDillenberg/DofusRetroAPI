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
                    m.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                        ? m.MonsterNames.First(mn => mn.Language == (Language)languageId).Name
                        : string.Empty,
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

    public async Task<ServiceResponse<List<GetArchMonsterDto>>> GetAllArchMonsters(int language)
    {
        ServiceResponse<List<GetArchMonsterDto>> serviceResponse = new ServiceResponse<List<GetArchMonsterDto>>();
        try
        {
            serviceResponse.Data = await _dbContext.ArchMonsters
                .Select(m => new GetArchMonsterDto(
                    m.Id,
                    m.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)language) != null
                        ? m.MonsterNames.First(mn => mn.Language == (Language)language).Name
                        : string.Empty,
                    (int)m.Ecosystem,
                    Localization.Localization.EcosystemNames![new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)language)],
                    (int)m.Breed,
                    Localization.Localization.EcosystemNames[new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)language)],
                    m.Characteristics
                        .Select(mc => mc.AsGetMonsterCharacteristicDto())
                        .ToList(),
                    m.Monster.Id,
                    m.Monster.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)language) != null
                        ? m.Monster.MonsterNames.First(mn => mn.Language == (Language)language).Name
                        : string.Empty))
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

    public async Task<ServiceResponse<List<GetNormalMonsterDto>>> GetAllNormalMonsters(int languageId)
    {
        ServiceResponse<List<GetNormalMonsterDto>> serviceResponse = new ServiceResponse<List<GetNormalMonsterDto>>();
        try
        {
            serviceResponse.Data = await _dbContext.NormalMonsters
                .Select(m => new GetNormalMonsterDto(
                    m.Id,
                    m.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                        ? m.MonsterNames.First(mn => mn.Language == (Language)languageId).Name
                        : string.Empty,
                    (int)m.Ecosystem,
                    Localization.Localization.EcosystemNames![new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    (int)m.Breed,
                    Localization.Localization.EcosystemNames[new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    m.Characteristics
                        .Select(mc => mc.AsGetMonsterCharacteristicDto())
                        .ToList(),
                    m.ArchMonster != null
                        ? m.ArchMonster.Id
                        : null,
                    m.ArchMonster != null
                        ? m.ArchMonster.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                            ? m.ArchMonster.MonsterNames.First(mn => mn.Language == (Language)languageId).Name
                            : string.Empty
                        : null
                    )
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
                    .FirstOrDefaultAsync(m => m.Id == addArchMonsterDto.Id);
            if (tryMonster != null)
                throw new HttpRequestException(
                    $"A Monster with provided Id {addArchMonsterDto.Id} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if the NormalMonster refered exists
            NormalMonster? monster =
                await _dbContext.NormalMonsters
                    .Include(m => m.ArchMonster)
                    .FirstOrDefaultAsync(m => m.Id == addArchMonsterDto.Id);
            if (monster == null)
                throw new HttpRequestException(
                    $"NormalMonster with provided Id {addArchMonsterDto.MonsterId} doest not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if the NormalMonster already has an Archmonster
            if (monster.ArchMonster != null)
                throw new HttpRequestException(
                    $"Monster with GameId {monster.Id} already has an Archmonster.",
                    null,
                    HttpStatusCode.Conflict);
            
            ArchMonster archMonster = new ArchMonster()
            {
                Id = addArchMonsterDto.Id,
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
                .FirstOrDefaultAsync(m => m.Id == addMonsterCharacteristicDto.MonsterId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Monster with Id {addMonsterCharacteristicDto.MonsterId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterCharacteristic already exist
            MonsterCharacteristic? monsterCharacteristic = await _dbContext.MonsterCharacteristics
                .FirstOrDefaultAsync(mc => mc.MonsterId == monster.Id && mc.Level == addMonsterCharacteristicDto.Level);
            if (monsterCharacteristic != null)
                throw new HttpRequestException(
                    $"MonsterCharacteristic for Monster with Id {addMonsterCharacteristicDto.MonsterId} and Level {addMonsterCharacteristicDto.Level} already exists.",
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
                .FirstOrDefaultAsync(m => m.Id == addMonsterNameDto.MonsterId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Monster with Id {addMonsterNameDto.MonsterId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterName for Language+Monster already exists
            MonsterName? monsterName = await _dbContext.MonsterNames
                .FirstOrDefaultAsync(mn =>
                    mn.MonsterId == monster.Id &&
                    mn.Language == (Language)addMonsterNameDto.LanguageId);
            if (monsterName != null)
                throw new HttpRequestException(
                    $"MonsterName for Monster with Id {addMonsterNameDto.MonsterId} and Language {addMonsterNameDto.LanguageId} already exists.",
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
                await _dbContext.Monsters.FirstOrDefaultAsync(m => m.Id == addNormalMonsterDto.Id);
            if (tryMonster != null)
                throw new HttpRequestException(
                    $"Monster with Id {addNormalMonsterDto.Id} already exists.",
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
                Id = addNormalMonsterDto.Id,
                Breed = (Breed)addNormalMonsterDto.Breed,
                Ecosystem = (Ecosystem)addNormalMonsterDto.Ecosystem
            };

            _dbContext.NormalMonsters.Add(normalMonster);
            await _dbContext.SaveChangesAsync();
            
            // Return GetMonsterDto
            serviceResponse.Data = new GetMonsterDto(
                 Id: normalMonster.Id,
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