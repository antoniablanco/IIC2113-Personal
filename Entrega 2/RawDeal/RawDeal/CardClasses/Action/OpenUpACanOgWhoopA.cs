using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class OpenUpACanOgWhoopA: Card
{
    public OpenUpACanOgWhoopA(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        ApplyEffect(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
    }

    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {   
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
        
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.OpenUpaCanOfWhoopAss, bonusValue:6, 
            BonusEnum.CardBonusType.Damage);
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.CardBonusName.OpenUpaCanOfWhoopAss, bonusValue:20, 
            BonusEnum.CardBonusType.Fortitude);
        
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(playerController);
        
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }
}