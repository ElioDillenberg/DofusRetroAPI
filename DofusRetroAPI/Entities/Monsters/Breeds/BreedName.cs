using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Monsters.Breeds;

public sealed class BreedName : BaseLocalizedName
{
    public BreedName(Breed breed)
    {
        Breed = breed;
    }

    public Breed Breed { get; set; }
    public int BreedId { get; set; }
}