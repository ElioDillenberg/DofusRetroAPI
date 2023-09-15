﻿using DofusRetroAPI.Entities.Monsters;

namespace DofusRetroAPI.Entities.Items.Equipments.Pets.SoulEaters;

public class SoulEaterFood : PetFood
{
    // Which monster needs to be eaten to increase the pet's effect
    public Monster Monster { get; set; }
    public int MonsterId { get; set; }
}