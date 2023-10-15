using RawDeal.GameClasses;

namespace RawDeal.CardClass.Hibrid;

public class CollarElbowLockup: Card
{
    public CollarElbowLockup(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.DiscardActionCardWithNoEfect(playedCardController, gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.StealCards( gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
}