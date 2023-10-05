using System.Net;
using ClassLibrary.DTOs.Items.CardDto;
using ClassLibrary.DTOs.Items.ItemDto;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Equipments.Gear;
using DofusRetroAPI.Entities.Items.Equipments.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Resources;
using DofusRetroClassLibrary.DTOs.Items.CardDto;
using DofusRetroClassLibrary.DTOs.Localization;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.ItemService;

public class ItemService : ServiceBase, IItemService
{
    public ItemService(DofusRetroDbContext context) : base(context) { }

    public async Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto addItemDto)
    {
        ServiceResponse<GetItemDto> serviceResponse = new ServiceResponse<GetItemDto>();
        try
        {
            // Check Item already exists
            Item? item =
                await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == addItemDto.Id);
            if (item != null)
                throw new HttpRequestException(
                    $"Item with Id {addItemDto.Id} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            // Check ItemType
            if (!Enum.IsDefined(typeof(ItemType), addItemDto.ItemType))
                throw new HttpRequestException(
                    $"Provided Item Type {addItemDto.ItemType} is not valid.",
                    null,
                    HttpStatusCode.BadRequest);

            // Create and add the right type Item to the database
            if (Enum.IsDefined(typeof(CardType), addItemDto.ItemType))
            {
                Card card = new Card
                {
                    Id = addItemDto.Id,
                    CardNumber = addItemDto.Id - 15000,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Cards.AddAsync(card);
                item = card;
            }
            else if (Enum.IsDefined(typeof(GearType), addItemDto.ItemType))
            {
                Gear gear = new Gear
                {
                    Id = addItemDto.Id,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Gears.AddAsync(gear);
                item = gear;
            }
            else if (Enum.IsDefined(typeof(PetType), addItemDto.ItemType))
            {
                Pet pet = new Pet
                {
                    Id = addItemDto.Id,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Pets.AddAsync(pet);
                item = pet;
            }
            else if (Enum.IsDefined(typeof(WeaponType), addItemDto.ItemType))
            {
                Weapon weapon = new Weapon
                {
                    Id = addItemDto.Id,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Weapons.AddAsync(weapon);
                item = weapon;
            }
            else if (Enum.IsDefined(typeof(ResourceType), addItemDto.ItemType))
            {
                Resource resource = new Resource
                {
                    Id = addItemDto.Id,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Resources.AddAsync(resource);
                item = resource;
            }
            else if (Enum.IsDefined(typeof(ConsumableType), addItemDto.ItemType))
            {
                Consumable consumable = new Consumable
                {
                    Id = addItemDto.Id,
                    Level = addItemDto.Level,
                    ItemType = (ItemType)addItemDto.ItemType,
                    Pods = addItemDto.Pods,
                    Image = addItemDto.Image
                };
                await _dbContext.Consumables.AddAsync(consumable);
                item = consumable;
            }
            else
                throw new HttpRequestException(
                    $"The provided Item Type {addItemDto.ItemType} is not valid.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Save adding!
            await _dbContext.SaveChangesAsync();
            
            // Return the added Item
            serviceResponse.Data = new GetItemDto
            (
                Id : item.Id,
                ItemType : (int)item.ItemType,
                Level : item.Level,
                Pods : item.Pods,
                Image : item.Image
            );
        }
        catch (HttpRequestException e)
        {
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }

        return serviceResponse;
    }

    public Task<ServiceResponse<GetCardDto>> AddCard(AddCardDto addCardDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemNameDto(AddLocalizedStringDto addItemNameDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemDescriptionDto(AddLocalizedStringDto addItemDescriptionDto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetItemDto>>> GetAllItems(int language)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetItemDto>> GetItemById(int itemId, int language)
    {
        throw new NotImplementedException();
    }
}