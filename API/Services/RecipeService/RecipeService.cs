using System.Net;
using System.Security.AccessControl;
using ClassLibrary.DTOs.Ingredients;
using ClassLibrary.DTOs.Recipe;
using DofusRetroAPI.Database;
using DofusRetroAPI.Entities.Items;
using DofusRetroAPI.Entities.Recipes;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Services.RecipeService;

public class RecipeService : IRecipeService
{
    private readonly DofusRetroDbContext _dbContext;

    public RecipeService(DofusRetroDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceResponse<GetRecipeDto>> AddRecipe(AddRecipeDto addRecipeDto)
    {
        ServiceResponse<GetRecipeDto> serviceResponse = new ServiceResponse<GetRecipeDto>();
        try
        {
            // Check if Item the Recipe is for does exist
            Item? item = _dbContext.Items
                .Include(item => item.Recipe)
                .FirstOrDefault(i => i.Id == addRecipeDto.ItemId);
            if (item == null)
                throw new HttpRequestException($"Item with Id {addRecipeDto.ItemId} does not exist.",
                    null,
                    HttpStatusCode.BadRequest);

            // Check if the item already has a Recipe
            if (item.Recipe != null)
                throw new HttpRequestException($"Item with Id {addRecipeDto.ItemId} already has a recipe.",
                    null,
                    HttpStatusCode.Conflict);
            
            List<Ingredient> ingredients = new();
            foreach (AddIngredientDto addIngredientDto in addRecipeDto.Ingredients)
            {
                // Check item exists,
                Item? ingredientItem = _dbContext.Items.FirstOrDefault(i => i.Id == addIngredientDto.ItemId);
                if (ingredientItem == null)
                    throw new HttpRequestException($"Ingredient (item) with Id {addIngredientDto.ItemId} does not exist.",
                        null,
                        HttpStatusCode.BadRequest);
                
                // Check quantity is > 0
                if (addIngredientDto.Quantity <= 0)
                    throw new HttpRequestException($"Quantity for ingredient with ItemId {addIngredientDto.ItemId} is {addIngredientDto.Quantity}. It must be greater than 0.",
                        null,
                        HttpStatusCode.BadRequest);

                Ingredient ingredient = new Ingredient()
                {
                    ItemId = ingredientItem.Id,
                    Item = ingredientItem,
                    Quantity = addIngredientDto.Quantity
                };
                ingredients.Add(ingredient);
            }
            
            // Create new Recipe based on information
            Recipe recipe = new()
            {
                ItemId = item.Id,
                Item = item,
                Ingredients = ingredients
            };
            
            // Add to database -> is this going to add the Ingredients as well? let's try out!
            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();

            // Response
            serviceResponse.Data = new GetRecipeDto
            (
                Id: recipe.Id,
                ItemId: recipe.ItemId,
                ItemName: "",
                Ingredients: recipe.Ingredients.Select(i => new GetIngredientDto
                (
                    Id : i.Id,
                    ItemId: i.ItemId,
                    ItemName: "",
                    Quantity: i.Quantity
                )).ToList()
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

    public Task<ServiceResponse<GetRecipeDto>> GetRecipeId(int recipeId, int languageId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetRecipeDto>>> GetAllRecipes(int languageId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updateRecipeDto)
    {
        throw new NotImplementedException();
    }
}