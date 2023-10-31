using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Y2J: Card
{
    public Y2J(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maximumNumberOfCardsToSteal = 5;
        const int maximumNumberOfCardsToDiscard = 5;
        new DrawOrForceToDiscardEffect(gameStructureInfo, maximumNumberOfCardsToSteal, 
            maximumNumberOfCardsToDiscard);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
    
}