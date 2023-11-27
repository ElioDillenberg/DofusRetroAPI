using ClassLibrary.DTOs.Ingredients;

namespace ClassLibrary.DTOs.Recipe;

public record AddRecipeDto(
    int ItemId,
    List<AddIngredientDto> Ingredients
);