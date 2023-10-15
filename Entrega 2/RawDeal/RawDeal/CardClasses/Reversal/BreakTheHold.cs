using RawDeal.GameClasses;

namespace RawDeal.CardClass.Reversal;

public class BreakTheHold: Card
{
     public BreakTheHold(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
         string stunValue, string cardEffect)
         :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
     {
         
     }
     public override bool CanReversalThisCard(CardController playedCardController)
     {
         return playedCardController.ContainsSubtype("Submission") && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
     }
     
     public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
     {
         gameStructureInfo.Effects.EndTurn();
     }
}