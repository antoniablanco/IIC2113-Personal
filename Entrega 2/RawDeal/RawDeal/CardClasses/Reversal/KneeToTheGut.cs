using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Reversal;

public class KneeToTheGut : Card
{
    public KneeToTheGut(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        const int maximumDamageProducedByPlayedCard = 7;
        return playedCardController.ContainsSubtype("Strike") &&
               playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               totalDamage <= maximumDamageProducedByPlayedCard &&
               playedCardController.HasAnyTypeDifferentOfReversal() &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
               
    }
}