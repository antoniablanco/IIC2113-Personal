using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class DoubleLegTakedown: Card
{
    public DoubleLegTakedown(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maximumNumberOfCardsToSteal = 1;
        gameStructureInfo.Effects.MayStealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), maximumNumberOfCardsToSteal);
    }
}