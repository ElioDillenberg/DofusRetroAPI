﻿namespace DofusRetroAPI.Entities.Items.Equipments.Pets;

public abstract class Pet : Equipment
{
    public List<PetEffect> Effects { get; set; } = new();
}