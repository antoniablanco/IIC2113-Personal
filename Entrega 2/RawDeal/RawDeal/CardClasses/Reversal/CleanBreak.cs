using RawDeal.GameClasses;

namespace RawDeal.CardClass.Reversal;

public class CleanBreak: Card
{
    public CleanBreak(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {
        return playedCardController.GetCardTitle() == "Jockeying for Position";
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        const int numberOfCardToDiscard = 4;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerCurrentPlayer,numberOfCardToDiscard);
        
        const int numberOfCardsToSteal = 1;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
        gameStructureInfo.Effects.EndTurn();
    }
}