using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class ReverseDDT: Card
{
    public ReverseDDT(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int maximumNumberOfCardsToSteal = 1;
        gameStructureInfo.Effects.MayStealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), maximumNumberOfCardsToSteal);
    }
}