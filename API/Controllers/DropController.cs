using ClassLibrary.DTOs.Drop;
using ClassLibrary.DTOs.ServiceResponse;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.DropService;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/drop")]
public class DropController : ControllerBase
{
    private readonly IDropService _service;

    public DropController(IDropService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetDropDto>>> AddDrop(AddDropDto addDropDto)
    {
        ServiceResponse<GetDropDto> response = await _service.AddDrop(addDropDto);
        return StatusCode((int)response.StatusCode!, response);
    }
}