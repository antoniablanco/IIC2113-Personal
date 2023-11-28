using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class UndertakerSitsUp: Card
{
    public UndertakerSitsUp(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
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
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.CardBonusName.UndertakerSitsUpDamage, bonusValue:2);
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect(BonusEnum.CardBonusName.UndertakerSitsUpFortitud, bonusValue:25);
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerOpponentPlayer);
        
        const int totalDamage = 4;
        new ColateralDamageEffectUtils(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo, totalDamage);
        
        const int numberOfCardToDiscard = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscard, gameStructureInfo);
    }
    
}