namespace DofusRetroAPI.Entities.Items;

public class Weapon : Equipment
{
    public int? NeutralDamageMin { get; set; }
    public int NeutralDamageMax { get; set; }
}