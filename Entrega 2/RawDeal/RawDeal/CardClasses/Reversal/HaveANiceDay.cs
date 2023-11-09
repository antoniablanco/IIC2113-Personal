using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class HaveANiceDay: Card
{
    public HaveANiceDay(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {
        return (playedCardController.ContainsSubtype("Strike") || playedCardController.ContainsSubtype("Grapple")
                                                               || playedCardController.ContainsSubtype("Submission"))
               && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver");
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        new DiscardHandCardsEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
    }
}