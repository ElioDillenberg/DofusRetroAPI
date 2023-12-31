﻿using DofusRetroAPI.Entities.Items.Resources;

namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public sealed class ResourceEaterFood : PetFood
{
    // Which resource needs to be eaten to increase the pet's effect
    public Resource Resource { get; set; } = null!;
    public int ResourceId { get; set; }
}