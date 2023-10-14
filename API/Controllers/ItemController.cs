using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.DTOs.Items.ItemDto;
using ClassLibrary.DTOs.Items.ItemEffectDto;
using ClassLibrary.DTOs.Localization;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/item")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;

    public ItemController(IItemService service)
    {
        _service = service;
    }
    
    // Post
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetItemDto>>> AddItem(AddItemDto addItemDto)
    {
       ServiceResponse<GetItemDto> response = await _service.AddItem(addItemDto);
       return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpPost]
    [Route("name")]
    public async Task<ActionResult<ServiceResponse<GetLocalizedStringDto>>> AddItemName(AddLocalizedStringDto addItemNameDto)
    {
        ServiceResponse<GetLocalizedStringDto> response = await _service.AddItemName(addItemNameDto);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpPost]
    [Route("description")]
    public async Task<ActionResult<ServiceResponse<GetLocalizedStringDto>>> AddItemDescription(AddLocalizedStringDto addItemDescriptionDto)
    {
        ServiceResponse<GetLocalizedStringDto> response = await _service.AddItemDescription(addItemDescriptionDto);
        return StatusCode((int)response.StatusCode!, response);
    }

    [HttpPost]
    [Route("condition")]
    public async Task<ActionResult<ServiceResponse<GetItemConditionDto>>> AddItemCondition(AddItemConditionDto addItemConditionDto)
    {
        ServiceResponse<GetItemConditionDto> response = await _service.AddItemCondition(addItemConditionDto);
        return StatusCode((int)response.StatusCode!, response); 
    }
    
    [HttpPost]
    [Route("effect")]
    public async Task<ActionResult<ServiceResponse<GetItemEffectDto>>> AddItemEffect(AddItemEffectDto addItemEffectDto)
    {
        ServiceResponse<GetItemEffectDto> response = await _service.AddItemEffect(addItemEffectDto);
        return StatusCode((int)response.StatusCode!, response); 
    }
    
    [HttpGet]
    [Route("byId")]
    public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItem(int itemId, int language = 1)
    {
        ServiceResponse<GetItemDto> response = await _service.GetItemById(itemId, language);
        return StatusCode((int)response.StatusCode!, response);
    }
} 