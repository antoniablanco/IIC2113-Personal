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
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
    }

    private void ApplyEffect(GameStructureInfo gameStructureInfo, PlayerController playerController)
    {   
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
        
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect("OpenUpaCanOfWhoopAss", bonusValue:6, "Damage");
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect("OpenUpaCanOfWhoopAss", bonusValue:20, "Fortitud");
        
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(playerController);
        
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
    }
}