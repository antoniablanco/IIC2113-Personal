using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class BellyToBellySuplex: Card
{
    public BellyToBellySuplex(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy)
    {
        return playedCardController.GetCardTitle() == "Belly to Belly Suplex" && 
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
}