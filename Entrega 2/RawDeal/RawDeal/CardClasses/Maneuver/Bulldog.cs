using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class Bulldog: Card
{
    public Bulldog(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscardCurrentPlayer = 1;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscardCurrentPlayer);
        
        const int numberOfCardToDiscardOpponentPlayer = 1;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscardOpponentPlayer);
    }
}