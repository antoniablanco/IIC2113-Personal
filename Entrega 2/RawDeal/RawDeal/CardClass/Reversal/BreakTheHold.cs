namespace RawDeal.CardClass.Reversal;

public class BreakTheHold: Card
{
     public BreakTheHold(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
         string stunValue, string cardEffect)
         :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
     {
         
     }
     public override bool CanReversalThisCard(CardController playedCardController, string typePlayed)
     {
         return playedCardController.VerifyIfTheCardContainsThisSubtype("Submission") && playedCardController.VerifyIfTheCardIsOfThisType("Maneuver");
     }
     
     public override void ReversalEffect(GameStructureInfo gameStructureInfo)
     {
         gameStructureInfo.CardEffects.EndTurn();
     }
}