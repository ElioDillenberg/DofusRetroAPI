﻿using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.DTOs.Items.ItemDto;
using ClassLibrary.DTOs.Items.ItemEffectDto;
using ClassLibrary.DTOs.Localization;

namespace DofusRetroAPI.Services.ItemService;

public interface IItemService
{
    // Create
    public Task<ServiceResponse<GetItemDto>> AddItem(AddItemDto addItemDto);
    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemName(AddLocalizedStringDto addItemNameDto);
    public Task<ServiceResponse<GetLocalizedStringDto>> AddItemDescription(AddLocalizedStringDto addItemDescriptionDto);
    public Task<ServiceResponse<GetItemConditionDto>> AddItemCondition(AddItemConditionDto addItemConditionDto);
    public Task<ServiceResponse<GetItemEffectDto>> AddItemEffect(AddItemEffectDto addItemEffectDto);
    
    // Read
    public Task<ServiceResponse<GetItemDto>> GetItemById(int itemId, int language);
}