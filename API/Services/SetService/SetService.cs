using System.Net;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Sets.SetDto;
using ClassLibrary.Enums.Languages;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Items.Equipments;
using DofusRetroAPI.Entities.Sets;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.SetService;
 
public class SetService : ISetService
{
    private readonly DofusRetroDbContext _dbContext;
    
    public SetService(DofusRetroDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceResponse<GetSetDto>> AddSet(AddSetDto addSetDto)
    {
        ServiceResponse<GetSetDto> serviceResponse = new ServiceResponse<GetSetDto>();
        try
        {
            Set? set = _dbContext.Sets.FirstOrDefault(s => s.Id == addSetDto.Id);
            if (set != null)
                throw new HttpRequestException($"Set with Id {addSetDto.Id} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            List<Equipment> equipments = new();
            foreach (int equipmentId in addSetDto.EquipmentIds)
            {
                Equipment? equipment = await _dbContext.Equipments.FirstOrDefaultAsync(e => e.Id == equipmentId);
                if (equipment == null)
                    throw new HttpRequestException($"Equipment with Id {equipmentId} does not exist.",
                        null,
                        HttpStatusCode.BadRequest);
                equipments.Add(equipment);
            }

            // Create new Set based on information
            set = new Set
            {
                Id = addSetDto.Id,
                Equipments = equipments,
                SetNames = new List<SetName>(),
                SetBonuses = new List<SetBonus>()
            };
            
            // Add to database
            await _dbContext.Sets.AddAsync(set);
            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = new GetSetDto
            (
                Id: set.Id,
                EquipmentIds: set.Equipments.Select(e => e.Id).ToArray(),
                Name: "",
                null
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

    public async Task<ServiceResponse<GetLocalizedStringDto>> AddSetName(AddLocalizedStringDto addLocalizedStringDto)
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
            Set? set = await _dbContext.Sets
                .FirstOrDefaultAsync(m => m.Id == addLocalizedStringDto.EntityId);
            if (set == null)
                throw new HttpRequestException(
                    $"Set with Id {addLocalizedStringDto.EntityId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if MonsterName for Language+Monster already exists
            SetName? setName = await _dbContext.SetNames
                .FirstOrDefaultAsync(mn =>
                    mn.SetId == set.Id &&
                    mn.Language == (Language)addLocalizedStringDto.LanguageId);
            if (setName != null)
                throw new HttpRequestException(
                    $"SetName for Monster with Id {addLocalizedStringDto.EntityId} and Language {addLocalizedStringDto.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            // Add SetName to database
            setName = new SetName()
            {
                Set = set,
                SetId = set.Id,
                Language = (Language)addLocalizedStringDto.LanguageId,
                Text = addLocalizedStringDto.Value
            };
            _dbContext.SetNames.Add(setName);
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = new GetLocalizedStringDto
            (
                Id : setName.Id,
                EntityId: setName.SetId,
                LanguageId: (int)setName.Language,
                Name: setName.Text
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