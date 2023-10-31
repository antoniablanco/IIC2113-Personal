using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class RollOutOfTheRing: Card
{
    public RollOutOfTheRing(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maxNumberOfCardsToDiscard = 2;
        new DiscardToObtainRingSideCardsEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo,
            maxNumberOfCardsToDiscard);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
}