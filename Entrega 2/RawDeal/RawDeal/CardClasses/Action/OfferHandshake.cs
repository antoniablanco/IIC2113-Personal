using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Action;

public class OfferHandshake: Card
{
    public OfferHandshake(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int maximumNumberOfCardsToSteal = 3;
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).MayStealCards(maximumNumberOfCardsToSteal);
        
        const int numberOfCardToDiscard = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
}