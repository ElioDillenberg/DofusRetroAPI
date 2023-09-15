﻿using DofusRetroAPI.Entities.Items.Equipments.Categories;

namespace DofusRetroAPI.Entities.Items.Equipments.Pets.ResourceEaters;

public class ResourceEater : Pet
{
    // List of resources that the pet can eat
    public List<ResourceEaterFood> FoodTable { get; set; } = new();
}