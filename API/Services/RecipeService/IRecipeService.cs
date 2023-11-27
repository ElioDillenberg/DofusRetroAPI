using ClassLibrary.DTOs.Recipe;

namespace DofusRetroAPI.Services.RecipeService;

public interface IRecipeService
{
    // Create
    public Task<ServiceResponse<GetRecipeDto>> AddRecipe(AddRecipeDto addRecipeDto);
    
    // Read
    public Task<ServiceResponse<GetRecipeDto>> GetRecipeId(int recipeId, int languageId);
    public Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipes(int languageId);

    // Update
    public Task<ServiceResponse<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updateRecipeDto);
}