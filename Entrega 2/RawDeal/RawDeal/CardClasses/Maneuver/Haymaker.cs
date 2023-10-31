using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Haymaker: Card
{
    public Haymaker(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("Haymaker", 1);
    }
}