using RawDeal.GameClasses;

namespace RawDeal.CardClass.Maneuver;

public class AustinElbowSmash: Card
{
    public AustinElbowSmash(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBeReverted()
    {
        return false;
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return gameStructureInfo.LastDamageComited >= 5;
    }
}