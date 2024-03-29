using System.Net;
using ClassLibrary.DTOs.Drop;
using ClassLibrary.DTOs.ServiceResponse;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Drops;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Monsters;

namespace DofusRetroAPI.Services.DropService;

public class DropService : IDropService
{
    private readonly DofusRetroDbContext _dbContext;
    
    public DropService(DofusRetroDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceResponse<GetDropDto>> AddDrop(AddDropDto addDropDto)
    {
        ServiceResponse<GetDropDto> serviceResponse = new();
        try
        {
            // Check if Item exists
            Item? item = _dbContext.Items.FirstOrDefault(i => i.Id == addDropDto.ItemId);
            if (item == null)
                throw new HttpRequestException($"Item with Id {addDropDto.ItemId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);

            // Check if Monster exists
            Monster? monster = _dbContext.Monsters.FirstOrDefault(m => m.Id == addDropDto.MonsterId);
            if (monster == null)
                throw new HttpRequestException($"Monster with Id {addDropDto.MonsterId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if Drop already exists
            Drop? drop = _dbContext.Drops.FirstOrDefault(d => d.ItemId == addDropDto.ItemId && d.MonsterId == addDropDto.MonsterId);
            if (drop != null)
                throw new HttpRequestException($"Drop with ItemId {addDropDto.ItemId} and MonsterId {addDropDto.MonsterId} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if Rate is between 0 and 100
            if (addDropDto.Rate < 0 || addDropDto.Rate > 100)
                throw new HttpRequestException($"Rate for Drop with ItemId {addDropDto.ItemId} and MonsterId {addDropDto.MonsterId} is {addDropDto.Rate}. It must be between 0 and 100.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ProspectionThreshold is between 0 and 800
            if (addDropDto.ProspectionThreshold != null && (addDropDto.ProspectionThreshold < 0 || addDropDto.ProspectionThreshold > 800))
                throw new HttpRequestException($"ProspectionThreshold for Drop with ItemId {addDropDto.ItemId} and MonsterId {addDropDto.MonsterId} is {addDropDto.ProspectionThreshold}. It must be between 0 and 800.",
                    null,
                    HttpStatusCode.BadRequest);

            // Add drop to the database
            drop = new Drop()
            {
                ItemId = item.Id,
                Item = item,
                MonsterId = monster.Id,
                Monster = monster,
                Rate = addDropDto.Rate,
                DropCap = addDropDto.DropCap,
                ProspectionThreshold = addDropDto.ProspectionThreshold
            };
            _dbContext.Drops.Add(drop);
            await _dbContext.SaveChangesAsync();
            
            // Return the drop
            serviceResponse.Data = new GetDropDto(
                Id : drop.Id,
                ItemId : drop.ItemId,
                MonsterId : drop.MonsterId,
                Rate : drop.Rate,
                DropCap : drop.DropCap,
                ProspectionThreshold : drop.ProspectionThreshold
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

    public Task<ServiceResponse<GetDropDto>> GetDropId(int dropId, int languageId)
    {
        throw new NotImplementedException();
    }
}