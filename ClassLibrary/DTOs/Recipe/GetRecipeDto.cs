using ClassLibrary.DTOs.Ingredients;

namespace ClassLibrary.DTOs.Recipe;

public record GetRecipeDto(
    int Id,
    int ItemId,
    string ItemName,
    List<GetIngredientDto> Ingredients
);

