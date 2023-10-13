namespace RawDeal.CardClass.Action;

public class SpitAtOpponent: Card
{
    public SpitAtOpponent(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.CardEffects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, 1);
        gameStructureInfo.CardEffects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, 4);
        gameStructureInfo.CardEffects.DiscardActionCardToRingAreButNotSaying(playedCardController, gameStructureInfo.GetCurrentPlayer());
    }
    
    public override bool CardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.ControllerCurrentPlayer.NumberOfCardsInTheHand() >= 2;
    }
}