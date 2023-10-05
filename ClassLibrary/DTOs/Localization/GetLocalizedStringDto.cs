namespace DofusRetroClassLibrary.DTOs.Localization;

public record GetLocalizedStringDto(
    int Id,
    int EntityId,
    int LanguageId,
    string Name
);