using RawDeal.GameClasses;

namespace RawDeal.CardClass.Hibrid;

public class Chop: Card
{
    public Chop(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.DiscardCardFromHandNotifying(playedCardController, gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.StealCards( gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
}