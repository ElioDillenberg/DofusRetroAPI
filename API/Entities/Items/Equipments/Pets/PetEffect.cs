using DofusRetroAPI.Entities.Items.Effects;

namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public sealed class PetEffect
{
    // PK
    public int Id { get; set; }
    
    // Pet effect this has an incident on?
    public ItemEffect Effect { get; set; } = null!;
    public int ItemEffectId { get; set; }
    
    // Once improved, this is how high the new stat can go
    public int ImprovedMax { get; set; }
}