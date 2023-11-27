using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class KanesReturn: Card
{
    public KanesReturn(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        const int totalDamage = 4;
        new ColateralDamageEffectUtils(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo, totalDamage);
        
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.BonusType.KanesReturnDamage, bonusValue:2);
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.BonusType.KanesReturnFortitud, bonusValue:15);
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerOpponentPlayer);
    }
}