using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class BreakTheHold : Card
{
    public BreakTheHold(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return playedCardController.DoesTheCardContainsSubtype("Submission") &&
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);
    }
}