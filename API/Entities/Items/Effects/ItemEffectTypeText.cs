using ClassLibrary.Enums.ItemEffects;
using DofusRetroAPI.Entities.Localization;

namespace DofusRetroAPI.Entities.Items.Effects;

public class ItemEffectTypeText : BaseLocalizedText
{
    public ItemEffectType EffectType { get; set; }
}