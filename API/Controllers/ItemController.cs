using DofusRetroAPI.Services.Items;
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
}