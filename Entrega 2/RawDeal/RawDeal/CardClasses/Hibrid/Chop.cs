using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Hibrid;

public class Chop: Card
{
    public Chop(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }

    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.EffectsUtils.DiscardCardFromHandNotifying(playedCardController,
            gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
}