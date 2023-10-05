namespace DofusRetroClassLibrary.DTOs.Localization;

public record AddLocalizedStringDto(
    int EntityId,
    int LanguageId,
    string Name
);