using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class DontYouNeverEver: Card
{
    public DontYouNeverEver(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return (playedCardController.ContainsSubtype("Strike") || playedCardController.ContainsSubtype("Grapple")
                                                               || playedCardController.ContainsSubtype("Submission"))
               && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver")  &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        const int numberOfCardToDiscard = 2;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);
        
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.CardBonusName.DontYouNeverEVER, bonusValue:2);
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerOpponentPlayer);
    }
}