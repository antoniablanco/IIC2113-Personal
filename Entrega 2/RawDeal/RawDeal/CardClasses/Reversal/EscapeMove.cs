using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class EscapeMove : Card
{
    public EscapeMove(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {
        return playedCardController.ContainsSubtype("Grapple") &&
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }

}