using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class ThePeoplesEyebrow: Card
{
    public ThePeoplesEyebrow(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maxNumberOfCardsToObtain = 2;
        new AddingChoosingCardFromRingSideToHandEffectUtils(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo, maxNumberOfCardsToObtain);
        
        const int numberOfDamageToRecover = 2;
        new GetBackDamageEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo, numberOfDamageToRecover);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
    
}