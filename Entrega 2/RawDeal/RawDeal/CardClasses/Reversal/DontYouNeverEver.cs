using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class DontYouNeverEver: Card
{
    public DontYouNeverEver(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("DontYouNeverEVER", bonusValue:2);
        gameStructureInfo.BonusManager.SetWhoActivateNextPlayedCardBonusEffect(gameStructureInfo.ControllerOpponentPlayer);
    }
}