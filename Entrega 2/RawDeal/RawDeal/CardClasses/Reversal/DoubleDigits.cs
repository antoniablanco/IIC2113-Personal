using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class DoubleDigits: Card
{
    public DoubleDigits(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return (playedCardController.ContainsSubtype("Strike") || playedCardController.ContainsSubtype("Grapple")
               || playedCardController.ContainsSubtype("Submission"))&& playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        const int numberOfCardToDiscard = 2;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);
        
        const int totalDamage = 2;
        new ProduceDamageEffectUtils(totalDamage, gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo);
    }
}