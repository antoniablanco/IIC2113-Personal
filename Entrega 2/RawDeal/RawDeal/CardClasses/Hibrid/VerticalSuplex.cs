using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class VerticalSuplex: Card
{
    public VerticalSuplex(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy)
    {
        return playedCardController.GetCardTitle() == "Vertical Suplex" && 
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
}