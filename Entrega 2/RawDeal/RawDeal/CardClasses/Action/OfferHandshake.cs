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
        gameStructureInfo.Effects.MayStealCards(
            gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), maximumNumberOfCardsToSteal);
        
        const int numberOfCardToDiscard = 1;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(
            gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard);
        
        gameStructureInfo.Effects.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
}