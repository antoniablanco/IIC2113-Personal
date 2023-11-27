using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Diversion: Card
{
    public Diversion(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect(BonusEnum.BonusType.Diversion, bonusValue:0, "Reversal");
        
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerCurrentPlayer);
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
        
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
    }
}