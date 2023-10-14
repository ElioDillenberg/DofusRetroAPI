namespace ClassLibrary.DTOs.Localization;

public record AddLocalizedStringDto(
    int EntityId,
    int LanguageId,
    string Value
);