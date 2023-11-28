using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Hibrid;

public class ArmBarTakedown: Card
{
    public ArmBarTakedown(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.EffectsUtils.DiscardCardFromHandNotifying(playedCardController,
            gameStructureInfo.ControllerCurrentPlayer);
        
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
}