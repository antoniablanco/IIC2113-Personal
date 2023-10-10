namespace RawDeal.CardClass.Reversal;

public class CleanBreak: Card
{
    public CleanBreak(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position";
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        gameStructureInfo.CardEffects.DiscardCardsFromHand(gameStructureInfo.ControllerCurrentPlayer, 4, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.CardEffects.StealCard(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), 1);
        gameStructureInfo.CardEffects.EndTurn();
    }
}