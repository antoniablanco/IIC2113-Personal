using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Superkick: Card
{
    public Superkick(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyBonusEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.BonusManager.ApplyNextPlayedCardBonusEffect("Superkick", bonusValue:5, "Damage");
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerCurrentPlayer);
        var turnsBeforeEffectExpires = 2;
        gameStructureInfo.BonusManager.SetTurnsLeftForBonusCounter(turnsBeforeEffectExpires);
        gameStructureInfo.BonusManager.SetBonusCardActivator(gameStructureInfo.CardBeingPlayed);
    }
}