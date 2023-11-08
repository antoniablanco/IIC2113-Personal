using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class UndertakerSitsUp: Card
{
    public UndertakerSitsUp(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("UndertakerSitsUpDamage", bonusValue:2);
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("UndertakerSitsUpFortitud", bonusValue:25);
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerOpponentPlayer);
    }
    
}