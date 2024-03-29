using System.Net;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterDto;
using ClassLibrary.DTOs.ServiceResponse;
using ClassLibrary.Enums.Languages;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Monsters;
using DofusRetroAPI.Entities.Monsters.Breeds;
using DofusRetroAPI.Entities.Monsters.Ecosystems;
using DofusRetroAPI.Localization;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.MonsterService;

public class MonsterService: ServiceBase, IMonsterService
{
    public MonsterService(DofusRetroDbContext context) : base(context) { }
    
    public async Task<ServiceResponse<List<GetMonsterDto>>> GetAllMonsters(int languageId)
    {
        ServiceResponse<List<GetMonsterDto>> serviceResponse = new ServiceResponse<List<GetMonsterDto>>();
        try
        {
            if (!Enum.IsDefined((Language)languageId))
                throw new HttpRequestException(
                    $"Provided Language {languageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            serviceResponse.Data = await _dbContext.Monsters
                .Select(m => new GetMonsterDto(
                    m.Id,
                    m.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                        ? m.MonsterNames.First(mn => mn.Language == (Language)languageId).Text
                        : string.Empty,
                    (int)m.Ecosystem,
                    LocalizedStrings.EcosystemNames![new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    (int)m.Breed,
                    LocalizedStrings.EcosystemNames[new ValueTuple<Ecosystem, Language>(m.Ecosystem, (Language)languageId)],
                    m.RelatedMonsterId,
                    m.RelatedMonster != null
                        ? m.RelatedMonster.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                            ? m.RelatedMonster.MonsterNames.First(mn => mn.Language == (Language)languageId).Text
                            : string.Empty
                        : null, 
                    m.Characteristics
                        .Select(mc => mc.AsGetMonsterCharacteristicDto())
                        .ToList(),
                    m.Image))
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

    public async Task<ServiceResponse<GetMonsterDto>> GetMonsterById(int monsterId, int languageId)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            Monster? monster = await _dbContext.Monsters
                .Include(m => m.MonsterNames)
                .Include(m => m.Characteristics)
                .Include(m => m.RelatedMonster)
                .ThenInclude(rm => rm.MonsterNames)
                .FirstOrDefaultAsync(m => m.Id == monsterId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Monster with Id {monsterId} does not exist.",
                    null,
                    HttpStatusCode.NotFound);

            if (!Enum.IsDefined((Language)languageId))
                throw new HttpRequestException(
                    $"Provided Language {languageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            serviceResponse.Data = new GetMonsterDto(
                Id: monster.Id,
                Name: monster.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                    ? monster.MonsterNames.First(mn => mn.Language == (Language)languageId).Text
                    : string.Empty,
                Ecosystem: (int)monster.Ecosystem,
                EcosystemName: LocalizedStrings.EcosystemNames![new ValueTuple<Ecosystem, Language>(monster.Ecosystem, (Language)languageId)],
                Breed: (int)monster.Breed,
                BreedName: LocalizedStrings.BreedNames![new ValueTuple<Breed, Language>(monster.Breed, (Language)languageId)],
                RelatedMonsterId: monster.RelatedMonsterId,
                RelatedMonsterName: monster.RelatedMonster != null
                    ? monster.RelatedMonster.MonsterNames.FirstOrDefault(mn => mn.Language == (Language)languageId) != null
                        ? monster.RelatedMonster.MonsterNames.First(mn => mn.Language == (Language)languageId).Text
                        : string.Empty
                    : null,
                Characteristics: monster.Characteristics
                    .Select(mc => mc.AsGetMonsterCharacteristicDto())
                    .ToList(),
                Image: monster.Image
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

    public async Task<ServiceResponse<GetMonsterDto>> UpdateMonster(UpdateMonsterDto updateMonsterDto)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if Monster exists
            Monster? monster = await _dbContext.Monsters
                .FirstOrDefaultAsync(m => m.Id == updateMonsterDto.Id);
            if (monster == null)
                throw new HttpRequestException(
                    $"Monster with Id {updateMonsterDto.Id} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if related monster exists
            Monster? relatedMonster = await _dbContext.Monsters
                .FirstOrDefaultAsync(m => m.Id == updateMonsterDto.RelatedMonsterId);
            if (relatedMonster == null)
                throw new HttpRequestException(
                    $"Related Monster with Id {updateMonsterDto.RelatedMonsterId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);

            // Update monster's related monster
            monster.RelatedMonster = relatedMonster;
            monster.RelatedMonsterId = relatedMonster.Id;
            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = new GetMonsterDto(
                Id: monster.Id, 
                Name: "",
                Ecosystem: (int)monster.Ecosystem,
                EcosystemName: "",
                Breed: (int)monster.Breed,
                BreedName:"",
                RelatedMonsterId: monster.RelatedMonsterId,
                RelatedMonsterName: "",
                Characteristics: monster.Characteristics
                    .Select(mc => mc.AsGetMonsterCharacteristicDto())
                    .ToList(),
                monster.Image
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
    
    public async Task<ServiceResponse<GetLocalizedStringDto>> AddMonsterNameDto(AddLocalizedStringDto addLocalizedStringDto)
    {
        ServiceResponse<GetLocalizedStringDto> serviceResponse = new ServiceResponse<GetLocalizedStringDto>();
        try
        {
            // Check if language exists
            if (!Enum.IsDefined(typeof(Language), addLocalizedStringDto.LanguageId))
                throw new HttpRequestException(
                    $"Provided Language {addLocalizedStringDto.LanguageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if Monster exists
            Monster? monster = await _dbContext.Monsters
                .FirstOrDefaultAsync(m => m.Id == addLocalizedStringDto.EntityId);
            if (monster == null)
                throw new HttpRequestException(
                    $"Monster with Id {addLocalizedStringDto.EntityId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterName for Language+Monster already exists
            MonsterName? monsterName = await _dbContext.MonsterNames
                .FirstOrDefaultAsync(mn =>
                    mn.MonsterId == monster.Id &&
                    mn.Language == (Language)addLocalizedStringDto.LanguageId);
            if (monsterName != null)
                throw new HttpRequestException(
                    $"MonsterName for Monster with Id {addLocalizedStringDto.EntityId} and Language {addLocalizedStringDto.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            // Add MonsterName to database
            monsterName = new MonsterName()
            {
                Monster = monster,
                MonsterId = monster.Id,
                Language = (Language)addLocalizedStringDto.LanguageId,
                Text = addLocalizedStringDto.Value
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

    public async Task<ServiceResponse<GetMonsterDto>> AddMonster(AddMonsterDto addMonsterDto)
    {
        ServiceResponse<GetMonsterDto> serviceResponse = new ServiceResponse<GetMonsterDto>();
        try
        {
            // Check if GameId is already defined in the Monsters table
            Monster? monster =
                await _dbContext.Monsters.FirstOrDefaultAsync(m => m.Id == addMonsterDto.Id);
            if (monster != null)
                throw new HttpRequestException(
                    $"Monster with Id {addMonsterDto.Id} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if breed exists
            if (!Enum.IsDefined(typeof(Breed), addMonsterDto.Breed))
                throw new HttpRequestException(
                    $"Provided Breed {addMonsterDto.Breed} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ecosystem exists
            if (!Enum.IsDefined(typeof(Ecosystem), addMonsterDto.Ecosystem))
                throw new HttpRequestException(
                    $"Provided Ecosystem {addMonsterDto.Ecosystem} is not defined.", 
                    null,
                    HttpStatusCode.BadRequest);
            
            monster = new Monster
            {
                Id = addMonsterDto.Id,
                Breed = (Breed)addMonsterDto.Breed,
                Ecosystem = (Ecosystem)addMonsterDto.Ecosystem
            };

            _dbContext.Monsters.Add(monster);
            await _dbContext.SaveChangesAsync();
            
            // Return GetMonsterDto
            serviceResponse.Data = new GetMonsterDto(
                Id: monster.Id,
                Name: "NoName",
                Ecosystem: (int)monster.Ecosystem,
                EcosystemName: "",
                Breed: (int)monster.Breed,
                BreedName: "",
                RelatedMonsterId: null,
                RelatedMonsterName: null,
                new List<GetMonsterCharacteristicDto>(),
                Image: monster.Image
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