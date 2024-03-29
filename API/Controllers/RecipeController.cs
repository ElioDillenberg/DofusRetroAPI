using ClassLibrary.DTOs.Recipe;
using ClassLibrary.DTOs.ServiceResponse;
using DofusRetroAPI.Services;
using DofusRetroAPI.Services.RecipeService;
using Microsoft.AspNetCore.Mvc;

namespace DofusRetroAPI.Controllers;

[ApiController]
[Route("/api/v1/recipe")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _service;

    public RecipeController(IRecipeService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetRecipeDto>>> AddRecipe(AddRecipeDto addRecipeDto)
    {
        ServiceResponse<GetRecipeDto> response = await _service.AddRecipe(addRecipeDto);
        return StatusCode((int)response.StatusCode!, response);
    }
}