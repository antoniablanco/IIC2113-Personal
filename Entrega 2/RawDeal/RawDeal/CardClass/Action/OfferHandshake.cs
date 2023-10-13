namespace RawDeal.CardClass.Action;

public class OfferHandshake: Card
{
    public OfferHandshake(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        gameStructureInfo.CardEffects.MayStealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), 3);
        
        gameStructureInfo.CardEffects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, 1);
        gameStructureInfo.CardEffects.DiscardActionCardToRingAreButNotSaying(playedCardController, gameStructureInfo.GetCurrentPlayer());

    }
}