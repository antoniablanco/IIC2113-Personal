namespace RawDeal.CardClass.Maneuver;

public class StandingSideHeadlock: Card
{
    public StandingSideHeadlock(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer,
        gameStructureInfo.GetOpponentPlayer(), 1);
    }
}