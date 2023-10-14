using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class AustinElbowSmash: Card
{
    public AustinElbowSmash(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CardCanBeReverted()
    {
        return false;
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
    
    public override bool CardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.LastDamageComited >= 5;
    }
}