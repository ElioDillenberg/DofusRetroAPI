using ClassLibrary.Enums.Localization;
using Microsoft.EntityFrameworkCore;

namespace DofusRetroAPI.Entities.Localization;

public abstract class BaseLocalizedName
{
    public int Id { get; set; }
    
    public Language Language { get; set; }

    public string Name { get; set; } = string.Empty;
}