using RawDeal.GameClasses;

namespace RawDeal.CardClass.Reversal;

public class StepAside: Card
{
    public StepAside(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
    {
        return playedCardController.VerifyIfTheCardContainsThisSubtype("Strike") && playedCardController.VerifyIfTheCardIsOfThisType("Maneuver");
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.Effects.EndTurn();
    }
}