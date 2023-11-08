using RawDeal.Exceptions;
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
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("Superkick", bonusValue:5);
    }
}