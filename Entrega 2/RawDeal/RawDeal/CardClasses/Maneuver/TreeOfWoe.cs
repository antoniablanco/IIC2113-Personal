using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class TreeOfWoe: Card
{
    public TreeOfWoe(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBeReverted()
    {
        return false;
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        const int numberOfCardToDiscard = 2;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscard);
    }
    
}