﻿namespace DofusRetroAPI.Entities.Items.Equipments.Weapons;

public sealed class Weapon : Equipment
{
    // Weapon specific characteristic
    public WeaponCharacteristic? WeaponCharacteristic { get; set; }
}