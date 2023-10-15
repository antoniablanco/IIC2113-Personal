using RawDeal.GameClasses;

namespace RawDeal.CardClass.Reversal;

public class EscapeMove: Card
{
    public EscapeMove(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController)
    {   
        return playedCardController.ContainsSubtype("Grapple") && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.Effects.EndTurn();
    }
}