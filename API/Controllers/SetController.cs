using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Sets.SetDto;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.SetService;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/set")]
public class SetController : ControllerBase
{
    private readonly ISetService _service;
    
    public SetController(ISetService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetSetDto>>> AddSet(AddSetDto addSetDto)
    {
        ServiceResponse<GetSetDto> response = await _service.AddSet(addSetDto);
        return StatusCode((int)response.StatusCode!, response);
    }
    
    [HttpPost]
    [Route("name")]
    public async Task<ActionResult<ServiceResponse<GetLocalizedStringDto>>> AddSetNameDto(AddLocalizedStringDto addLocalizedNameDto)
    {
        ServiceResponse<GetLocalizedStringDto> response = await _service.AddSetName(addLocalizedNameDto);
        return StatusCode((int)response.StatusCode!, response);
    }
}