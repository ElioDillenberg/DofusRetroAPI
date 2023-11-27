namespace ClassLibrary.DTOs.Ingredients;

public record GetIngredientDto(
    int Id,
    int ItemId,
    string ItemName,
    int Quantity 
);