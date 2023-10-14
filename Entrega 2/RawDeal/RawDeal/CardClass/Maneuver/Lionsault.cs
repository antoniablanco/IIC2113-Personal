namespace RawDeal.CardClass.Maneuver;

public class Lionsault: Card
{
    public Lionsault(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, 1);
    }
    
    public override bool CardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.LastDamageComited >= 4;
    }
}