using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class HellfireBrimstone: Card
{
    public HellfireBrimstone(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        new DiscardHandEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
        new DiscardHandEffect(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo);
        
        const int totalDamage = 5;
        new ProduceDamageEffectUtils(totalDamage, gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo);
    }
}