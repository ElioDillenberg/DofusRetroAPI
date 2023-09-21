﻿using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Equipments.Sets;

public sealed class SetName : BaseLocalizedName
{
    // Reference to the Set
    public Set Set { get; set; }
    public int SetId { get; set; }
}