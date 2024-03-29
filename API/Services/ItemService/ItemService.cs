using System.Net;
using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.DTOs.Items.ItemDto;
using ClassLibrary.DTOs.Items.ItemEffectDto;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.ServiceResponse;
using ClassLibrary.Enums.ItemConditions;
using ClassLibrary.Enums.ItemEffects;
using ClassLibrary.Enums.Languages;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Items.Cards;
using DofusRetroAPI.Entities.Items.Conditions;
using DofusRetroAPI.Entities.Items.Consumables;
using DofusRetroAPI.Entities.Items.Effects;
using DofusRetroAPI.Entities.Items.Equipments.Gear;
using DofusRetroAPI.Entities.Items.Equipments.Pets;
using DofusRetroAPI.Entities.Items.Equipments.Weapons;
using DofusRetroAPI.Entities.Items.Resources;
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
                    Price = addItemDto.Price,
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
                    Price = addItemDto.Price,
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
                    Price = addItemDto.Price,
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
                    Price = addItemDto.Price,
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
                    Price = addItemDto.Price,
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
                    Price = addItemDto.Price,
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
                Name: "",
                Description: "",
                ItemType : (int)item.ItemType,
                Level : item.Level,
                Pods : item.Pods,
                Price : item.Price,
                Image : item.Image,
                Conditions : null,
                Effects : null
            );
        }
        catch (HttpRequestException e)
        {
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetLocalizedStringDto>> AddItemName(AddLocalizedStringDto addLocalizedStringDto)
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
            
            // Check if Item exists
            Item? item = await _dbContext.Items
                .FirstOrDefaultAsync(i => i.Id == addLocalizedStringDto.EntityId);
            if (item == null)
                throw new HttpRequestException(
                    $"Item with Id {addLocalizedStringDto.EntityId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemName for Language+Item already exists
            ItemName? itemName = await _dbContext.ItemNames
                .FirstOrDefaultAsync(mn =>
                    mn.ItemId == item.Id &&
                    mn.Language == (Language)addLocalizedStringDto.LanguageId);
            if (itemName != null)
                throw new HttpRequestException(
                    $"ItemName for Item with Id {addLocalizedStringDto.EntityId} and Language {addLocalizedStringDto.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            itemName = new ItemName()
            {
                Item = item,
                ItemId = item.Id,
                Language = (Language)addLocalizedStringDto.LanguageId,
                Text = addLocalizedStringDto.Value
            };
            _dbContext.ItemNames.Add(itemName);
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = new GetLocalizedStringDto(
                Id: itemName.Id,
                EntityId: itemName.ItemId,
                LanguageId: (int)itemName.Language,
                Name: itemName.Text
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

    public async Task<ServiceResponse<GetLocalizedStringDto>> AddItemDescription(AddLocalizedStringDto addLocalizedStringDto)
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
            
            // Check if Item exists
            Item? item = await _dbContext.Items
                .FirstOrDefaultAsync(i => i.Id == addLocalizedStringDto.EntityId);
            if (item == null)
                throw new HttpRequestException(
                    $"Item with Id {addLocalizedStringDto.EntityId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemDescription for Language+Item already exists
            ItemDescription? itemDescription = await _dbContext.ItemDescriptions
                .FirstOrDefaultAsync(mn =>
                    mn.ItemId == item.Id &&
                    mn.Language == (Language)addLocalizedStringDto.LanguageId);
            if (itemDescription != null)
                throw new HttpRequestException(
                    $"ItemDescription for Item with Id {addLocalizedStringDto.EntityId} and Language {addLocalizedStringDto.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            itemDescription = new ItemDescription()
            {
                Item = item,
                ItemId = item.Id,
                Language = (Language)addLocalizedStringDto.LanguageId,
                Description = addLocalizedStringDto.Value
            };
            _dbContext.ItemDescriptions.Add(itemDescription);
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = new GetLocalizedStringDto(
                Id: itemDescription.Id,
                EntityId: itemDescription.ItemId,
                LanguageId: (int)itemDescription.Language,
                Name: itemDescription.Description
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

    public async Task<ServiceResponse<GetItemConditionDto>> AddItemCondition(AddItemConditionDto addItemConditionDto)
    {
        ServiceResponse<GetItemConditionDto> serviceResponse = new ServiceResponse<GetItemConditionDto>();
        try
        {
            // Check if Item exists
            Item? item = await _dbContext.Items
                .FirstOrDefaultAsync(i => i.Id == addItemConditionDto.ItemId);
            if (item == null)
                throw new HttpRequestException(
                    $"Item with Id {addItemConditionDto.ItemId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemCondition for Item already exists
            ItemCondition? itemCondition = await _dbContext.ItemConditions
                .FirstOrDefaultAsync(mn =>
                    mn.ItemId == item.Id &&
                    mn.ConditionType == (ConditionType)addItemConditionDto.ConditionType &&
                    mn.ConditionSign == (ConditionSign)addItemConditionDto.ConditionSignType &&
                    mn.Value == addItemConditionDto.Value);
            
            if (itemCondition != null)
                throw new HttpRequestException(
                    $"ItemCondition for Item with Id {addItemConditionDto.ItemId} and ConditionType {addItemConditionDto.ConditionType} and Sign {addItemConditionDto.ConditionSignType} and Value {addItemConditionDto.Value} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            itemCondition = new ItemCondition()
            {
                Item = item,
                ItemId = item.Id,
                ConditionType = (ConditionType)addItemConditionDto.ConditionType,
                ConditionSign = (ConditionSign)addItemConditionDto.ConditionSignType,
                Value = addItemConditionDto.Value
            };
            _dbContext.ItemConditions.Add(itemCondition);
            await _dbContext.SaveChangesAsync();
            
            // Add all the localized texts for the condition
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                ItemConditionText itemConditionText = new ItemConditionText()
                {
                    ItemCondition = itemCondition,
                    ItemConditionId = itemCondition.Id,
                    Language = language,
                    Text = itemCondition.BuildStringCondition(language)
                };
                _dbContext.ItemConditionTexts.Add(itemConditionText);
            }
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = itemCondition.AsGetItemConditionDto(1);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            serviceResponse.Message = e.Message;
            serviceResponse.StatusCode = e.StatusCode;
        }
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetItemEffectDto>> AddItemEffect(AddItemEffectDto addItemEffectDto)
    {
        ServiceResponse<GetItemEffectDto> serviceResponse = new ServiceResponse<GetItemEffectDto>();
        try
        {
            // Check if Item exists
            Item? item = await _dbContext.Items
                .FirstOrDefaultAsync(i => i.Id == addItemEffectDto.ItemId);
            if (item == null)
                throw new HttpRequestException(
                    $"Item with Id {addItemEffectDto.ItemId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemEffectType exists
            if (!Enum.IsDefined(typeof(ItemEffectType), addItemEffectDto.EffectType))
                throw new HttpRequestException(
                    $"Provided ItemEffectType {addItemEffectDto.EffectType} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemEffect for Item already exists
            ItemEffect? itemEffect = await _dbContext.ItemEffects
                .FirstOrDefaultAsync(mn =>
                    mn.ItemId == item.Id &&
                    mn.EffectType == (ItemEffectType)addItemEffectDto.EffectType &&
                    mn.MinValue == addItemEffectDto.MinValue &&
                    mn.MaxValue == addItemEffectDto.MaxValue);
            if (itemEffect != null)
                throw new HttpRequestException(
                    $"ItemEffect for Item with Id {addItemEffectDto.ItemId} and EffectType {addItemEffectDto.EffectType} and MinValue {addItemEffectDto.MinValue} and MaxValue {addItemEffectDto.MaxValue} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            itemEffect = new ItemEffect()
            {
                Item = item,
                ItemId = item.Id,
                EffectType = (ItemEffectType)addItemEffectDto.EffectType,
                MinValue = addItemEffectDto.MinValue,
                MaxValue = addItemEffectDto.MaxValue
            };
            _dbContext.ItemEffects.Add(itemEffect);
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = new GetItemEffectDto(
                Id: itemEffect.Id,
                ItemId: itemEffect.ItemId,
                EffectType: (int)itemEffect.EffectType,
                MinValue: itemEffect.MinValue,
                MaxValue: itemEffect.MaxValue
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

    /// <summary>
    /// Add a localized ItemEffectText to the database, it will be used to display the effect of an ItemEffect in the approriate language
    /// </summary>
    /// <param name="addItemEffectText"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<ServiceResponse<GetLocalizedStringDto>> AddItemEffectText(AddLocalizedStringDto addItemEffectText)
    {
        ServiceResponse<GetLocalizedStringDto> serviceResponse = new ServiceResponse<GetLocalizedStringDto>();
        try
        {
            // Check if language exists
            if (!Enum.IsDefined(typeof(Language), addItemEffectText.LanguageId))
                throw new HttpRequestException(
                    $"Provided Language {addItemEffectText.LanguageId} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);
            
            // Check if ItemEffectType exists
            if (!Enum.IsDefined(typeof(ItemEffectType), addItemEffectText.EntityId))
                throw new HttpRequestException(
                    $"ItemEffectText for ItemEffectType with Id {addItemEffectText.EntityId} and Language {addItemEffectText.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);
            
            // Check if ItemEffectText for Language+EffectType already exists
            ItemEffectTypeText? itemEffectTypeText = await _dbContext.ItemEffectTypeTexts
                .FirstOrDefaultAsync(ief =>
                    ief.Id == addItemEffectText.EntityId &&
                    ief.Language == (Language)addItemEffectText.LanguageId);
            if (itemEffectTypeText != null)
                throw new HttpRequestException(
                    $"ItemEffectText for ItemEffectType with Id {addItemEffectText.EntityId} and Language {addItemEffectText.LanguageId} already exists.",
                    null,
                    HttpStatusCode.Conflict);

            itemEffectTypeText = new ItemEffectTypeText()
            {
                Language = (Language)addItemEffectText.LanguageId,
                Text = addItemEffectText.Value,
                EffectType = (ItemEffectType)addItemEffectText.EntityId
            };
            _dbContext.ItemEffectTypeTexts.Add(itemEffectTypeText);
            await _dbContext.SaveChangesAsync();
            
            // Response
            serviceResponse.Data = new GetLocalizedStringDto(
                Id: itemEffectTypeText.Id,
                EntityId: (int)itemEffectTypeText.EffectType,
                LanguageId: (int)itemEffectTypeText.Language,
                Name: itemEffectTypeText.Text
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

    public async Task<ServiceResponse<GetItemDto>> GetItemById(int itemId, int language)
    {
        ServiceResponse<GetItemDto> serviceResponse = new ServiceResponse<GetItemDto>();
        try
        {
            // Check if language exists
            if (!Enum.IsDefined(typeof(Language), language))
                throw new HttpRequestException(
                    $"Provided Language {language} is not defined.",
                    null,
                    HttpStatusCode.BadRequest);

            // Check if Item exists
            Item? item = await _dbContext.Items
                .Include(i => i.Names)
                .Include(i => i.Descriptions)
                .Include(item => item.Conditions)
                .ThenInclude(itemCondiditon => itemCondiditon.ConditionTexts)
                .Include(item => item.Effects)
                .FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null)
                throw new HttpRequestException($"Item with Id {itemId} does not exist.",
                    null,
                    HttpStatusCode.NotFound);

            serviceResponse.Data = new GetItemDto(
                Id: item.Id,
                Name: item.Names.FirstOrDefault(n => n.Language == (Language)language) != null
                    ? item.Names.First(n => n.Language == (Language)language).Text
                    : string.Empty,
                Description: item.Descriptions.FirstOrDefault(n => n.Language == (Language)language) != null
                    ? item.Descriptions.First(n => n.Language == (Language)language).Description
                    : string.Empty,
                ItemType: (int)item.ItemType,
                Level: item.Level,
                Pods: item.Pods,
                Price: item.Price,
                Image: item.Image,
                Conditions: item.Conditions
                    .Select(c => c.AsGetItemConditionDto(language))
                    .ToList(),
                Effects: item.Effects
                    .Select(e => new GetItemEffectDto(
                        Id: e.Id,
                        ItemId: e.ItemId,
                        EffectType: (int)e.EffectType,
                        MinValue: e.MinValue,
                        MaxValue: e.MaxValue
                    ))
                    .ToList()
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