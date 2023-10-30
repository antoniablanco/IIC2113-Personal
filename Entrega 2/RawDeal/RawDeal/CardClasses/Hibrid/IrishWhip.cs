using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class IrishWhip: Card
{
    public IrishWhip(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy)
    {
        return playedCardController.GetCardTitle() == "Irish Whip";
    }
}