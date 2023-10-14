using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class PressSlam: Card
{
    public PressSlam(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.ColateralDamage(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, 2);
    }
}