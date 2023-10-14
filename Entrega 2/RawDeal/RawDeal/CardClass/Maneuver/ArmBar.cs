namespace RawDeal.CardClass.Maneuver;

public class ArmBar: Card
{
    public ArmBar(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, 1);
    }
}