using DofusRetroAPI.Entities.Enums;

namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public class SetName
{
    // Database Id
    public int Id { get; set; }

    // Reference to the Set
    public Set Set { get; set; } = null!;
    public int SetId { get; set; }
    
    // Language
    public Language Language { get; set; }
    
    // Localized Name
    public string Name { get; set; } = string.Empty;
}