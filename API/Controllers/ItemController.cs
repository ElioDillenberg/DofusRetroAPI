using ClassLibrary.DTOs.Items.ItemDto;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
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
}